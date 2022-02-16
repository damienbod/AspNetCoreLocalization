using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Extensions.Localization;

namespace Localization.SqlLocalizer.DbStringLocalizer
{
    public class SqlStringLocalizer : IStringLocalizer
    {
        private readonly Dictionary<string, string> _localizations;

        private readonly DevelopmentSetup _developmentSetup;
        private readonly string _resourceKey;
        private bool _returnKeyOnlyIfNotFound;
        private bool _createNewRecordWhenLocalisedStringDoesNotExist;

        public SqlStringLocalizer(Dictionary<string, string> localizations, DevelopmentSetup developmentSetup, string resourceKey, bool returnKeyOnlyIfNotFound, bool createNewRecordWhenLocalisedStringDoesNotExist)
        {
            _localizations = localizations;
            _developmentSetup = developmentSetup;
            _resourceKey = resourceKey;
            _returnKeyOnlyIfNotFound = returnKeyOnlyIfNotFound;
            _createNewRecordWhenLocalisedStringDoesNotExist = createNewRecordWhenLocalisedStringDoesNotExist;
        }
        public LocalizedString this[string name]
        {
            get
            {
                bool notSucceed;
                var text = GetText(name, out notSucceed);
                
                return new LocalizedString(name, text,notSucceed);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var localizedString = this[name];
                return new LocalizedString(name, String.Format(localizedString.Value, arguments), localizedString.ResourceNotFound);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
            
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string GetText(string key,out bool notSucceed)
        {

#if NET451
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
#elif NET46
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
#else
            var culture = CultureInfo.CurrentCulture;
#endif

            string computedKey = $"{key}.{culture}";
            string parentComputedKey = $"{key}.{culture.Parent.TwoLetterISOLanguageName}";

            string result;
            if (_localizations.TryGetValue(computedKey, out result) || _localizations.TryGetValue(parentComputedKey, out result))
            {
                notSucceed = false;
                return result;
            }
            else
            {
                notSucceed = true;
                if (_createNewRecordWhenLocalisedStringDoesNotExist)
                {
                    _developmentSetup.AddNewLocalizedItem(key, culture.ToString(), _resourceKey);
                    _localizations.Add(computedKey, computedKey);
                    return computedKey;
                }
                if (_returnKeyOnlyIfNotFound)
                {
                    return key;
                }

                return _resourceKey + "." + computedKey;
            }
        }



    }
}
