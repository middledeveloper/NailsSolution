using System.Collections.Generic;

namespace WebApplication.Models.ViewModels
{
    public class UserDataViewModel
    {
        // User
        public IList<Social> Socials { get; set; }
        public IList<Reservation> Reservations { get; set; }
        public IList<Testimonial> Testimonials { get; set; }

        // Master
        public IList<Certificate> Certificates { get; set; }
        public IList<ContentBlock> ContentBlocks { get; set; }
        public IList<PortfolioImage> Portfolio { get; set; }
        public IList<Service> Services { get; set; }

        public bool IsMaster { get; set; }
    }
}
