using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace WebApplication.Models.ViewModels
{
    public class SocialViewModel
    {
        public int SocialId { get; set; }
        public string SocialName { get; set; }
        public string SocialLogo { get; set; }
        public IFormFile LogoFile { get; set; }
        public List<SocialType> SocialTypes { get; set; }
    }
}
