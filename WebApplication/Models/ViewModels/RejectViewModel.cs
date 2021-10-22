using System.Collections.Generic;

namespace WebApplication.Models.ViewModels
{
    public class RejectViewModel
    {
        public int RejectId { get; set; }
        public string RejectName { get; set; }
        public List<RejectReason> RejectReasons { get; set; }
    }
}
