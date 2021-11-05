
|                           | Build                                                                                                                                                       | Localization.SqlLocalizer                                                                                                                                   |
| ------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------ | ----------------------------------------------------------------------------------------------------------------------------------------------------------- |
| .net core                 | [![Build status](https://ci.appveyor.com/api/projects/status/gyychgc7l5g4g5lb?svg=true)](https://ci.appveyor.com/project/damienbod/aspnet5localization)      | [![NuGet Status](http://img.shields.io/nuget/v/Localization.SqlLocalizer.svg?style=flat-square)](https://www.nuget.org/packages/Localization.SqlLocalizer/) |


========================

Documentation: http://localizationsqllocalizer.readthedocs.io/en/latest/


<a href="https://www.nuget.org/packages/Localization.SqlLocalizer/">NuGet</a> | <a href="https://github.com/damienbod/AspNetCoreLocalization/issues">Issues</a> | <a href="https://github.com/damienbod/AspNetCoreLocalization/tree/master/src/Localization.SqlLocalizer">Code</a>


<strong>Basic Usage ASP.NET Core</strong>

Add the NuGet package to the project.csproj file

```
"dependencies": {
        "Localization.SqlLocalizer": "3.1.0",
```

Add the DbContext and use the AddSqlLocalization extension method to add the SQL Localization package.

```
public void ConfigureServices(IServiceCollection services)
{
    // init database for localization
    var sqlConnectionString = Configuration["DbStringLocalizer:ConnectionString"];

    services.AddDbContext<LocalizationModelContext>(options =>
        options.UseSqlite(
            sqlConnectionString,
            b => b.MigrationsAssembly("ImportExportLocalization")
        ),
        ServiceLifetime.Singleton,
        ServiceLifetime.Singleton
    );

    // Requires that LocalizationModelContext is defined
    services.AddSqlLocalization(options => options.UseTypeFullNames = true);

```

Create your database

```
dotnet ef migrations add Localization --context localizationModelContext

dotnet ef database update Localization --context localizationModelContext
```

========================

# ASP.NET Core MVC Localization Example

<ul>
    <li><a href="http://damienbod.com/2015/10/21/asp-net-5-mvc-6-localization/">ASP.NET Core MVC Localization</a></li>
    <li><a href="http://damienbod.com/2015/10/24/using-dataannotations-and-localization-in-asp-net-5-mvc-6/">Using DataAnnotations and Localization in ASP.NET Core MVC </a></li>
    <li><a href="http://damienbod.com/2016/01/29/asp-net-core-1-0-using-sql-localization/">ASP.NET Core using SQL Localization</a></li>
</ul>



