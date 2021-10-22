using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Models.ViewModels;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;

        public HomeController(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                //var user = await userManager.FindByIdAsync("d89a9858-d74e-4da7-8262-60f35a8452ed");
                //var userRoles = await userManager.GetRolesAsync(user);

                //user.LoadUserCollections(context, userRoles);
            }
            else
            {

            };


            //var model = new UserMainPageViewModel()
            //{
            //    Name = $"{user.FirstName} {user.LastName}",
            //    Location = $"{context.Regions.FirstOrDefault(x => x.Id == user.RegionId).Title}, {context.Cities.FirstOrDefault(x => x.Id == user.CityId).Title}",
            //    Photo = user.Photo,
            //    ContentBlocks = user.ContentBlocks.ToList(),
            //    Portfolio = user.Portfolio.ToList(),
            //    Services = user.Services.ToList(),
            //    Certificates = user.Certificates.ToList(),
            //    Socials = user.Socials.ToList()
            //};

            Log.Information("Home:Index data collected successfully");

            return View();
        }

        [Authorize]
        public IActionResult Timetable()
        {
            return View("Index");
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
