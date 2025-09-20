using cinemaSite.DataAccess;
using cinemaSite.Models;
using cinemaSite.Repositories.IRepositories;
using cinemaSite.Utitlity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace cinemaSite.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]

    public class CategoryController : Controller
    {

        //private readonly ApplicationDbContext _context;


        //public ActorController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}
        private readonly IRepository<Category> _categoryRepository;
        public CategoryController(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            var categories =await _categoryRepository.GetAsync();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Category());
        }
        [HttpPost]

        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
           await  _categoryRepository.CreateAsync(category);
          await  _categoryRepository.CommitAsync();
            TempData["save"] = "Category has been saved successfully";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryRepository.GetOneAsync(c => c.Id == id);
            if (category is null) return NotFound();
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _categoryRepository.Update(category);
            await _categoryRepository.CommitAsync();
            TempData["edit"] = "Category has been updated successfully";
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var category =await _categoryRepository.GetOneAsync(c => c.Id == id);
            if (category == null) return NotFound();

            _categoryRepository.Delete(category);
            await _categoryRepository.CommitAsync();

            TempData["delete"] = "Category has been deleted successfully";


            return RedirectToAction(nameof(Index));
        }


    }
}
