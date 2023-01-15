using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebMVC.Data;
using WebMVC.Helpers;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _db;

        public UserController(AppDbContext db)
        {
            _db = db;
        }
        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<User> objUserList = _db.User;
            return View(objUserList);
        }

        public IActionResult Login()
        {            
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Login(User obj)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            obj.Password = HashPassword.Hash(obj.Password);
            if (!_db.User.Any(user => user.Name == obj.Name))
            {
                ModelState.AddModelError("Name", "Юзер с таким логином не найден");
                return View();
            }
            if (!_db.User.Any(user => user.Name == obj.Name && user.Password == obj.Password))
            {
                ModelState.AddModelError("Password", "Неверный пароль");
                return View();
            }
            await Authenticate(obj.Name);
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Registry()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registry(User obj)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (_db.User.Any(x => x.Name == obj.Name))
            {
                ModelState.AddModelError("Name", "Такой логин уже существует");
                return View();
            }
            obj.Password = HashPassword.Hash(obj.Password);
            _db.User.Add(obj);
            _db.SaveChanges();
            await Authenticate(obj.Name);
            return RedirectToAction("Index", "Home");

        }

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
