using System.Globalization;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNet5Localization.Controllers
{
    [Route("api/[controller]")]
    public class LanguageController : Controller
    {
        // PUT api/language/en-US
        [HttpPut("{isoCode}")]
        public void Put(string isoCode)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(isoCode);
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(isoCode);
        }

    }
}
