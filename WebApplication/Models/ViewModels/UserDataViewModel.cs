using System.Collections.Generic;

namespace WebApplication.Models.ViewModels
{
    public class UserDataViewModel
    {
        public List<Social> Socials { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<Testimonial> Testimonials { get; set; }
    }
}
