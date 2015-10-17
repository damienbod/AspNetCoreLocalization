using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace AspNet5Localization.Controllers
{
    [Route("api/[controller]")]
    public class AboutController : Controller
    {
        // GET: api/values
        [HttpGet]
        public string Get()
        {
            return "Welcome en-US";
        }
    }
}
