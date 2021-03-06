using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<City> Cities { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
