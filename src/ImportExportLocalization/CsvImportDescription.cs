using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace ImportExportLocalization
{
    public class CsvImportDescription
    {
        public string Information { get; set; }
        public ICollection<IFormFile> File { get; set; }
    }
}