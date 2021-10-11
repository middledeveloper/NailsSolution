using System;

namespace WebApplication.Models
{
    public class Social
    {
        public int Id { get; set; }
        public string UserId { get; internal set; }
        public int SocialTypeId { get; internal set; }
        public string Account { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
