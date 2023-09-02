using Microsoft.AspNetCore.Identity;

namespace PaleBazaar.GuardianAegis
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            var adminRole = "WizardOverlord";

            if (await roleManager.FindByNameAsync(adminRole) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }

            // TODO: move name and password to secrets

            var adminEmail = "scottfalboart@gmail.com";
            var adminUsername = "Spaceghost";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new IdentityUser
                {
                    UserName = adminUsername,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(adminUser, "Pass!23Pass!23");
                await userManager.AddToRoleAsync(adminUser, adminRole);
            }
        }
    }
}