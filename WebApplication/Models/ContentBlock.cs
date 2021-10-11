using System;

namespace WebApplication.Models
{
    public class ContentBlock
    {
        public int Id { get; set; }
        public string UserId { get; internal set; }
        public int DomOrder { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
