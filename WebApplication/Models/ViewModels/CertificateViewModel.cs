using System.Collections.Generic;

namespace WebApplication.Models.ViewModels
{
    public class CertificateViewModel
    {
        public int AuthorityId { get; set; }
        public string AuthorityName { get; set; }
        public string AuthorityUrl { get; set; }
        public IList<CertificationAuthority> Authorities { get; set; }
    }
}
