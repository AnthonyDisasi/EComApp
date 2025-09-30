using EComApp.Constants;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace EComApp.Data
{
    public class DbDeeder
    {
        public static async Task SeedDefaultData(IServiceProvider service)
        {
            var userManager = service.GetService<UserManager<IdentityUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>(); // Fix: Use RoleManager<IdentityRole>

            //We are adding some roles to db
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

            //Creating admin user
            var adminUser = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };

            var UserInDb = await userManager.FindByEmailAsync(adminUser.Email);
            if (UserInDb is null)
            {
                await userManager.CreateAsync(adminUser, "Admin@123");
                await userManager.AddToRoleAsync(adminUser, Roles.Admin.ToString());

            } 
        } 
    }
}
