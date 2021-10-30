using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Models.ViewModels;
using WebApplication.Utility;

namespace WebApplication.Controllers
{
    public class UserDataController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;

        public UserDataController(UserManager<User> userManager, ApplicationDbContext context)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            var user = await userManager.FindByEmailAsync(userEmail);
            await user.LoadUserData(context);

            var userRoles = await userManager.GetRolesAsync(user);

            var model = new UserDataViewModel
            {
                Socials = user.Socials.ToList(),
                Testimonials = user.Testimonials.ToList(),
                Reservations = user.Reservations.ToList()
            };

            if (userRoles.Contains(RolesEnum.MasterRole))
            {
                model.IsMaster = user.IsMaster;
                model.Certificates = user.Certificates.ToList();
                model.ContentBlocks = user.ContentBlocks.ToList();
                model.Portfolio = user.Portfolio.ToList();
                model.Services = user.Services.ToList();
            }

            return View(model);
        }
    }
}
