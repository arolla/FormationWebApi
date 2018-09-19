using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Bookings.Hosting.Models;
using Swashbuckle.Swagger.Annotations;

namespace Bookings.Hosting.Controllers
{
    /// <summary>
    /// The availabilities
    /// </summary>
    [RoutePrefix("api/v1/availabilities")]
    [SwaggerResponse(HttpStatusCode.InternalServerError)]
    [SwaggerResponse(HttpStatusCode.BadRequest)]
    public class AvailabilitiesController : ApiController
    {
        /// <summary>
        /// Allow to search the availabilities between 2 dates
        /// </summary>
        /// <param name="from">the start of the period</param>
        /// <param name="to">the end of the period</param>
        /// <returns>the rooms that are available during this period with their price</returns>
        [SwaggerResponse(HttpStatusCode.OK, type: typeof(IEnumerable<Availability>))]
        [HttpGet]
        [Route]
        public IHttpActionResult Get(DateTimeOffset from, DateTimeOffset to)
        {
            if (from.Date < DateTimeOffset.Now.Date)
            {
                return this.BadRequest("the period can't be in the past");
            }
            if (from.Date >= to.Date)
            {
                return this.BadRequest("the period is invalid");
            }

            var availabilities = new[] { new Availability { RoomCapacity = 1, RoomId = 1, RoomPrice = 50 } };
            return this.Ok(availabilities);
        }
    }
}