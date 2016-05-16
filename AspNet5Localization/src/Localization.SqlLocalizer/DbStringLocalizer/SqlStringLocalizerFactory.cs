using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Localization.SqlLocalizer.DbStringLocalizer
{
    public class SqlStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly LocalizationModelContext _context;
        private readonly ConcurrentDictionary<string, IStringLocalizer> _resourceLocalizations = new ConcurrentDictionary<string, IStringLocalizer>();
        private readonly IOptions<SqlLocalizationOptions> _options;
        private const string Global = "global";

        public SqlStringLocalizerFactory(
           LocalizationModelContext context,
           IOptions<SqlLocalizationOptions> localizationOptions)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(LocalizationModelContext));
            }

            if (localizationOptions == null)
            {
                throw new ArgumentNullException(nameof(localizationOptions));
            }

            _options = localizationOptions;
            _context = context;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            SqlStringLocalizer sqlStringLocalizer;

            if (_options.Value.UseOnlyPropertyNames)
            {
                if (_resourceLocalizations.Keys.Contains(Global))
                {
                    return _resourceLocalizations[Global];
                }

                sqlStringLocalizer = new SqlStringLocalizer(GetAllFromDatabaseForResource(Global), Global);
                return _resourceLocalizations.GetOrAdd(Global, sqlStringLocalizer);
                
            }

            if (_options.Value.UseTypeFullNames)
            {
                if (_resourceLocalizations.Keys.Contains(resourceSource.FullName))
                {
                    return _resourceLocalizations[resourceSource.FullName];
                }

                sqlStringLocalizer = new SqlStringLocalizer(GetAllFromDatabaseForResource(resourceSource.FullName), resourceSource.FullName);
                _resourceLocalizations.GetOrAdd(resourceSource.FullName, sqlStringLocalizer);
            }


            if (_resourceLocalizations.Keys.Contains(resourceSource.Name))
            {
                return _resourceLocalizations[resourceSource.Name];
            }

            sqlStringLocalizer = new SqlStringLocalizer(GetAllFromDatabaseForResource(resourceSource.Name), resourceSource.Name);
            return _resourceLocalizations.GetOrAdd(resourceSource.Name, sqlStringLocalizer);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            if (_resourceLocalizations.Keys.Contains(baseName + location))
            {
                return _resourceLocalizations[baseName + location];
            }

            var sqlStringLocalizer = new SqlStringLocalizer(GetAllFromDatabaseForResource(baseName + location), baseName + location);
            return _resourceLocalizations.GetOrAdd(baseName + location, sqlStringLocalizer);
        }

        private Dictionary<string, string> GetAllFromDatabaseForResource(string resourceKey)
        {
            return _context.LocalizationRecords.Where(data => data.ResourceKey == resourceKey).ToDictionary(kvp => (kvp.Key + "." + kvp.LocalizationCulture), kvp => kvp.Text);
        }
    }
}
