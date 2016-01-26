using System.Globalization;
using System.Threading;
using AspNet5Localization.Resources;

namespace AspNet5Localization.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Localization;
    using Microsoft.Extensions.Localization;

    [ServiceFilter(typeof(LanguageActionFilter))]
    [Route("api/{culture}/[controller]")]
    public class AboutWithCultureInRouteController : Controller
    {
        // http://localhost:5000/api/it-CH/AboutWithCultureInRoute
        // http://localhost:5000/api/fr-CH/AboutWithCultureInRoute

        private readonly IStringLocalizer<AmazingResource> _localizer;


        public AboutWithCultureInRouteController(IStringLocalizer<AmazingResource> localizer)
        {
            _localizer = localizer;
        }

        [HttpGet]
        public string Get()
        {
            return _localizer["Name"];
        }
    }
}
