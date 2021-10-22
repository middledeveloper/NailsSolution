using System.Collections.Generic;

namespace WebApplication.Models.ViewModels
{
    public class UserDataViewModel
    {
        public IList<Social> Socials { get; set; }
        public IList<Reservation> Reservations { get; set; }
        public IList<Testimonial> Testimonials { get; set; }
    }
}
