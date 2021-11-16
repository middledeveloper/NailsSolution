using System;

namespace WebApplication.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Path { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
