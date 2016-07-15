using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Localization.SqlLocalizer.DbStringLocalizer
{
    public class ImportHistory
    {
        public long Id { get; set; }

        public DateTime Exported { get; set; }

        public DateTime Imported { get; set; }

        public string Information { get; set; }
    }
}
