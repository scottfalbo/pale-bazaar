using Azure.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PaleBazaar.Areas.Identity;
using PaleBazaar.Data;
using PaleBazaar.GuardianAegis;
using PaleBazaar.MechanistTower.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<IConfigurationSigils, ConfigurationSigils>();

builder.Configuration.AddEnvironmentVariables()
    .AddCommandLine(args)
    .AddUserSecrets<Program>();

string keyVaultUri = builder.Configuration["KeyVaultUri"];

var clientId = builder.Configuration["Azure:ClientId"];
var clientSecret = builder.Configuration["Azure:ClientSecret"];
var tenantId = builder.Configuration["Azure:TenantId"];

builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUri), new ClientSecretCredential(tenantId, clientId, clientSecret));

var app = builder.Build();

ConfigurationRituals.ImbueConstruct(app);

app.Run();