using System;

namespace WebApplication.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public string AuthorId { get; set; }
        public string MasterId { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
