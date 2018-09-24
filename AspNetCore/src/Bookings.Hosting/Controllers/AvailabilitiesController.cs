using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Bookings.Core;
using Bookings.Hosting.Models;
using Bookings.Services;
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
        private readonly IAvailabilityService availabilities;

        public AvailabilitiesController(IAvailabilityService availabilities)
        {
            this.availabilities = availabilities;
        }

        /// <summary>
        /// Allow to search the availabilities between 2 dates
        /// </summary>
        /// <param name="from">the start of the period</param>
        /// <param name="to">the end of the period</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(IEnumerable<AvailabilityView>), StatusCodes.Status200OK)]
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
            var result = this.availabilities.GetAvailabilities(new AvailabilitySearch(from, to, null));

            switch (result)
            {
                case AvailabilitiesResult.Succeed succeed:
                    var data = succeed.Availabilities.Select(AvailabilityView.From);
                    return this.Ok(data);
                case AvailabilitiesResult.Error error:
                    return this.StatusCode(StatusCodes.Status500InternalServerError, error.Exception);
                default:
                    return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
