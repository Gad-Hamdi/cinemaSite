using cinemaSite.Models;
using cinemaSite.Utitlity;
using cinemaSite.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace cinemaSite.Areas.Identity.Controllers
{
    [Area(SD.IdentityArea)]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Welcome()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return NotFound();

            //PersonalInformationVM personalInformationVM = new()
            //{
            //    Email = user.Email,
            //    Id = user.Id,
            //    Name = user.Name,
            //    PhoneNumber = user.PhoneNumber,
            //    State = user.State,
            //    City = user.City,
            //    Street = user.Street,
            //    ZipCode = user.ZipCode
            //};

            PersonalInformationVM personalInformationVM = user.Adapt<PersonalInformationVM>();

            return View(personalInformationVM);
        }

        public async Task<IActionResult> UpdateInfo(PersonalInformationVM personalInformationVM)
        {
            if(!ModelState.IsValid)
            {
                return View(personalInformationVM);
            }

            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return NotFound();

            user.Name = personalInformationVM.Name;
            user.Email = personalInformationVM.Email;
            user.PhoneNumber = personalInformationVM.PhoneNumber;
            user.Street = personalInformationVM.Street;
            user.City = personalInformationVM.City;
            user.State = personalInformationVM.State;
            user.ZipCode = personalInformationVM.ZipCode;

            await _userManager.UpdateAsync(user);

            TempData["success-notification"] = "Update Info Successfully";
            return RedirectToAction("Welcome");
        }

        public async Task<IActionResult> ChangePassword(ChangePasswordVM changePasswordVM)
        {
            if (!ModelState.IsValid)
            {
                return View(changePasswordVM);
            }

            var user = await _userManager.GetUserAsync(User);

            if (user is null)
                return NotFound();

             var result = await _userManager.ChangePasswordAsync(user, changePasswordVM.CurrentPassword, changePasswordVM.NewPassword);

            if (!result.Succeeded)
            {
                TempData["error-notification"] = String.Join(", ", result.Errors.Select(e => e.Description));
            }
            else
            {
                TempData["success-notification"] = "Update Password Successfully";
            }

            return RedirectToAction("Welcome");
        }

    }
}
