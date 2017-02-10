using AspNetCoreLocalization.Model;
using Microsoft.AspNetCore.Mvc;

namespace AspNet5Localization.Controllers
{
    [Route("api/[controller]")]
    public class BoxesController : Controller
    {
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
            {
                return NotFound(id);
            }

            return Ok(new Box() { Id = id, Height = 10, Length = 10, Width=10 });
        }

        /// <summary>
        /// http://localhost:5000/api/boxes?culture=it-CH
        /// Content-Type: application/json
        /// 
        /// { "Id":7,"Height":10,"Width":10,"Length":1000}
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]Box box)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {           
                string url = Url.RouteUrl("api/boxes", new { id = 11111 },
                    Request.Scheme, Request.Host.ToUriComponent());

                return Created(url, box);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Box box)
        {
            if(id == 0)
            {
                return NotFound(box);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
                return NotFound(id);
            }

            return new NoContentResult();
        }
    }
}
