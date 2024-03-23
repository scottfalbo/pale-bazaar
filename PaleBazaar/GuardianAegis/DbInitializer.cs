// ------------------------------------
// The Pale Bazaar
// ------------------------------------

using Microsoft.AspNetCore.Identity;
using PaleBazaar.MechanistTower.Configuration;

namespace PaleBazaar.GuardianAegis;

public static class DbInitializer
{
    public static async Task InitializeAsync(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfigurationSigils configurationSigils)
    {
        var adminRole = "WizardOverlord";

        if (await roleManager.FindByNameAsync(adminRole) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(adminRole));
        }

        var adminEmail = configurationSigils.AdminEmail;
        var adminUsername = configurationSigils.AdminUserName;

        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new IdentityUser
            {
                UserName = adminUsername,
                Email = adminEmail,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(adminUser, configurationSigils.AdminPassword);
            await userManager.AddToRoleAsync(adminUser, adminRole);
        }
    }
}