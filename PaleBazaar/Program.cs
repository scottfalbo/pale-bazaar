using Microsoft.AspNetCore.Identity;
using PaleBazaar.GuardianAegis;
using PaleBazaar.MechanistTower.Configuration;

var builder = WebApplication.CreateBuilder(args);

var configurationSigils = ConfigurationRituals.InvokeConfigurationSigils(builder, builder.Configuration);

ConfigurationRituals.AttuneEnchantments(builder, configurationSigils);

var app = builder.Build();

ConfigurationRituals.ImbueConstruct(app, configurationSigils);

app.Run();