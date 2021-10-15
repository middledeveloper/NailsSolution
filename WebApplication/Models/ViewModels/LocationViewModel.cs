using System.Collections.Generic;

namespace WebApplication.Models.ViewModels
{
    public class LocationViewModel
    {
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public List<Region> Regions { get; set; }
        public List<City> Cities { get; set; }
    }
}
