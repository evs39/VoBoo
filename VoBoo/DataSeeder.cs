using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using VoBoo.Data;
using VoBoo.Models;

namespace VoBoo
{
	public class DataSeeder
	{
		public static async Task SeedAdmin(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
		{
            var admin = await userManager.FindByNameAsync("Admin");
            if (admin == null)
            {
                admin = new ApplicationUser()
                {
                    Email = "admin@voboo.com",
                    UserName = "Admin",
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(admin, "admin12345");
            }

            if (admin.Id == null)
                throw new Exception("Seeding users to db exception");

            var adminRole = await roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                adminRole = new ApplicationRole()
                {
                    Name = "Admin"
                };
                await roleManager.CreateAsync(adminRole);
            }

            if (adminRole.Id == null)
                throw new Exception("Seeding roles to db exception");

            if (! (await userManager.IsInRoleAsync(admin, adminRole.Name)))
                await userManager.AddToRoleAsync(admin, adminRole.Name);
        }
    }
}
