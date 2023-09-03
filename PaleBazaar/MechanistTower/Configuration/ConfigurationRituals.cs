using Azure.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using PaleBazaar.Areas.Identity;
using PaleBazaar.Data;
using PaleBazaar.GuardianAegis;
using PaleBazaar.MechanistTower.Manipulators;
using PaleBazaar.MechanistTower.SpellChanters;
using PaleBazaar.MechanistTower.Tomes;

namespace PaleBazaar.MechanistTower.Configuration
{
    public static class ConfigurationRituals
    {
        public static ConfigurationSigils InvokeConfigurationSigils(
            WebApplicationBuilder builder,
            IConfiguration configuration)
        {
            var keyVaultUri = configuration["KeyVaultUri"];

            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets(builder.Environment.ApplicationName);

            var clientId = configuration["Azure:ClientId"];
            var clientSecret = configuration["Azure:ClientSecret"];
            var tenantId = configuration["Azure:TenantId"];

            builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUri), new ClientSecretCredential(tenantId, clientId, clientSecret));

            var configurationSigils = new ConfigurationSigils();
            builder.Configuration.Bind(configurationSigils);

            builder.Services.AddSingleton<IConfigurationSigils>(configurationSigils);
            return configurationSigils;
        }

        public static void AttuneEnchantments(
            WebApplicationBuilder builder,
            IConfigurationSigils configurationSigils)
        {
            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            var cosmosEndpoint = configurationSigils.CosmosEndpoint;
            var cosmosKey = configurationSigils.CosmosKey;
            var cosmosClient = new CosmosClient(cosmosEndpoint, cosmosKey);

            builder.Services.AddSingleton<ICosmosTomeScryer>(
                new CosmosTomeScryer(cosmosClient));

            builder.Services.AddAzureClients(builder =>
            {
                builder.AddBlobServiceClient(configurationSigils.BlobConnectionString);
            });

            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            builder.Services.AddServerSideBlazor();

            builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

            builder.Services.AddScoped<IFleshRiteChanters, FleshRiteChanters>();
            builder.Services.AddScoped<IFleshRitesTome, FleshRitesTome>();
            builder.Services.AddScoped<IIllustrationChanters, IllustrationChanters>();
            builder.Services.AddScoped<IIllustrationsTome, IllustrationsTome>();
            builder.Services.AddScoped<IEchoKeeperChanter, EchoKeeperChanter>();
            builder.Services.AddScoped<IEchoShaper, EchoShaper>();
        }

        public static void ImbueConstruct(
            WebApplication app,
            IConfigurationSigils configurationSigils)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                DbInitializer.InitializeAsync(userManager, roleManager, configurationSigils).Wait();
            }

            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");
        }
    }
}