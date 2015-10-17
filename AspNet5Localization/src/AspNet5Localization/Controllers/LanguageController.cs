using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNet5Localization.Controllers
{
    [Route("api/[controller]")]
    public class LanguageController : Controller
    {
        // PUT api/language/en-US
        [HttpPut("{isoCode}")]
        public void Put(string isoCode, [FromQuery]string value)
        {
        }

    }
}
