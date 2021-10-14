using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using WebApplication.Models;

namespace WebApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<CertificationAhority> CertificationAhorities { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<User> AppUsers { get; set; }
        public DbSet<ContentBlock> ContentBlocks { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<RejectReason> RejectReasons { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<PortfolioImage> PortfolioImages { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<SocialType> SocialTypes { get; set; }
        public DbSet<Social> Socials { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var dt = DateTime.Now;

            var region = new Region() { Id = 1, Title = "Ленинградская область", Created = dt, Updated = dt };
            builder.Entity<Region>().HasData(region);

            var city1 = new City() { Id = 1, Title = "Тихвин", RegionId = 1, Created = dt, Updated = dt };
            var city2 = new City() { Id = 2, Title = "Пикалёво", RegionId = 1, Created = dt, Updated = dt };
            var city3 = new City() { Id = 3, Title = "Бокситогорск", RegionId = 1, Created = dt, Updated = dt };
            builder.Entity<City>().HasData(city1, city2, city3);

            var socialType1 = new SocialType() { Id = 1, Title = "Email", Logo = "/Logos/Email.png", Created = dt, Updated = dt };
            var socialType2 = new SocialType() { Id = 2, Title = "Instagram", Logo = "/Logos/Instagram.png", Created = dt, Updated = dt };
            var socialType3 = new SocialType() { Id = 3, Title = "Whatsapp", Logo = "/Logos/Whatsapp.png", Created = dt, Updated = dt };
            var socialType4 = new SocialType() { Id = 4, Title = "VK", Logo = "/Logos/VK.png", Created = dt, Updated = dt };
            builder.Entity<SocialType>().HasData(socialType1, socialType2, socialType3, socialType4);

            var authority = new CertificationAhority() { Id = 1, Title = "Paris Nail", Url = "https://parisnail.ru/", Created = dt, Updated = dt };
            builder.Entity<CertificationAhority>().HasData(authority);

            var reject1 = new RejectReason() { Id = 1, Title = "Болезнь мастера", Created = dt, Updated = dt };
            var reject2 = new RejectReason() { Id = 2, Title = "Болезнь клиента", Created = dt, Updated = dt };
            var reject3 = new RejectReason() { Id = 3, Title = "Технические проблемы", Created = dt, Updated = dt };
            var reject4 = new RejectReason() { Id = 4, Title = "Клиент не явился", Created = dt, Updated = dt };
            builder.Entity<RejectReason>().HasData(reject1, reject2, reject3, reject4);
        }
    }
}
