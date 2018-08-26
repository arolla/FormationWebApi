using System.Web.Http;

namespace Bookings.Hosting.Controllers
{
    [RoutePrefix("api/valus")]
    public class ValuesController : ApiController
    {
        // GET api/values
        [HttpGet]
        public IHttpActionResult Get()
        {
            return this.Ok(new string[] { "value1", "value2" });
        }

        // GET api/values/5
        [Route("{id}")]
        [HttpGet()]
        public IHttpActionResult Get(int id)
        {
            return this.Ok("value");
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [Route("{id}")]
        [HttpPut()]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [Route("{id}")]
        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}
