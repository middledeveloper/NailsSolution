using System;

namespace WebApplication.Models
{
    public class Certificate
    {
        public int Id { get; set; }
        public string UserId { get; internal set; }
        public int AuthorityId { get; internal set; }
        public string Scan { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
