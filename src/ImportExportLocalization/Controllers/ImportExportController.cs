using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Localization.SqlLocalizer.DbStringLocalizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace ImportExportLocalization.Controllers
{
    [Route("api/ImportExport")]
    public class ImportExportController : Controller
    {
        private IStringExtendedLocalizerFactory _stringExtendedLocalizerFactory;

        public ImportExportController(IStringExtendedLocalizerFactory stringExtendedLocalizerFactory)
        {
            _stringExtendedLocalizerFactory = stringExtendedLocalizerFactory;
        }

        // http://localhost:6062/api/ImportExport/localizedData.csv
        [HttpGet]
        [Route("localizedData.csv")]
        [Produces("text/csv")]
        public IActionResult GetDataAsCsv()
        {
            return Ok(_stringExtendedLocalizerFactory.GetLocalizationData());
        }

        [Route("update")]
        [HttpPost]
        [ServiceFilter(typeof(ValidateMimeMultipartContentFilter))]
        public IActionResult ImportCsvFileForExistingData(CsvImportDescription csvImportDescription)
        {
            // TODO validate that data is a csv file.
            var contentTypes = new List<string>();

            if (ModelState.IsValid)
            {
                foreach (var file in csvImportDescription.File)
                {
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim('"');
                        contentTypes.Add(file.ContentType);

                        var inputStream = file.OpenReadStream();
                        var items = readStream(file.OpenReadStream());
                        _stringExtendedLocalizerFactory.UpdatetLocalizationData(items, csvImportDescription.Information);
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [Route("new")]
        [HttpPost]
        [ServiceFilter(typeof(ValidateMimeMultipartContentFilter))]
        public IActionResult ImportCsvFileForNewData(CsvImportDescription csvImportDescription)
        {
            // TODO validate that data is a csv file.
            var contentTypes = new List<string>();

            if (ModelState.IsValid)
            {
                foreach (var file in csvImportDescription.File)
                {
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim('"');
                        contentTypes.Add(file.ContentType);

                        var inputStream = file.OpenReadStream();
                        var items = readStream(file.OpenReadStream());
                        _stringExtendedLocalizerFactory.AddNewLocalizationData(items, csvImportDescription.Information);
                    }
                }
            }

            return RedirectToAction("Index", "Home");
        }

        private List<LocalizationRecord> readStream(Stream stream)
        {
            bool skipFirstLine = true;
            string csvDelimiter = ";";

            List<LocalizationRecord> list = new List<LocalizationRecord>();
            var reader = new StreamReader(stream);


            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(csvDelimiter.ToCharArray());
                if (skipFirstLine)
                {
                    skipFirstLine = false;
                }
                else
                {
                    var itemTypeInGeneric = list.GetType().GetTypeInfo().GenericTypeArguments[0];
                    var item = new LocalizationRecord();
                    var properties = item.GetType().GetProperties();
                    for (int i = 0; i < values.Length; i++)
                    {
                        properties[i].SetValue(item, Convert.ChangeType(values[i], properties[i].PropertyType), null);
                    }

                    list.Add(item);
                }

            }

            return list;
        }
    }
}
