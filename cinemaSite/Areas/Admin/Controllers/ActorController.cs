using cinemaSite.DataAccess;
using cinemaSite.Models;
using cinemaSite.Repositories.IRepositories;
using cinemaSite.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace cinemaSite.Areas.Admin.Controllers
{
        [Area(SD.AdminArea)]
    public class ActorController : Controller
    {
        //private ApplicationDbContext _context;
        private readonly IRepository<Actor> _actorRepository;
        private IRepository<Actor>? actorRepository;

        //public ActorController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        public ActorController(IRepository<Actor> actorRepository)
        {
            _actorRepository = actorRepository;
        }

        public async Task<IActionResult> Index()
        {
            var actor =await _actorRepository.GetAsync();
            return View(actor.ToList());
        }
        [HttpGet]
        public IActionResult Create( )
        {
            return View( new Actor());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
           await _actorRepository.CreateAsync(actor);
            await _actorRepository.CommitAsync();
            TempData["save"] = "Actor has been saved successfully";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var actor =await _actorRepository.GetOneAsync(e => e.Id == id);

            if (actor is null)
                return NotFound();

            return View(actor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            _actorRepository.Update(actor);
            await _actorRepository.CommitAsync();
            TempData["edit"] = "Actor has been updated successfully";

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var actor = await _actorRepository.GetOneAsync(e => e.Id == id);

            if (actor is null)
                return NotFound();

            _actorRepository.Delete(actor);
           await _actorRepository.CommitAsync();
            TempData["delete"] = "Actor has been deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
