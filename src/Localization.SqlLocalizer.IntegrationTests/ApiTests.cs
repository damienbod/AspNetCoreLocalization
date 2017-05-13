using Localization.SqlLocalizer.DbStringLocalizer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreProtobuf.IntegrationTests
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class ApiTests
    {

        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ApiTests()
        {
        }

        [Fact]
        public async Task GetProtobufDataAsString()
        {
            var builder = new WebHostBuilder()
                .ConfigureServices(services =>
                {
                    var sqlConnectionString = Configuration["DbStringLocalizer:ConnectionString"];

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
                        var requestCultureFeature = context.Features.Get<IRequestCultureFeature>();
                        var requestCulture = requestCultureFeature.RequestCulture;
                        Assert.Equal("ar-SA", requestCulture.Culture.Name);
                        return Task.FromResult(0);
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
