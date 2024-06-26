using Microsoft.EntityFrameworkCore;
using System;
using Web_Giam_Sat_Massan.Data;
using System.Net.Sockets;
using Web_Giam_Sat_Massan.Models;
using System.Threading;
using Web_Giam_Sat_Massan.Controllers;
using Microsoft.AspNetCore.Identity;
using Web_Giam_Sat_Massan.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext"))); 
       
       builder.Services.Configure<CookiePolicyOptions>(options =>
        {
            options.MinimumSameSitePolicy = SameSiteMode.None;
            options.Secure = CookieSecurePolicy.Always;
        });
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<RoleService>();

        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian session hết hạn
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        builder.Services.AddSingleton<MSProduct>();

        var app = builder.Build();       

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseSession();
        app.UseRouting();
        app.UseCookiePolicy();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Account}/{action=Login}/{id?}");
        IoT.ConnectToPlc();
        Timer timer = new System.Threading.Timer(Read, app, TimeSpan.Zero, TimeSpan.FromMilliseconds(500));
        app.Run();

    }

    private static void Read(object? state)
    {
        IoT.ConnectToPlc();
        IoT.Readdata();
    }
}

