using System.Collections.Generic;

namespace WebApplication.Models.ViewModels
{
    public class UserMainPageViewModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Photo { get; set; }
        public List<ContentBlock> ContentBlocks { get; set; }
        public List<PortfolioImage> Portfolio { get; set; }
        public List<Service> Services { get; set; }
        public List<Social> Socials { get; set; }
        public List<Certificate> Certificates { get; set; }

    }
}
