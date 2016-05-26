Localization.SqlLocalizer [![NuGet Status](http://img.shields.io/nuget/v/Localization.SqlLocalizer.svg?style=flat-square)](https://www.nuget.org/packages/Localization.SqlLocalizer/)
========================
<strong>Documentation:</strong>
 https://damienbod.wordpress.com

<a href="https://www.nuget.org/packages/Localization.SqlLocalizer/">NuGet</a> | <a href="https://github.com/damienbod/AspNet5Localization/issues">Issues</a> | <a href="https://github.com/damienbod/AspNet5Localization/tree/master/AspNet5Localization/src/Localization.SqlLocalizer">Code</a>

<strong>Release History</strong>

<em>Version 1.0.0.0</em>
<ul>
 	<li>Initial release</li>
	<li>Runtime localization updates</li>
	<li>Cache support, reset cache</li>
        <li>ASP.NET DI support</li>
        <li>Supports any Entity Framework Core database</li>

</ul>

<strong>Basic Usage ASP.NET Core</strong>

Add the NuGet package to the project.json file

[code language="csharp"]
"dependencies": {
        "Localization.SqlLocalizer": "1.0.0.0",
[/code]

Add the DbContext and use the AddSqlLocalization extension method to add the SQL Localization package.

[code language="csharp"]
public void ConfigureServices(IServiceCollection services)
{
	// init database for localization
	var sqlConnectionString = Configuration["DbStringLocalizer:ConnectionString"];

	services.AddDbContext<LocalizationModelContext>(options =>
		options.UseSqlite(
			sqlConnectionString,
			b => b.MigrationsAssembly("Angular2LocalizationAspNetCore")
		)
	);

	// Requires that LocalizationModelContext is defined
	services.AddSqlLocalization(options => options.UseTypeFullNames = true);

[/code]

Create your database

[code language="csharp"]
dotnet ef migrations add Localization --context localizationModelContext

dotnet ef database update Localization --context localizationModelContext
[/code]


========================


# ASP.NET Core 1.0 MVC Localization Example


<ul>
	<li><a href="http://damienbod.com/2015/10/21/asp-net-5-mvc-6-localization/">ASP.NET Core 1.0 MVC Localization</a></li>
	<li><a href="http://damienbod.com/2015/10/24/using-dataannotations-and-localization-in-asp-net-5-mvc-6/">Using DataAnnotations and Localization in ASP.NET Core 1.0 MVC </a></li>
	<li><a href="http://damienbod.com/2016/01/29/asp-net-core-1-0-using-sql-localization/">ASP.NET Core 1.0 using SQL Localization</a></li>
</ul>



