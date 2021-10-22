using System.Collections.Generic;

namespace WebApplication.Models.ViewModels
{
    public class MasterPageViewModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Photo { get; set; }
        public IList<ContentBlock> ContentBlocks { get; set; }
        public IList<PortfolioImage> Portfolio { get; set; }
        public IList<Service> Services { get; set; }
        public IList<Social> Socials { get; set; }
        public IList<Certificate> Certificates { get; set; }

    }
}
