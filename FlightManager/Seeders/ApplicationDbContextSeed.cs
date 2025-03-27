using FlightManager.Data.Entities.Identity;
using FlightManager.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;

namespace FlightManager.Seeders
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(
           ApplicationDbContext dbContext,
           UserManager<ApplicationUser> userManager,
           RoleManager<IdentityRole> roleManager)
        {
            await SeedUsersAsync(userManager, roleManager);
        }

        private static async Task SeedUsersAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                IdentityRole admin = new IdentityRole(Constants.ADMINISTRATOR_ROLE);
                IdentityRole employee = new IdentityRole(Constants.EMPLOYEE_ROLE);

                await roleManager.CreateAsync(admin);
                await roleManager.CreateAsync(employee);
            }
        }
    }
}
