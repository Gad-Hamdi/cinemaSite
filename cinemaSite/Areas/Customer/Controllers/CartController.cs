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
    public class CartController : Controller
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(IRepository<Cart> cartRepository, UserManager<ApplicationUser> userManager)
        {
            _cartRepository = cartRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> AddToCart(AddToCartVM addToCartVM)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return NotFound();

            var cart = await _cartRepository.GetOneAsync(e => e.ApplicationUserId == user.Id && e.MovieId == addToCartVM.MovieId);

            if(cart is not null)
            {
                cart.Count += addToCartVM.Count;
                TempData["success-notification"] = "Update To Cart Successfully";
            }
            else
            {
                await _cartRepository.CreateAsync(new()
                {
                    Count = addToCartVM.Count,
                    MovieId = addToCartVM.MovieId,
                    ApplicationUserId = user.Id
                });
                TempData["success-notification"] = "Add To Cart Successfully";
            }

            await _cartRepository.CommitAsync();

            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return NotFound();

            var carts = await _cartRepository.GetAsync(e => e.ApplicationUserId == user.Id, includes: [e => e.movie]);

            ViewBag.totalAmount = carts.Sum(e => e.movie.Price * e.Count);

            return View(carts);
        }

        public async Task<IActionResult> IncrementCount(int MovieId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return NotFound();

            var cart = await _cartRepository.GetOneAsync(e => e.ApplicationUserId == user.Id && e.MovieId == MovieId);
            
            if(cart is not null)
            {
                cart.Count += 1;
                await _cartRepository.CommitAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DecrementCount(int MovieId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return NotFound();

            var cart = await _cartRepository.GetOneAsync(e => e.ApplicationUserId == user.Id && e.MovieId == MovieId);

            if (cart is not null)
            {
                if(cart.Count> 1)
                {
                    cart.Count -= 1;
                    await _cartRepository.CommitAsync();
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteProductFromCart(int MovieId)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return NotFound();

            var cart = await _cartRepository.GetOneAsync(e => e.ApplicationUserId == user.Id && e.MovieId == MovieId);

            if (cart is not null)
            {
                _cartRepository.Delete(cart);
                await _cartRepository.CommitAsync();
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Pay()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return NotFound();

            var carts = await _cartRepository.GetAsync(e => e.ApplicationUserId == user.Id, includes: [e => e.movie]);

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = $"{Request.Scheme}://{Request.Host}/checkout/success",
                CancelUrl = $"{Request.Scheme}://{Request.Host}/checkout/cancel",
            };

            foreach (var item in carts)
            {
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "egp",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.movie.Name,
                            Description = item.movie.Description,
                        },
                        UnitAmount = (long)item.movie.Price * 100,
                    },
                    Quantity = item.Count,
                });
            }

            var service = new SessionService();
            var session = service.Create(options);
            return Redirect(session.Url);
        }
    }
}
