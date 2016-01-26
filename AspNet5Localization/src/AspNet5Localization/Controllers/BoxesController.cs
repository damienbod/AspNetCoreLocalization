using AspNet5Localization.Model;

namespace AspNet5Localization.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class BoxesController : Controller
    {
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
            {
                return HttpNotFound(id);
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
                return HttpBadRequest(ModelState);
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

            return new NoContentResult();
        }
    }
}
