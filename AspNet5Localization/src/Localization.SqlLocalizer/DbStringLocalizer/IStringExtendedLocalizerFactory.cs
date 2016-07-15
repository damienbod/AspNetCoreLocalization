using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Localization;

namespace Localization.SqlLocalizer.DbStringLocalizer
{
    public interface IStringExtendedLocalizerFactory : IStringLocalizerFactory
    {
        void ResetCache();

        void ResetCache(Type resourceSource);

        IList GetImportHistory();

        IList GetExportHistory();

        IList GetLocalizationData();

        IList GetLocalizationData(DateTime from, string culture = null);

        IList GetLocalizationDataSinceLastImport(bool updateExportHistory = false, string reason = "default export");

        void ImportLocalizationData(List<LocalizationRecord> data, string information);

    }
}
