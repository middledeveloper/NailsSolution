using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _environment = environment;
        }

        public string Username { get; set; }
        public string UserId { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public List<Region> Regions { get; set; }
        public List<City> Cities { get; set; }
        public string Photo { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Имя")]
            public string FirstName { get; set; }
            [Display(Name = "Фамилия")]
            public string LastName { get; set; }
            [Display(Name = "Регион")]
            public int RegionId { get; set; }
            [Display(Name = "Город")]
            public int CityId { get; set; }
            [Phone]
            [Display(Name = "Телефон")]
            public string PhoneNumber { get; set; }
            [Display(Name = "Фото")]
            public IFormFile PhotoImage { get; set; }
        }

        private async Task LoadAsync(User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Regions = _context.Regions.ToList();
            Cities = _context.Cities.ToList();

            Username = userName;
            UserId = user.Id;
            Photo = user.Photo;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RegionId = user.RegionId,
                CityId = user.CityId
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Не найден пользователь с ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Не найден пользователь с ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Возникла непредвиденная ошибка при указании номера телефона.";
                    return RedirectToPage();
                }
            }

            if (Input.FirstName != user.FirstName)
            {
                user.FirstName = Input.FirstName;
                await _userManager.UpdateAsync(user);
            }

            if (Input.LastName != user.LastName)
            {
                user.LastName = Input.LastName;
                await _userManager.UpdateAsync(user);
            }

            if (Input.RegionId != user.RegionId)
            {
                user.RegionId = Input.RegionId;
                await _userManager.UpdateAsync(user);
            }

            if (Input.CityId != user.CityId)
            {
                user.CityId = Input.CityId;
                await _userManager.UpdateAsync(user);
            }

            if (Input.PhotoImage != null)
            {
                string filePath = Path.Combine(_environment.WebRootPath, "Photos", $"{user.Id}.jpg");

                if (Photo != null)
                    System.IO.File.Delete(filePath);

                Input.PhotoImage.CopyTo(new FileStream(filePath, FileMode.Create));
                user.Photo = $"{user.Id}.jpg";
                await _userManager.UpdateAsync(user);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Ваш аккаунт обновлён!";
            return RedirectToPage();
        }
    }
}
