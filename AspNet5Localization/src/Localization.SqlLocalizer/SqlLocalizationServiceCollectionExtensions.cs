using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;

namespace Microsoft.Extensions.DependencyInjection
{
    using global::Localization.SqlLocalizer;
    using global::Localization.SqlLocalizer.DbStringLocalizer;

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
                typeof(IStringLocalizerFactory),
                typeof(SqlStringLocalizerFactory),
                ServiceLifetime.Singleton));
            services.TryAdd(new ServiceDescriptor(
                typeof(IStringLocalizer),
                typeof(SqlStringLocalizer),
                ServiceLifetime.Singleton));

            if (setupAction != null)
            {
                services.Configure(setupAction);
            }
            return services;
        }
    }
}
