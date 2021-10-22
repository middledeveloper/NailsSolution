using System.Collections.Generic;

namespace WebApplication.Models.ViewModels
{
    public class MasterDataViewModel : UserDataViewModel
    {
        public IList<Certificate> Certificates { get; set; }
        public IList<ContentBlock> ContentBlocks { get; set; }
        public IList<PortfolioImage> Portfolio { get; set; }
        public IList<Service> Services { get; set; }
    }
}
