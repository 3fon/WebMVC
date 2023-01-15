using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebMVC.Helpers;
using WebMVC.Models;
using WebMVC.Data;

namespace WebMVC.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _db;
        public AdminController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login(Admin obj)
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            obj.Password = HashPassword.Hash(obj.Password);
            if (!_db.Admin.Any(user => user.Login == obj.Login))
            {
                ModelState.AddModelError("Login", "Админ с таким логином не найден");
                return View();
            }
            if (!_db.Admin.Any(user => user.Login == obj.Login && user.Password == obj.Password))
            {
                ModelState.AddModelError("Password", "Неверный пароль");
                return View();
            }
            await Authenticate(obj.Login);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Treners(Admin obj)
        {
            IEnumerable<User> TrenerList = _db.User;
            return View(TrenerList);
        }

        public IActionResult TrenerAdd(Admin obj)
        {
            IEnumerable<User> TrenerList = _db.User;
            return View(TrenerList);
        }
        //
        public async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));


        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
