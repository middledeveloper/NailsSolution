using System.Collections.Generic;

namespace WebApplication.Models.ViewModels
{
    public class RejectViewModel
    {
        public int RejectReasonId { get; set; }
        public string RejectReasonName { get; set; }
        public List<RejectReason> RejectReasons { get; set; }
    }
}
