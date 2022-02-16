using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Localization.SqlLocalizer.DbStringLocalizer
{
    public class SqlStringLocalizerFactory : IStringLocalizerFactory, IStringExtendedLocalizerFactory
    {
        private readonly DevelopmentSetup _developmentSetup;
        private readonly LocalizationModelContext _context;
        private static readonly ConcurrentDictionary<string, IStringLocalizer> _resourceLocalizations = new ConcurrentDictionary<string, IStringLocalizer>();
        private readonly IOptions<SqlLocalizationOptions> _options;
        private const string Global = "global";

        public SqlStringLocalizerFactory(
           LocalizationModelContext context,
           DevelopmentSetup developmentSetup,
           IOptions<SqlLocalizationOptions> localizationOptions)
        {
            _options = localizationOptions ?? throw new ArgumentNullException(nameof(localizationOptions));
            _context = context ?? throw new ArgumentNullException(nameof(LocalizationModelContext));
            _developmentSetup = developmentSetup;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            var returnOnlyKeyIfNotFound = _options.Value.ReturnOnlyKeyIfNotFound;
            var createNewRecordWhenLocalisedStringDoesNotExist = _options.Value.CreateNewRecordWhenLocalisedStringDoesNotExist;
            SqlStringLocalizer sqlStringLocalizer;

            if (_options.Value.UseOnlyPropertyNames)
            {
                if (_resourceLocalizations.Keys.Contains(Global))
                {
                    return _resourceLocalizations[Global];
                }

                sqlStringLocalizer = new SqlStringLocalizer(GetAllFromDatabaseForResource(Global),  _developmentSetup, Global, returnOnlyKeyIfNotFound, createNewRecordWhenLocalisedStringDoesNotExist);
                return _resourceLocalizations.GetOrAdd(Global, sqlStringLocalizer);
                
            }
            else if (_options.Value.UseTypeFullNames)
            {
                if (_resourceLocalizations.Keys.Contains(resourceSource.FullName))
                {
                    return _resourceLocalizations[resourceSource.FullName];
                }

                sqlStringLocalizer = new SqlStringLocalizer(GetAllFromDatabaseForResource(resourceSource.FullName), _developmentSetup, resourceSource.FullName, returnOnlyKeyIfNotFound, createNewRecordWhenLocalisedStringDoesNotExist);
                return _resourceLocalizations.GetOrAdd(resourceSource.FullName, sqlStringLocalizer);
            }

            if (_resourceLocalizations.Keys.Contains(resourceSource.Name))
            {
                return _resourceLocalizations[resourceSource.Name];
            }

            sqlStringLocalizer = new SqlStringLocalizer(GetAllFromDatabaseForResource(resourceSource.Name), _developmentSetup, resourceSource.Name, returnOnlyKeyIfNotFound, createNewRecordWhenLocalisedStringDoesNotExist);
            return _resourceLocalizations.GetOrAdd(resourceSource.Name, sqlStringLocalizer);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            var returnOnlyKeyIfNotFound = _options.Value.ReturnOnlyKeyIfNotFound;
            var createNewRecordWhenLocalisedStringDoesNotExist = _options.Value.CreateNewRecordWhenLocalisedStringDoesNotExist;

            var resourceKey = baseName + location;
            if (_options.Value.UseOnlyPropertyNames)
            {
                resourceKey = Global;
            }

            if (_resourceLocalizations.Keys.Contains(resourceKey))
            {
                return _resourceLocalizations[resourceKey];
            }

            var sqlStringLocalizer = new SqlStringLocalizer(GetAllFromDatabaseForResource(resourceKey), _developmentSetup, resourceKey, returnOnlyKeyIfNotFound, createNewRecordWhenLocalisedStringDoesNotExist);
            return _resourceLocalizations.GetOrAdd(resourceKey, sqlStringLocalizer);
        }

        public void ResetCache()
        {
            _resourceLocalizations.Clear();

            lock (_context)
            {
                _context.DetachAllEntities();
            }
        }

        public void ResetCache(Type resourceSource)
        {
            IStringLocalizer returnValue;
            _resourceLocalizations.TryRemove(resourceSource.FullName, out returnValue);

            lock (_context)
            {
                _context.DetachAllEntities();                
            }
        }

        private Dictionary<string, string> GetAllFromDatabaseForResource(string resourceKey)
        {
            lock (_context)
            {
                return _context.LocalizationRecords.Where(data => data.ResourceKey == resourceKey)
                    .ToDictionary(kvp => (kvp.Key + "." + kvp.LocalizationCulture), kvp => kvp.Text, StringComparer.OrdinalIgnoreCase);
            }
        }

        public IList GetImportHistory()
        {
            lock (_context)
            {
                return _context.ImportHistoryDbSet.ToList();
            }
        }

        public IList GetExportHistory()
        {
            lock (_context)
            {
                return _context.ExportHistoryDbSet.ToList();
            }
        }

        public IList GetLocalizationData(string reason = "export")
        {
            lock (_context)
            {
                _context.ExportHistoryDbSet.Add(new ExportHistory {Reason = reason, Exported = DateTime.UtcNow});
                _context.SaveChanges();

                return _context.LocalizationRecords.ToList();
            }
        }

        public IList GetLocalizationData(DateTime from, string culture = null, string reason = "export")
        {
            lock (_context)
            {
                _context.ExportHistoryDbSet.Add(new ExportHistory {Reason = reason, Exported = DateTime.UtcNow});
                _context.SaveChanges();

                if (culture != null)
                {
                    return _context.LocalizationRecords.Where(item =>
                            EF.Property<DateTime>(item, "UpdatedTimestamp") > from &&
                            item.LocalizationCulture == culture)
                        .ToList();
                }

                return _context.LocalizationRecords
                    .Where(item => EF.Property<DateTime>(item, "UpdatedTimestamp") > from).ToList();
            }
        }

 
        public void UpdatetLocalizationData(List<LocalizationRecord> data, string information)
        {
            lock (_context)
            {
                _context.DetachAllEntities();
                _context.UpdateRange(data);
                _context.ImportHistoryDbSet.Add(new ImportHistory { Information = information, Imported = DateTime.UtcNow });
                _context.SaveChanges();                
            }
        }

        public void AddNewLocalizationData(List<LocalizationRecord> data, string information)
        {
            lock (_context)
            {
                _context.DetachAllEntities();
                _context.AddRange(data);
                _context.ImportHistoryDbSet.Add(new ImportHistory { Information = information, Imported = DateTime.UtcNow });
                _context.SaveChanges();                
            }
        }
    }
}
