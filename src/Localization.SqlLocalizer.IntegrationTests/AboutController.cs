using System.Globalization;
using System.Threading;
using Localization.SqlLocalizer.DbStringLocalizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using System.Linq;

namespace Localization.SqlLocalizer.IntegrationTests.Controllers
{
    [Route("api/[controller]")]
    public class AboutController : Controller
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IStringLocalizer<AboutController> _aboutLocalizerizer;
        private readonly IStringExtendedLocalizerFactory _stringExtendedLocalizerFactory;
        private readonly LocalizationModelContext _context;


        public AboutController(IStringLocalizer<SharedResource> localizer, IStringLocalizer<AboutController> aboutLocalizerizer, IStringExtendedLocalizerFactory stringExtendedLocalizerFactory, LocalizationModelContext context)
        {
            _localizer = localizer;
            _aboutLocalizerizer = aboutLocalizerizer;
            _stringExtendedLocalizerFactory = stringExtendedLocalizerFactory;
            _context = context;
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
        [Route("noncount")]
        public int GetNonExistingTextCount()
        {
            return _context.LocalizationRecords.Count();
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
