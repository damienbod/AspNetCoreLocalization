using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet5Localization.Model;
using Microsoft.AspNet.Mvc;

namespace AspNet5Localization.Controllers
{
    [Route("api/[controller]")]
    public class BoxesController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
            {
                return HttpNotFound(id);
            }

            return Ok(new Box() { Id = id, Height = 10, Length = 10, Width=10 });
        }

        [HttpPost]
        public IActionResult Post([FromBody]Box box)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }
            else
            {           
                string url = Url.RouteUrl("GetByIdRoute", new { id = 11111 },
                    Request.Scheme, Request.Host.ToUriComponent());

                return Created(url, box);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Box box)
        {
            if(id == 0)
            {
                return HttpNotFound(box);
            }

            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }
            else
            {
                return Ok(box);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return HttpNotFound(id);
            }

            return Ok();
        }
    }
}
