using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNet5Localization.DbStringLocalizer
{
    using System.Globalization;
    using Microsoft.Extensions.Localization;

    public class SqlStringLocalizer<T> : IStringLocalizer<T>
    {
        public LocalizedString this[string name]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                throw new NotImplementedException();
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
    }
}
