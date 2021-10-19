using System;

namespace WebApplication.Models
{
    public class CertificationAuthority
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
