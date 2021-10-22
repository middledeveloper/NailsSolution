using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Models.ViewModels;

namespace WebApplication.Controllers
{
    public class RejectController : Controller
    {
        private readonly ApplicationDbContext context;

        public RejectController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            RejectViewModel model = new()
            {
                RejectReasons = context.RejectReasons.OrderBy(x => x.Title).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(string rejectName)
        {
            if (!string.IsNullOrEmpty(rejectName))
            {
                if (context.Regions.FirstOrDefault(x => x.Title == rejectName) == null)
                {
                    var reject = new RejectReason()
                    {
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Title = rejectName
                    };

                    context.RejectReasons.Add(reject);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int rejectId, string rejectName)
        {
            var reject = context.RejectReasons.FirstOrDefault(x => x.Id == rejectId);
            if (!string.IsNullOrEmpty(rejectName) && reject.Title != rejectName)
            {
                reject.Title = rejectName;
                reject.Updated = DateTime.Now;
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
