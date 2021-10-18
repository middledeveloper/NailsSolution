using Microsoft.AspNetCore.Mvc;
using WebApplication.Data;

namespace WebApplication.Controllers
{
    public class CertificateController : Controller
    {
        private readonly ApplicationDbContext context;

        public CertificateController(ApplicationDbContext context)
        {
            this.context = context;
        }
    }
}
