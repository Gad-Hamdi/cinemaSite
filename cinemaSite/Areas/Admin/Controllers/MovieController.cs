using cinemaSite.DataAccess;
using cinemaSite.Models;
using cinemaSite.Repositories;
using cinemaSite.Repositories.IRepositories;
using cinemaSite.Utitlity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace cinemaSite.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]

    public class MovieController : Controller
    {
        //private readonly ApplicationDbContext _movieRepository;
        //public MovieController(ApplicationDbContext context)
        //{
        //    _movieRepository = context;
        //}
        private Repository<Category> _categoryRepository = new();
        private Repository<Cinema> _cinemaRepository = new();
        private Repository<Movie> _movieRepository = new();

        private readonly IRepository<Movie> _moviesRepository;
        private readonly IRepository<Cinema> _cinemasRepository;
        private readonly IRepository<Category> _catRepository;

        public MovieController(IRepository<Movie> movieRepository, IRepository<Category> catRepository, IRepository<Cinema> cinemasRepository)
        {
            _moviesRepository = movieRepository;
            _catRepository = catRepository;
            _cinemasRepository = cinemasRepository;
        }
        public async Task<IActionResult> Index()
        {
            var movies = await _movieRepository.GetAsync(includes: [e => e.Cinema, e => e.Category]);

            return View(movies);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var cinemas = await _cinemaRepository.GetAsync();
            var categories = (await _categoryRepository.GetAsync()).Select(e => new SelectListItem()
            {
                Value = e.Id.ToString(),
                Text = e.Name
            });

            return View(new cinemawithcategoryVM() { 
                categories = categories.ToList(), 
                cinemas = cinemas.ToList() }
            );


            
        }


        [HttpPost]

        public async Task<IActionResult> Create(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }
            await _movieRepository.CreateAsync(movie);
           await _movieRepository.CommitAsync();

            TempData["save"] = "Movie has been saved successfully";
             return RedirectToAction("Index");



        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _movieRepository.GetOneAsync(m => m.Id == id);
            if (movie == null) return NotFound();

            var cinemas = await _cinemaRepository.GetAsync();
            var categories = (await _categoryRepository.GetAsync()).Select(e => new SelectListItem()
            {
                Value = e.Id.ToString(),
                Text = e.Name
            });

            return View(new cinemawithcategoryVM()
            {
                categories = categories.ToList(),
                cinemas = cinemas.ToList(),
                movie = movie
            }
            );

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }
            _movieRepository.Update(movie);
            await _movieRepository.CommitAsync();
            TempData["edit"] = "Movie has been updated successfully";
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie =await _movieRepository.GetOneAsync(m=>m.Id==id);
            if (movie == null) return NotFound();

            _movieRepository.Delete(movie);
            await _movieRepository.CommitAsync();
            TempData["delete"] = "Movie has been deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
