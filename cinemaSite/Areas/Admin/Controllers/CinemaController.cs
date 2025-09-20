using cinemaSite.DataAccess;
using cinemaSite.Models;
using cinemaSite.Repositories.IRepositories;
using cinemaSite.Utitlity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace cinemaSite.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]

    public class CinemaController : Controller
    {
        //private readonly ApplicationDbContext _cinemaRepository;

        //public CinemaController(ApplicationDbContext context)
        //{
        //    _cinemaRepository = context;
        //}

        private readonly IRepository<Cinema> _cinemaRepository;
        public CinemaController(IRepository<Cinema> cinemaRepository) => _cinemaRepository = cinemaRepository;
        public async Task<IActionResult> Index()
        {
            var cinemas =await _cinemaRepository.GetAsync();
            return View(cinemas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Cinema());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cinema cinema)
        {
            if (!ModelState.IsValid)
                return View(cinema);

            await _cinemaRepository.CreateAsync(cinema);
           await _cinemaRepository.CommitAsync();
            TempData["save"] = "Cinema has been saved successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var cinema =await _cinemaRepository.GetOneAsync(c=>c.Id==id);
            if (cinema == null) return NotFound();
            return View(cinema);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Cinema cinema)
        {
            if (!ModelState.IsValid)
                return View(cinema);

            _cinemaRepository.Update(cinema);
            await _cinemaRepository.CommitAsync();
            TempData["edit"] = "Cinema has been updated successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinema = await _cinemaRepository.GetOneAsync(c => c.Id == id);
            if (cinema == null) return NotFound();

            _cinemaRepository.Delete(cinema);
            await _cinemaRepository.CommitAsync();
            TempData["delete"] = "Cinema has been deleted successfully";
            return RedirectToAction(nameof(Index));
        }


    }
}
