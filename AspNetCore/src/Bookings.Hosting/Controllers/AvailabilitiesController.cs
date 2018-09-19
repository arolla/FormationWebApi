using System;
using Bookings.Hosting.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Hosting.Controllers
{
    [Route("api/v1/availabilities")]
    [ApiController]
    public class AvailabilitiesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(
            DateTimeOffset from,
            DateTimeOffset to
        )
        {
            var availabilities = new[] {new Availability { RoomCapacity = 1, RoomId = 1, RoomPrice = 50 }};
            return this.Ok(availabilities);
        }

    }
}
