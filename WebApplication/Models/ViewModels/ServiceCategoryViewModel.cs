using System.Collections.Generic;

namespace WebApplication.Models.ViewModels
{
    public class ServiceCategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<ServiceCategory> Categories { get; set; }
    }
}
