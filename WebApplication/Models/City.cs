using System;

namespace WebApplication.Models
{
    public class City
    {
        public int Id { get; set; }
        public int RegionId { get; internal set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
