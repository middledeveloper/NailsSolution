using System.Collections.Generic;

namespace WebApplication.Models.ViewModels
{
    public class MasterDataViewModel : UserDataViewModel
    {
        public List<Certificate> Certificates { get; set; }
        public List<ContentBlock> ContentBlocks { get; set; }
        public List<PortfolioImage> Portfolio { get; set; }
        public List<Service> Services { get; set; }
    }
}
