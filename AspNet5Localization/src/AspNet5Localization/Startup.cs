using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Localization;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNet5Localization
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            services.AddScoped<LanguageActionFilter>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.MinimumLevel = LogLevel.Information;
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            var requestLocalizationOptions = new RequestLocalizationOptions
            {  
                SupportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("de-CH"),
                    new CultureInfo("fr-CH"),

                    new CultureInfo("it-CH")
                },
                SupportedUICultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("de-CH"),
                    new CultureInfo("fr-CH"),
                    new CultureInfo("it-CH")
                }
            };

            app.UseRequestLocalization(requestLocalizationOptions,  new RequestCulture("en-US"));
            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
