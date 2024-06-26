using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web_Giam_Sat_Massan.Data;
using Web_Giam_Sat_Massan.Models;

namespace Web_Giam_Sat_Massan.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        public RoleSystem roleSystem = new RoleSystem();
        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault();

                if (user != null)
                {
                    string role = user.Role.Trim();
                    if(role == "Admin")
                    {
                        roleSystem.isAdmin = true;
                    }
                    else
                    {
                        roleSystem.isAdmin = false;
                    }

                    HttpContext.Session.SetString("Username", user.Username);
                    HttpContext.Session.SetString("Role", user.Role);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Sai tài khoản hoặc mật khẩu");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Users
                {
                    Username = model.Username,
                    Password = model.Password, // Bạn nên mã hóa mật khẩu trước khi lưu
                    Role = model.Role
                };

                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        
        public IActionResult Logout()
        {
            return RedirectToAction("Login", "Account");
        }
    }

}
