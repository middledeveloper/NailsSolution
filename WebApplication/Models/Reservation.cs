using System;
using System.Collections.Generic;

namespace WebApplication.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string MasterId { get; set; }
        public ICollection<Service> ServiceList { get; set; }
        public int CityId { get; set; }
        public DateTime ReceptionTime { get; set; }
        public bool Completed { get; set; }
        public int RejectReasonId { get; set; }
        public int TestimonialId { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
