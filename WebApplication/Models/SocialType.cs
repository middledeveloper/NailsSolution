using System;

namespace WebApplication.Models
{
    public class SocialType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Logo { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
