using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Views.Home.Components
{
    public class MasterViewComponent
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public MasterViewComponent(
            ApplicationDbContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<string> InvokeAsync(string userId = "")
        {
            var master = userManager.FindByIdAsync(userId);
            return $"Текущее время: {DateTime.Now.ToString("hh:mm:ss")}";
        }
    }
}
