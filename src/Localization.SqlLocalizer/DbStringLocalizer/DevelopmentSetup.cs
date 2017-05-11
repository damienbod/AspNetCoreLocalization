
using Microsoft.Extensions.Options;

namespace Localization.SqlLocalizer.DbStringLocalizer
{
    // >dotnet ef migrations add LocalizationMigration
    public class DevelopmentSetup
    {
        private readonly LocalizationModelContext _context;
        private readonly IOptions<SqlLocalizationOptions> _options;

        public DevelopmentSetup(
           LocalizationModelContext context,
           IOptions<SqlLocalizationOptions> localizationOptions)
        {
            _options = localizationOptions;
            _context = context;
        }

        public void AddNewLocalizedItem(string key, string culture, string resourceKey)
        {
            string computedKey = $"{key}.{culture}";

            LocalizationRecord localizationRecord = new LocalizationRecord()
            {
                LocalizationCulture = culture,
                Key = key,
                Text = computedKey,
                ResourceKey = resourceKey
            };
            _context.LocalizationRecords.Add(localizationRecord);
            _context.SaveChanges();
        }
    }
}