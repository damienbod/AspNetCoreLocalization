using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Localization.SqlLocalizer.DbStringLocalizer
{
    public class ExportHistory
    {
        public long Id { get; set; }

        public DateTime Exported { get; set; }

        public string Reason { get; set; }
    }
}
