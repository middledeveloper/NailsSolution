using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
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
        private readonly IWebHostEnvironment environment;

        public UserDataController(UserManager<User> userManager, ApplicationDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.userManager = userManager;
            this.environment = environment;
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
                model.Authorities = await context.CertificationAuthorities.ToListAsync();

                model.IsMaster = user.IsMaster;
                model.Certificates = user.Certificates.ToList();
                model.ContentBlocks = user.ContentBlocks.ToList();
                model.Portfolio = user.Portfolio.ToList();
                model.Services = user.Services.ToList();
            }

            return View(model);
        }

        public async Task<IActionResult> AddCertificate(int authorityId, IFormFile scanFile)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            var user = await userManager.FindByEmailAsync(userEmail);
            await context.Entry(user).Collection(x => x.Certificates).LoadAsync();

            if (scanFile != null)
            {
                var userCertificatesDirectoryPath = Path.Combine(environment.WebRootPath, "Certificates", user.Id);
                if (!Directory.Exists(userCertificatesDirectoryPath))
                    Directory.CreateDirectory(userCertificatesDirectoryPath);

                if (scanFile.ContentType == "image/jpeg")
                {
                    var filePath = Path.Combine(userCertificatesDirectoryPath, $"{user.Certificates.Count + 1}.jpg");

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        scanFile.CopyTo(stream);
                    }
                }
            }

            var certificate = new Certificate()
            {
                AuthorityId = authorityId,
                Scan = $"/Certificates/{user.Id}/{user.Certificates.Count + 1}.jpg",
                Created = DateTime.Now,
                Updated = DateTime.Now,
                UserId = user.Id
            };

            context.Certificates.Add(certificate);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCertificate(int certificateId)
        {
            var certificate = context.Certificates.First(x => x.Id == certificateId);

            var filePath = Path.Combine(environment.WebRootPath, "Certificates", certificate.UserId, certificate.Scan.Split('/').Last());
            System.IO.File.Delete(filePath);
            context.Certificates.Remove(certificate);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
