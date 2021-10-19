using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Models.ViewModels;

namespace WebApplication.Controllers
{
    public class CertificateController : Controller
    {
        private readonly ApplicationDbContext context;

        public CertificateController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            CertificateViewModel model = new()
            {
                Authorities = context.CertificationAuthorities.OrderBy(x => x.Title).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateAuthorityCenter(string authorityName, string authorityUrl)
        {
            if (!string.IsNullOrEmpty(authorityName))
            {
                if (context.Regions.FirstOrDefault(x => x.Title == authorityName) == null)
                {
                    var authority = new CertificationAuthority()
                    {
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Title = authorityName,
                        Url = authorityUrl
                    };

                    context.CertificationAuthorities.Add(authority);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateAuthorityCenter(int authorityId, string authorityName)
        {
            var authority = context.CertificationAuthorities.FirstOrDefault(x => x.Id == authorityId);
            if (!string.IsNullOrEmpty(authorityName) && authority.Title != authorityName)
            {
                authority.Title = authorityName;
                authority.Updated = DateTime.Now;
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
