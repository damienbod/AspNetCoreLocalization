using System.Globalization;
using System.Threading;
using Localization.SqlLocalizer.DbStringLocalizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;

namespace Localization.SqlLocalizer.IntegrationTests.Controllers
{
    [Route("api/[controller]")]
    public class AboutController : Controller
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IStringLocalizer<AboutController> _aboutLocalizerizer;
        private readonly IStringExtendedLocalizerFactory _stringExtendedLocalizerFactory;


        public AboutController(IStringLocalizer<SharedResource> localizer, IStringLocalizer<AboutController> aboutLocalizerizer, IStringExtendedLocalizerFactory stringExtendedLocalizerFactory)
        {
            _localizer = localizer;
            _aboutLocalizerizer = aboutLocalizerizer;
            _stringExtendedLocalizerFactory = stringExtendedLocalizerFactory;
        }

        [HttpGet]
        public string Get()
        {
            // _localizer["Name"] 
            return _aboutLocalizerizer["AboutTitle"];
        }

        [HttpGet]
        [Route("non")]
        public string GetNonExistingText()
        {
            // _localizer["Name"] 
            return _aboutLocalizerizer["AboutTitleNon"];
        }

        [HttpGet]
        [Route("reset")]
        public string Reset()
        {
            _stringExtendedLocalizerFactory.ResetCache();
            return _aboutLocalizerizer["AboutTitle"];
        }
    }
}
