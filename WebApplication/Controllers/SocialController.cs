using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Models.ViewModels;

namespace WebApplication.Controllers
{
    public class SocialController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;

        public SocialController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }

        public IActionResult Index()
        {
            SocialViewModel model = new()
            {
                SocialTypes = context.SocialTypes.OrderBy(x => x.Title).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(string socialName, IFormFile logoFile)
        {
            if (!string.IsNullOrEmpty(socialName))
            {
                if (context.SocialTypes.FirstOrDefault(x => x.Title == socialName) == null)
                {
                    var socialType = new SocialType()
                    {
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Title = socialName
                    };

                    if (logoFile != null)
                    {
                        if (logoFile.FileName.EndsWith(".jpg") || logoFile.FileName.EndsWith(".jpeg"))
                        {
                            var filePath = Path.Combine(environment.WebRootPath, "Logos", $"{socialName}.jpg");
                            logoFile.CopyTo(new FileStream(filePath, FileMode.Create));
                            socialType.Logo = $"{socialName}.jpg";
                        }
                    }

                    context.SocialTypes.Add(socialType);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int socialId, string socialName, IFormFile logoFile)
        {
            var socialType = context.SocialTypes.FirstOrDefault(x => x.Id == socialId);
            if (!string.IsNullOrEmpty(socialName) && socialType.Title != socialName)
            {
                socialType.Title = socialName;
                socialType.Updated = DateTime.Now;

                if (logoFile != null)
                {
                    if (logoFile.FileName.EndsWith(".jpg") || logoFile.FileName.EndsWith(".jpeg"))
                    {
                        var filePath = Path.Combine(environment.WebRootPath, "Logos", $"{socialName}.jpg");
                        if (System.IO.File.Exists(filePath))
                            System.IO.File.Delete(filePath);

                        logoFile.CopyTo(new FileStream(filePath, FileMode.Create));
                        socialType.Logo = $"{socialName}.jpg";
                    }
                }

                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
