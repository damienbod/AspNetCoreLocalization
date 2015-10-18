using AspNet5Localization.Resources;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Localization;

namespace AspNet5Localization.Controllers
{
    [Route("api/[controller]")]
    public class AboutController : Controller
    {
        private IHtmlLocalizer<AmazingResource> _htmlLocalizer;

        public AboutController(IHtmlLocalizer<AmazingResource> localizer)
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
