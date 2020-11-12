using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using Localization.SqlLocalizer;
using Localization.SqlLocalizer.DbStringLocalizer;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for adding localization servics to the DI container.
    /// </summary>
    public static class SqlLocalizationServiceCollectionExtensions
    {
        /// <summary>
        /// Adds services required for application localization.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddSqlLocalization(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return AddSqlLocalization(services, setupAction: null);
        }

        /// <summary>
        /// Adds services required for application localization.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="setupAction">An action to configure the <see cref="LocalizationOptions"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddSqlLocalization(
            this IServiceCollection services,
            Action<SqlLocalizationOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAdd(new ServiceDescriptor(
                typeof(IStringExtendedLocalizerFactory),
                typeof(SqlStringLocalizerFactory),
                ServiceLifetime.Singleton));
            services.TryAdd(new ServiceDescriptor(
                typeof(IStringLocalizerFactory),
                typeof(SqlStringLocalizerFactory),
                ServiceLifetime.Singleton));
            //services.TryAdd(new ServiceDescriptor(
            //    typeof(IStringLocalizer),
            //    typeof(SqlStringLocalizer),
            //    ServiceLifetime.Transient));
            services.TryAdd(new ServiceDescriptor(
              typeof(DevelopmentSetup),
              typeof(DevelopmentSetup),
              ServiceLifetime.Singleton));

            if (setupAction != null)
            {
                services.Configure(setupAction);
            }
            return services;
        }

        public static IServiceCollection AddLocalizationSqlSchema(
            this IServiceCollection services,
            string schema)
        {
            // Adds services required for using options.
            services.AddOptions();

            // Registers the following lambda used to configure options.
            services.Configure<SqlContextOptions>(myOptions =>
            {
                myOptions.SqlSchemaName = schema;
            });

            return services;
        }

    }
}
