using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace AspNet5Localization.Controllers
{

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
