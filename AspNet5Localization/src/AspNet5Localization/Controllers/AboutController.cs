using System.Globalization;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
	
namespace AspNet5Localization.Controllers
{
    [Route("api/[controller]")]
    public class AboutController : Controller
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IStringLocalizer<AboutController> _aboutLocalizerizer;

        public AboutController(IStringLocalizer<SharedResource> localizer, IStringLocalizer<AboutController> aboutLocalizerizer)
        {
            _localizer = localizer;
            _aboutLocalizerizer = aboutLocalizerizer;
        }

        [HttpGet]
        public string Get()
        {
            // _localizer["Name"] 
            return _aboutLocalizerizer["AboutTitle"];
        }
    }
}
