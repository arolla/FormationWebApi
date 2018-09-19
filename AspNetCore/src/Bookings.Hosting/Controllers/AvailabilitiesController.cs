using System.Collections.Generic;
using Bookings.Hosting.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Hosting.Controllers
{
    /// <summary>
    /// The availabilities
    /// </summary>
    [Route("api/v1/availabilities")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class AvailabilitiesController : ControllerBase
    {
        /// <summary>
        /// Allow to search the availabilities between 2 dates
        /// </summary>
        /// <param name="query">the filters used for querying</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(AvailabilitiesView), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult Get([FromQuery]AvailabilityQuery query)
        {
            if (query.From.Value.Date >= query.To.Value.Date)
            {
                return this.BadRequest("the period is invalid");
            }
            var availabilities = new[] {new Availability { RoomCapacity = 1, RoomId = 1, RoomPrice = 50 }};
            return this.Ok(new AvailabilitiesView(availabilities));
        }
    }
}
