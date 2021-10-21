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
        public IActionResult Create(string rejectReasonName)
        {
            if (!string.IsNullOrEmpty(rejectReasonName))
            {
                if (context.Regions.FirstOrDefault(x => x.Title == rejectReasonName) == null)
                {
                    var reject = new RejectReason()
                    {
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Title = rejectReasonName
                    };

                    context.RejectReasons.Add(reject);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int rejectReasonId, string rejectReasonName)
        {
            var reject = context.RejectReasons.FirstOrDefault(x => x.Id == rejectReasonId);
            if (!string.IsNullOrEmpty(rejectReasonName) && reject.Title != rejectReasonName)
            {
                reject.Title = rejectReasonName;
                reject.Updated = DateTime.Now;
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
