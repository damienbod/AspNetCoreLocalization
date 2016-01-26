using System.Globalization;
using System.Threading;
using AspNet5Localization.Resources;

namespace AspNet5Localization.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Localization;
    using Microsoft.Extensions.Localization;

    [Route("api/[controller]")]
    public class AboutController : Controller
    {
        private readonly IStringLocalizer<AmazingResource> _localizer;

       
        public AboutController(IStringLocalizer<AmazingResource> localizer)
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
