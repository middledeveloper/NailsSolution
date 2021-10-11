using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public User Master { get; set; }
        public ICollection<Service> ServiceList { get; set; }
        public City City { get; set; }
        public DateTime ReceptionTime { get; set; }
        public bool Completed { get; set; }
        public RejectReason RejectReason { get; set; }
        public Testimonial Testimonial { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
