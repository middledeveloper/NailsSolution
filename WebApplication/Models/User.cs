using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication.Data;

namespace WebApplication.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
        public int CityId { get; internal set; }
        public int RegionId { get; internal set; }
        public ICollection<Social> Socials { get; set; }
        public ICollection<Service> Services { get; set; }
        public ICollection<Certificate> Certificates { get; set; }
        public ICollection<PortfolioImage> Portfolio { get; set; }
        public ICollection<ContentBlock> ContentBlocks { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Testimonial> Testimonials { get; set; }
        public int ExperienceYears { get; set; }
        public bool ActiveOnSaturday { get; set; }
        public bool ActiveOnSunday { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        [NotMapped]
        public bool IsMaster { get; set; }

        public User LoadUserCollections(ApplicationDbContext context, IEnumerable<string> roles)
        {
            //if (roles.FirstOrDefault(x => x.ToLower() == "master") != null ||
            //    roles.FirstOrDefault(x => x.ToLower() == "admin") != null)
            //{
                context.Entry(this).Collection(x => x.Certificates).Load();
                context.Entry(this).Collection(x => x.ContentBlocks).Load();
                context.Entry(this).Collection(x => x.Portfolio).Load();
                context.Entry(this).Collection(x => x.Services).Load();
            //}

            context.Entry(this).Collection(x => x.Reservations).Load();
            context.Entry(this).Collection(x => x.Socials).Load();

            return this;
        }
    }
}
