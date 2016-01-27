using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using AspNet5Localization.DbStringLocalizer;

namespace AspNet5Localization
{
    using System;

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

            var sqlConnectionString = Configuration["DbStringLocalizer:ConnectionString"];

            services.AddEntityFramework()
                .AddSqlite()
                .AddDbContext<LocalizationModelSqliteContext>( 
                    options =>options.UseSqlite(sqlConnectionString));

            services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            services.AddScoped<LanguageActionFilter>();

            services.Configure<RequestLocalizationOptions>(
                options =>
                    {
                        var supportedCultures = new List<CultureInfo>
                        {
                            new CultureInfo("en-US"),
                            new CultureInfo("de-CH"),
                            new CultureInfo("fr-CH"),
                            new CultureInfo("it-CH")
                        };

                        options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");
                        options.SupportedCultures = supportedCultures;
                        options.SupportedUICultures = supportedCultures;
                    });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //try
            //{
            //    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
            //                                 .CreateScope())
            //    {
            //        serviceScope.ServiceProvider.GetService<LocalizationModelSqliteContext>()
            //                    .Database.Migrate();
            //    }
            //}
            //catch(Exception ex)
            //{
            //    string rr = ex.Message;
            //}

            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            var options = new IISPlatformHandlerOptions();
            options.AuthenticationDescriptions.Clear();
            app.UseIISPlatformHandler(options);

            app.UseStaticFiles();

            app.UseMvc();
        }

 
        // Entry point for the application.
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseDefaultConfiguration(args)
                .UseIISPlatformHandlerUrl()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
