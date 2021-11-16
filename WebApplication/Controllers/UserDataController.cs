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

        private async Task<User> DefineCurrentUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Name);
            return await userManager.FindByEmailAsync(userEmail);
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await DefineCurrentUser();
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
                model.ActiveOnSaturday = user.ActiveOnSaturday;
                model.ActiveOnSunday = user.ActiveOnSunday;
                model.Authorities = await context.CertificationAuthorities.ToListAsync();
                model.IsMaster = true;
                model.Certificates = user.Certificates.ToList();
                model.ContentBlocks = user.ContentBlocks.ToList();
                model.Portfolio = user.Portfolio.ToList();
                model.Services = user.Services.ToList();
            }

            model.SocialTypes = context.SocialTypes.ToList();
            return View(model);
        }

        public async Task<IActionResult> AddCertificate(int authorityId, IFormFile scanFile)
        {
            var user = await DefineCurrentUser();
            await context.Entry(user).Collection(x => x.Certificates).LoadAsync();

            if (scanFile != null)
            {
                var directoryPath = Path.Combine(environment.WebRootPath, "Certificates", user.Id);
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                if (scanFile.ContentType == "image/jpeg")
                {
                    var filePath = Path.Combine(directoryPath, $"{user.Certificates.Count + 1}.jpg");

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
            var certificate = await context.Certificates.FirstAsync(x => x.Id == certificateId);

            var filePath = Path.Combine(environment.WebRootPath, "Certificates", certificate.UserId, certificate.Scan.Split('/').Last());
            System.IO.File.Delete(filePath);
            context.Certificates.Remove(certificate);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddPortfolioImage(string portfolioFileDesc, IFormFile portfolioFile)
        {
            var user = await DefineCurrentUser();
            await context.Entry(user).Collection(x => x.Portfolio).LoadAsync();

            if (portfolioFile != null)
            {
                var directoryPath = Path.Combine(environment.WebRootPath, "Portfolio", user.Id);
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                if (portfolioFile.ContentType == "image/jpeg")
                {
                    var filePath = Path.Combine(directoryPath, $"{user.Portfolio.Count + 1}.jpg");

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        portfolioFile.CopyTo(stream);
                    }
                }
            }

            var portfolioImage = new PortfolioImage()
            {
                Path = $"/Portfolio/{user.Id}/{user.Portfolio.Count + 1}.jpg",
                Desc = portfolioFileDesc == null ? string.Empty : portfolioFileDesc,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                UserId = user.Id
            };

            context.PortfolioImages.Add(portfolioImage);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeletePortfolioImage(int portfolioImageId)
        {
            var portfolioImage = await context.PortfolioImages.FirstAsync(x => x.Id == portfolioImageId);

            var filePath = Path.Combine(environment.WebRootPath, "Portfolio", portfolioImage.UserId, portfolioImage.Path.Split('/').Last());
            System.IO.File.Delete(filePath);
            context.PortfolioImages.Remove(portfolioImage);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddContentBlock(string contentBlockTitle, int contentBlockDomOrder, string contentBlockContent)
        {
            var user = await DefineCurrentUser();
            await context.Entry(user).Collection(x => x.Portfolio).LoadAsync();

            var contentBlock = new ContentBlock()
            {
                Title = contentBlockTitle,
                DomOrder = contentBlockDomOrder,
                Content = contentBlockContent,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                UserId = user.Id
            };

            context.ContentBlocks.Add(contentBlock);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteContentBlock(int contentBlockId)
        {
            var contentBlock = await context.ContentBlocks.FirstAsync(x => x.Id == contentBlockId);

            context.ContentBlocks.Remove(contentBlock);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddSocial(int socialTypeId, string socialAccount)
        {
            var user = await DefineCurrentUser();
            await context.Entry(user).Collection(x => x.Socials).LoadAsync();

            var social = new Social()
            {
                SocialTypeId = socialTypeId,
                Account = socialAccount,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                UserId = user.Id
            };

            context.Socials.Add(social);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteSocial(int socialId)
        {
            var social = await context.Socials.FirstAsync(x => x.Id == socialId);

            context.Socials.Remove(social);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> UpdateActiveDays(bool activeOnSaturday, bool activeOnSunday)
        {
            var user = await DefineCurrentUser();
            user.ActiveOnSaturday = activeOnSaturday;
            user.ActiveOnSunday = activeOnSunday;

            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
