using CinemaTask.DataAccess;
using CinemaTask.Models;
using CinemaTask.Utility;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTask.Areas.Admin.Controllers
{
        [Area(SD.AdminArea)]
    public class ActorController : Controller
    {
        private ApplicationDbContext _context;

        public ActorController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var actor = _context.Actors;
            return View(actor.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Actor actor)
        {
            _context.Actors.Add(actor);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var actor = _context.Actors.FirstOrDefault(e => e.Id == id);

            if (actor is null)
                return NotFound();

            return View(actor);
        }

        [HttpPost]
        public IActionResult Edit(Actor actor)
        {
            _context.Actors.Update(actor);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var actor = _context.Actors.FirstOrDefault(e => e.Id == id);

            if (actor is null)
                return NotFound();

            _context.Actors.Remove(actor);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
