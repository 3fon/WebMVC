using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMVC.Data;
using WebMVC.Helpers;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class TrenerController : Controller
    {
        private readonly AppDbContext _db;
        public TrenerController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Trener> TrenerList = _db.Trener;
            return View(TrenerList);
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Trener obj)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _db.Trener.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            
            var trener = await _db.Trener.FirstOrDefaultAsync(trener => trener.Id == id);
            if(trener == null) {
                return RedirectToAction("Index", "Home");
            }
            _db.Remove(trener);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
    }
}
