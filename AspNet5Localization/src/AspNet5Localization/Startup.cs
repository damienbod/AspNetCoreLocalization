using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Options;

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

            services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization();

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



                        // State what the default culture for your application is. This will be used if no specific culture
                        // can be determined for a given request.
                        options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");

                        // You must explicitly state which cultures your application supports.
                        // These are the cultures the app supports for formatting numbers, dates, etc.
                        options.SupportedCultures = supportedCultures;

                        // These are the cultures the app supports for UI strings, i.e. we have localized resources for.
                        options.SupportedUICultures = supportedCultures;

                        // You can change which providers are configured to determine the culture for requests, or even add a custom
                        // provider with your own logic. The providers will be asked in order to provide a culture for each request,
                        // and the first to provide a non-null result that is in the configured supported cultures list will be used.
                        // By default, the following built-in providers are configured:
                        // - QueryStringRequestCultureProvider, sets culture via "culture" and "ui-culture" query string values, useful for testing
                        // - CookieRequestCultureProvider, sets culture via "ASPNET_CULTURE" cookie
                        // - AcceptLanguageHeaderRequestCultureProvider, sets culture via the "Accept-Language" request header
                        //options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
                        //{
                        //  // My custom request culture logic
                        //  return new ProviderCultureResult("en");
                        //}));
                    });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
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
