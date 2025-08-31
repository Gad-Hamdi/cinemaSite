using CinemaTask.DataAccess;
using CinemaTask.Models;
using CinemaTask.Utility;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTask.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]

    public class CinemaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CinemaController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var cinemas = _context.Cinemas.ToList();
            return View(cinemas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cinema cinema)
        {
            if (!ModelState.IsValid)
                return View(cinema);

            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var cinema = _context.Cinemas.Find(id);
            if (cinema == null) return NotFound();
            return View(cinema);
        }

        [HttpPost]
        public IActionResult Edit(Cinema cinema)
        {
            if (!ModelState.IsValid)
                return View(cinema);

            _context.Cinemas.Update(cinema);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var cinema = _context.Cinemas.Find(id);
            if (cinema == null) return NotFound();

            _context.Cinemas.Remove(cinema);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


    }
}
