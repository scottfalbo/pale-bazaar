using Azure.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PaleBazaar.Areas.Identity;
using PaleBazaar.Data;
using PaleBazaar.MechanistTower.Configuration;

var builder = WebApplication.CreateBuilder(args);

var configurationSigils = ConfigurationRituals.InvokeConfigurationSigils(builder, builder.Configuration);

ConfigurationRituals.AttuneEnchantments(builder, configurationSigils);

var app = builder.Build();

ConfigurationRituals.ImbueConstruct(app);

app.Run();