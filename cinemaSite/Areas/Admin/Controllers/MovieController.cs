using CinemaTask.DataAccess;
using CinemaTask.Models;
using CinemaTask.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CinemaTask.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]

    public class MovieController : Controller
    {
        private readonly ApplicationDbContext _context;
        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var movies = _context.Movies
            .Include(m => m.Cinema)
            .Include(m => m.Category)
            .ToList();
            return View(movies);
        }


        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Cinemas = new SelectList(_context.Cinemas, "Id", "Name");
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");

            return View();
        }


        [HttpPost]

        public IActionResult Create(Movie movie)
        {

            _context.Movies.Add(movie);
            _context.SaveChanges();

            ViewBag.Cinemas = new SelectList(_context.Cinemas, "Id", "Name", movie.CinemaId);
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", movie.CategoryId);
            return RedirectToAction("Index");



        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null) return NotFound();

            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(Movie movie)
        {

            _context.Movies.Update(movie);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = _context.Movies.Find(id);
            if (movie == null) return NotFound();

            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
