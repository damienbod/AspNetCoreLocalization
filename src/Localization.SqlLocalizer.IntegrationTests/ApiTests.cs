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

namespace AspNetCoreProtobuf.IntegrationTests
{
    public class ApiTests
    {
        [Fact]
        public async Task HelloWorldTest()
        {
            var builder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    var sqlConnectionString = "Data Source=C:\\git\\damienbod\\AspNetCoreLocalization\\AspNetCoreLocalization\\src\\AspNetCoreLocalization\\LocalizationRecords.sqlite";

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

                })
                .Configure(app =>
                {
                    app.Run(context =>
                    {
                        var response = String.Format("Hello, Universe! It is {0}", DateTime.Now);
                        return context.Response.WriteAsync(response);
                    });
                });

            using (var server = new TestServer(builder))
            {
                var client = server.CreateClient();
                client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("jp,ar-SA,en-US");
                var count = client.DefaultRequestHeaders.AcceptLanguage.Count;
                var response = await client.GetAsync(string.Empty);
                Assert.Equal(3, count);
            }
        }

    }
}
