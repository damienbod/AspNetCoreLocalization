using System.Globalization;
using System.Threading;
using AspNet5Localization.Resources;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Localization;

namespace AspNet5Localization.Controllers
{
    [ServiceFilter(typeof(LanguageActionFilter))]
    [Route("api/{culture}/[controller]")]
    public class AboutWithCultureInRouteController : Controller
    {
        // http://localhost:5000/api/it-CH/AboutWithCultureInRoute
        // http://localhost:5000/api/fr-CH/AboutWithCultureInRoute

        private IHtmlLocalizer<AmazingResource> _htmlLocalizer;

        public AboutWithCultureInRouteController(IHtmlLocalizer<AmazingResource> localizer)
        {
            _htmlLocalizer = localizer;
        }

        [HttpGet]
        public string Get()
        {
            return _htmlLocalizer["Name"];
        }
    }
}
