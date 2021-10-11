using System;

namespace WebApplication.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
