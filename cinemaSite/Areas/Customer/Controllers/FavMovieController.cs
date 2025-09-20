using cinemaSite.Models;
using cinemaSite.Repositories.IRepositories;
using cinemaSite.Utitlity;
using cinemaSite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

using System.Threading.Tasks;

namespace ECommerce516.Areas.Customer.Controllers
{
    [Authorize]
    [Area(SD.CustomerArea)]
    public class FavMovieController : Controller
    {
        private readonly IRepository<FavMovie> _favMovieRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public FavMovieController(IRepository<FavMovie> favMovieRepository, UserManager<ApplicationUser> userManager)
        {
            _favMovieRepository = favMovieRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> AddToFav(AddToFavVM  addToFavVM)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return NotFound();

            var fav = await _favMovieRepository.GetOneAsync(e => e.ApplicationUserId == user.Id && e.MovieId == addToFavVM.MovieId);

            if(fav is not null)
               addToFavVM.status=true;
          
            
            await _favMovieRepository.CommitAsync();

            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return NotFound();

            var fav = await _favMovieRepository.GetAsync(e => e.ApplicationUserId == user.Id, includes: [e => e.movie]);


            return View(fav);
        }

        public async Task<IActionResult> DeleteProductFromFav(int movieId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return NotFound();

            var fav = await _favMovieRepository.GetOneAsync(e => e.ApplicationUserId == user.Id && e.MovieId == movieId);

            if (fav is not null)
            {
                _favMovieRepository.Delete(fav);
                await _favMovieRepository.CommitAsync();
            }

            return RedirectToAction("Index");
        }

    }
}
