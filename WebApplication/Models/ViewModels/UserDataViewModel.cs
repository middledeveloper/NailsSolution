using Microsoft.AspNetCore.Http;
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
        public bool ActiveOnSaturday { get; set; }
        public bool ActiveOnSunday { get; set; }
        public IList<Certificate> Certificates { get; set; }
        public IList<ContentBlock> ContentBlocks { get; set; }
        public IList<PortfolioImage> Portfolio { get; set; }
        public IList<Service> Services { get; set; }

        public bool IsMaster { get; set; }

        // Certificates form fields
        public IFormFile ScanFile { get; set; }
        public int AuthorityId { get; set; }
        public IList<CertificationAuthority> Authorities { get; set; }

        // Portfolio form fields
        public IFormFile PortfolioFile { get; set; }
        public string PortfolioFileDesc { get; set; }

        // Content block form fields
        public int ContentBlockDomOrder { get; set; }
        public string ContentBlockTitle { get; set; }
        public string ContentBlockContent { get; set; }

        // Socials form fields
        public int SocialTypeId { get; set; }
        public string SocialAccount { get; set; }
        public IList<SocialType> SocialTypes { get; set; }

        // Services form fields
        public int ServiceCategoryId { get; set; }
        public string ServiceTitle { get; set; }
        public decimal ServicePrice { get; set; }
        public IList<ServiceCategory> ServiceCategories { get; set; }
    }
}
