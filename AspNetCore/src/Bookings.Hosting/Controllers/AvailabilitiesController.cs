using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        /// <param name="from">the start of the period</param>
        /// <param name="to">the end of the period</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(IEnumerable<Availability>), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult Get([Required]DateTimeOffset from, [Required]DateTimeOffset to)
        {
            if (from.Date < DateTimeOffset.Now.Date)
            {
                return this.BadRequest("the period can't be in the past");
            }
            if (from.Date >= to.Date)
            {
                return this.BadRequest("the period is invalid");
            }
            var availabilities = new[] {new Availability { RoomCapacity = 1, RoomId = 1, RoomPrice = 50 }};
            return this.Ok(availabilities);
        }
    }
}
