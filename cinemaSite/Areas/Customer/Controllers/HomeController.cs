using cinemaSite.DataAccess;
using cinemaSite.Models;
using cinemaSite.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace cinemaSite.Areas.Customer.Controllers
{
    [Area(SD.CustomerArea)]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string name, int page = 1)
        {
            var moviesQuery = _context.Movies
                .Include(m => m.Cinema)
                .Include(m => m.Category)
                .Include(m => m.Actors)
                .AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                moviesQuery = moviesQuery.Where(m => m.Name != null && m.Name.Contains(name));
            }

            //// Handle NULL values in the query
            //moviesQuery = moviesQuery.Where(m =>
            //    m.Name != null &&
            //    m.Cinema != null &&
            //    m.Category != null);

            // Pagination - get total count first
            var totalCount = moviesQuery.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / 4);

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;

            // Apply pagination
            var movies = moviesQuery.Where(m => m.Name != null && m.Cinema != null && m.Category != null)
                .Skip((page - 1) * 4)
                .Take(4)
                .ToList();

            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult MovieDetails(int id)
        {
            var movie = _context.Movies
                .Include(m => m.Category)
                .Include(m => m.Cinema)
                .Include(m => m.Actors)
                .FirstOrDefault(m => m.Id == id);

            if (movie is null) return NotFound();

            // Handle NULL values for actors
            ViewBag.Actors = movie.Actors?
                .Where(a => a != null && a.FirstName != null && a.LastName != null)
                .ToList() ?? new List<Actor>();

            return View(movie);
        }

        public IActionResult ActorDetails(int id)
        {
            var actor = _context.Actors
                .Include(a => a.Movies)
                .FirstOrDefault(a => a.Id == id);

            if (actor is null) return NotFound();

            // Handle NULL values for movies
            ViewBag.movies = actor.Movies?
                .Where(m => m != null && m.Name != null)
                .ToList() ?? new List<Movie>();

            return View(actor);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}