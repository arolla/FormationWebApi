using System;
using System.Web.Http;
using Bookings.Hosting.Models;

namespace Bookings.Hosting.Controllers
{
    [RoutePrefix("api/v1/availabilities")]
    public class AvailabilitiesController : ApiController
    {
        [HttpGet]
        [Route]
        public IHttpActionResult Get(
            DateTimeOffset from,
            DateTimeOffset to
        )
        {
            var availabilities = new[] {new Availability { RoomCapacity = 1, RoomId = 1, RoomPrice = 50 }};
            return this.Ok(availabilities);
        }

    }
}