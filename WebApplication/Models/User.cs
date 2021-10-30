using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
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

        public async Task<User> LoadUserData(ApplicationDbContext context)
        {
            await context.Entry(this).Collection(x => x.Certificates).LoadAsync();
            await context.Entry(this).Collection(x => x.ContentBlocks).LoadAsync();
            await context.Entry(this).Collection(x => x.Portfolio).LoadAsync();
            await context.Entry(this).Collection(x => x.Services).LoadAsync();
            await context.Entry(this).Collection(x => x.Reservations).LoadAsync();
            await context.Entry(this).Collection(x => x.Socials).LoadAsync();
            await context.Entry(this).Collection(x => x.Testimonials).LoadAsync();

            return this;
        }
    }
}
