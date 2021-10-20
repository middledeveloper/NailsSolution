using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
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
                        if (logoFile.ContentType == "image/png")
                        {
                            var filePath = Path.Combine(environment.WebRootPath, "Logos", $"{socialName}.png");

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                logoFile.CopyTo(stream);
                            }

                            socialType.Logo = $"/Logos/{socialName}.png";
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
            var updated = false;

            if (!string.IsNullOrEmpty(socialName) && socialType.Title != socialName)
            {
                socialType.Title = socialName;
                updated = true;
            }

            if (logoFile != null)
            {
                if (logoFile.ContentType == "image/png")
                {
                    var oldFilePath = Path.Combine(environment.WebRootPath, "Logos", $"{socialType.Title}.png");
                    var newFilePath = Path.Combine(environment.WebRootPath, "Logos", $"{socialName}.png");

                    if (System.IO.File.Exists(oldFilePath))
                        System.IO.File.Delete(oldFilePath);

                    var resultPath = String.IsNullOrEmpty(socialName) ? oldFilePath : newFilePath;
                    using (var stream = new FileStream(resultPath, FileMode.Create))
                    {
                        logoFile.CopyTo(stream);
                    }
                    socialType.Logo = String.IsNullOrEmpty(socialName) ?
                        $"/Logos/{socialType.Title}.png" :
                        $"/Logos/{socialName}.png";

                    updated = true;
                }
            }

            if (updated)
            {
                socialType.Updated = DateTime.Now;
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
