using Microsoft.AspNetCore.Identity;
using PaleBazaar.GuardianAegis;
using PaleBazaar.MechanistTower.Configuration;

var builder = WebApplication.CreateBuilder(args);

var configurationSigils = ConfigurationRituals.InvokeConfigurationSigils(builder, builder.Configuration);

ConfigurationRituals.AttuneEnchantments(builder, configurationSigils);

var app = builder.Build();

ConfigurationRituals.ImbueConstruct(app, configurationSigils);

app.Run();

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var config = scope.ServiceProvider.GetRequiredService<IConfigurationSigils>();

    DbInitializer.InitializeAsync(userManager, roleManager, config).Wait();
}