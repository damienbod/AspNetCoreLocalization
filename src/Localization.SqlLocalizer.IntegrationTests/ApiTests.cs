using Localization.SqlLocalizer.DbStringLocalizer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

namespace Localization.SqlLocalizer.IntegrationTests
{
    public class ApiTests
    {
        [Fact]
        public async Task GetExistingItem()
        {
            var builder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    var sqlConnectionString = "Data Source=.\\LocalizationRecords.sqlite";

                    services.AddDbContext<LocalizationModelContext>(options =>
                        options.UseSqlite(
                            sqlConnectionString,
                            b => b.MigrationsAssembly("AspNetCoreLocalization")
                        )
                    );

                    var useTypeFullNames = false;
                    var useOnlyPropertyNames = false;
                    var returnOnlyKeyIfNotFound = false;
                    var createNewRecordWhenLocalisedStringDoesNotExist = false;


                    // Requires that LocalizationModelContext is defined
                    // _createNewRecordWhenLocalisedStringDoesNotExist read from the dev env. 
                    services.AddSqlLocalization(options => options.UseSettings(
                        useTypeFullNames,
                        useOnlyPropertyNames,
                        returnOnlyKeyIfNotFound,
                        createNewRecordWhenLocalisedStringDoesNotExist));

                    services.AddMvc()
                      .AddViewLocalization()
                      .AddDataAnnotationsLocalization();

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

                })
                .Configure(app =>
                {

                    var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
                    app.UseRequestLocalization(locOptions.Value);

                    app.UseStaticFiles();

                    app.UseMvc();

                    //app.Run(context =>
                    //{
                    //    var response = String.Format("Hello, Universe! It is {0}", DateTime.Now);
                    //    return context.Response.WriteAsync(response);
                    //});
                });

            using (var server = new TestServer(builder))
            {
                var client = server.CreateClient();

                var response = await client.GetStringAsync("api/about?culture=de-CH");
                Assert.Equal("About in German", response);
            }
        }

        [Fact]
        public async Task GetNonExistingItemAllOptionsFalse()
        {
            var builder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    var sqlConnectionString = "Data Source=.\\LocalizationRecords.sqlite";

                    services.AddDbContext<LocalizationModelContext>(options =>
                        options.UseSqlite(
                            sqlConnectionString,
                            b => b.MigrationsAssembly("AspNetCoreLocalization")
                        )
                    );

                    var useTypeFullNames = false;
                    var useOnlyPropertyNames = false;
                    var returnOnlyKeyIfNotFound = false;
                    var createNewRecordWhenLocalisedStringDoesNotExist = false;


                    // Requires that LocalizationModelContext is defined
                    // _createNewRecordWhenLocalisedStringDoesNotExist read from the dev env. 
                    services.AddSqlLocalization(options => options.UseSettings(
                        useTypeFullNames,
                        useOnlyPropertyNames,
                        returnOnlyKeyIfNotFound,
                        createNewRecordWhenLocalisedStringDoesNotExist));

                    services.AddMvc()
                      .AddViewLocalization()
                      .AddDataAnnotationsLocalization();

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

                })
                .Configure(app =>
                {

                    var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
                    app.UseRequestLocalization(locOptions.Value);

                    app.UseStaticFiles();

                    app.UseMvc();

                    //app.Run(context =>
                    //{
                    //    var response = String.Format("Hello, Universe! It is {0}", DateTime.Now);
                    //    return context.Response.WriteAsync(response);
                    //});
                });

            using (var server = new TestServer(builder))
            {
                var client = server.CreateClient();

                var response = await client.GetStringAsync("api/about/non?culture=de-CH");
                Assert.Equal("AboutController.AboutTitleNon.de-CH", response);
            }
        }

        [Fact]
        public async Task GetNonExistingItemUsePropertiesOnly()
        {
            var builder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    var sqlConnectionString = "Data Source=.\\LocalizationRecords.sqlite";

                    services.AddDbContext<LocalizationModelContext>(options =>
                        options.UseSqlite(
                            sqlConnectionString,
                            b => b.MigrationsAssembly("AspNetCoreLocalization")
                        )
                    );

                    var useTypeFullNames = false;
                    var useOnlyPropertyNames = true;
                    var returnOnlyKeyIfNotFound = false;
                    var createNewRecordWhenLocalisedStringDoesNotExist = false;

                    services.AddSqlLocalization(options => options.UseSettings(
                        useTypeFullNames,
                        useOnlyPropertyNames,
                        returnOnlyKeyIfNotFound,
                        createNewRecordWhenLocalisedStringDoesNotExist));

                    services.AddMvc()
                      .AddViewLocalization()
                      .AddDataAnnotationsLocalization();

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

                })
                .Configure(app =>
                {

                    var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
                    app.UseRequestLocalization(locOptions.Value);

                    app.UseStaticFiles();

                    app.UseMvc();

                    //app.Run(context =>
                    //{
                    //    var response = String.Format("Hello, Universe! It is {0}", DateTime.Now);
                    //    return context.Response.WriteAsync(response);
                    //});
                });

            using (var server = new TestServer(builder))
            {
                var client = server.CreateClient();

                var response = await client.GetStringAsync("api/about/non?culture=de-CH");
                Assert.Equal("global.AboutTitleNon.de-CH", response);
            }
        }
    }
}
