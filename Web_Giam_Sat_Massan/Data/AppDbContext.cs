using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web_Giam_Sat_Massan.Models;



namespace Web_Giam_Sat_Massan.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }        
        public DbSet<Product> Product { get; set; }
        public DbSet<ShiftLeader> ShiftLeader { get; set; }
        public DbSet<Line> Line { get; set; }
        public DbSet<Machine> Machine { get; set; }
        public DbSet<Shift> Shift { get; set; }
        public DbSet<Shift1> Shift1 { get; set; }
        public DbSet<Shift2> Shift2 { get; set; }
        public DbSet<Shift3> Shift3 { get; set; }
        public DbSet<Shift6> Shift6 { get; set; }
        public DbSet<Users> Users {  get; set; }
        public DbSet<Record> Record { get; set; }
    }
}
