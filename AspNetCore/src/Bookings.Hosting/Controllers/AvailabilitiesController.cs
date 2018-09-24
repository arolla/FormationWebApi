using System;
using System.Linq;
using Bookings.Core;
using Bookings.Hosting.Examples;
using Bookings.Hosting.Models;
using Bookings.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace Bookings.Hosting.Controllers
{
    /// <summary>
    /// The availabilities
    /// </summary>
    [Route("api/v1/availabilities")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        /// <param name="query">the filters used for querying</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(AvailabilitiesView), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult Search([FromQuery]AvailabilitiesQuery query)
        {
            if (query.From.Value.Date >= query.To.Value.Date)
            {
                return this.BadRequest("the period is invalid");
            }
            var result = this.availabilities.GetAvailabilities(query.Map());

            switch (result)
            {
                case AvailabilitiesResult.Succeed succeed:
                    var data = succeed.Availabilities.Select(AvailabilityView.From);
                    return this.Ok(new AvailabilitiesView(data));
                case AvailabilitiesResult.Error error:
                    return this.StatusCode(StatusCodes.Status500InternalServerError, error.Exception);
                default:
                    return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
