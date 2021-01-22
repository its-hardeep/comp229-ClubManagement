using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace C229_G4.Models
{
    public static class IdentitySeedData
    {
        private const string adminUser = "Admin";
        private const string adminPassword = "Admin123$";

        private const string generalUser = "User";
        private const string generalPassword = "User123$";
        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            UserManager<IdentityUser> userManager = app.ApplicationServices.GetRequiredService<UserManager<IdentityUser>>();
            RoleManager<IdentityRole> roleManager = app.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>();
            IdentityUser admin = await userManager.FindByIdAsync(adminUser);
            IdentityRole role = new IdentityRole("R_Admin");
            await roleManager.CreateAsync(role);
            if (admin == null)
            {
                admin = new IdentityUser("Admin");
                await userManager.CreateAsync(admin, adminPassword);
                await userManager.AddToRoleAsync(admin, "R_Admin");
            }
            IdentityUser general = await userManager.FindByIdAsync(generalUser);
            if (general == null)
            {
                general = new IdentityUser("User");
                await userManager.CreateAsync(general, generalPassword);
            }
        }
    }
}
