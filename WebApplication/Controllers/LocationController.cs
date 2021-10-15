using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Models.ViewModels;

namespace WebApplication.Controllers
{
    public class LocationController : Controller
    {
        private readonly ApplicationDbContext context;

        public LocationController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            LocationViewModel model = new()
            {
                Regions = context.Regions.ToList(),
                Cities = context.Cities.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateRegion(string regionName)
        {
            if (!string.IsNullOrEmpty(regionName))
            {
                if (context.Regions.FirstOrDefault(x => x.Title == regionName) == null)
                {
                    var region = new Region()
                    {
                        Created = DateTime.Now,
                        Updated = DateTime.Now,
                        Title = regionName
                    };

                    context.Regions.Add(region);
                    context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult CreateCity(int regionId, string cityName)
        {
            if (!string.IsNullOrEmpty(cityName))
            {
                var city = new City()
                {
                    Created = DateTime.Now,
                    Updated = DateTime.Now,
                    RegionId = regionId,
                    Title = cityName
                };

                context.Cities.Add(city);
                context.SaveChanges();
            }

            return View("Index");
        }

        [HttpPost]
        public IActionResult UpdateRegion(int regionId)
        {
            var region = context.Regions.FirstOrDefault(x => x.Id == regionId);
            if (region != null)
            {
                // Do something
                context.SaveChanges();
            }

            return View("Index");
        }

        [HttpPost]
        public IActionResult UpdateCity(int cityId)
        {
            var city = context.Cities.FirstOrDefault(x => x.Id == cityId);
            if (city != null)
            {
                context.SaveChanges();
            }

            return View("Index");
        }
    }
}
