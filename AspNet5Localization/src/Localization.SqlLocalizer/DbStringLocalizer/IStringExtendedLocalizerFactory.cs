using System;
using System.Collections;
using Microsoft.Extensions.Localization;

namespace Localization.SqlLocalizer.DbStringLocalizer
{
    public interface IStringExtendedLocalizerFactory : IStringLocalizerFactory
    {
        void ResetCache();

        void ResetCache(Type resourceSource);

        IList GetImportHistory();

        IList GetExportHistory();

        void ImportLocalizationData();

        IList GetAllLocalizationData(bool updateExportHistory = false);

        IList GetNewLocalizationDataSinceLastImport(bool updateExportHistory = false);

        IList GetLocalizationData(DateTime from, string culture = null);
    }
}
