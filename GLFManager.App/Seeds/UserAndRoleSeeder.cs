using GLFManager.Models.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GLFManager.App.Seeds
{
    public class UserAndRoleSeeder
    {
        public static async Task SeedUsersAndRoles(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            var adminRoleExists = await roleManager.RoleExistsAsync("administrator");
            if (!adminRoleExists)
                await roleManager.CreateAsync(new IdentityRole("administrator"));

            var adminFound = await userManager.FindByNameAsync("shanelgmcguire@gmail.com");
            if (adminFound == null)
            {
                var user = new User { Email = "shanelgmcguire@gmail.com", UserName = "shanelgmcguire@gmail.com" };
                IdentityResult result = await userManager.CreateAsync(user, "Password1");

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, "administrator");
                else
                    throw new Exception("An error occured when creating a seeded user");
            }


        }
    }
}
