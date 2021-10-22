using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Models.ViewModels;

namespace WebApplication.Controllers
{
    public class ServiceCategoryController : Controller
    {
        private readonly ApplicationDbContext context;

        public ServiceCategoryController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            ServiceCategoryViewModel model = new()
            {
                Categories = context.ServiceCategories.OrderBy(x => x.Title).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(string categoryName)
        {
            if (!string.IsNullOrEmpty(categoryName))
            {
                if (context.ServiceCategories.FirstOrDefault(x => x.Title == categoryName) == null)
                {
                    var category = new ServiceCategory()
                    {
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Title = categoryName
                    };

                    context.ServiceCategories.Add(category);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int categoryId, string categoryName)
        {
            var category = context.ServiceCategories.FirstOrDefault(x => x.Id == categoryId);
            if (!string.IsNullOrEmpty(categoryName) && category.Title != categoryName)
            {
                category.Title = categoryName;
                category.Updated = DateTime.Now;
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
