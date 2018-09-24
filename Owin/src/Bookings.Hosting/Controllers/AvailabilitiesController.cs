using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Bookings.Core;
using Bookings.Hosting.Models;
using Bookings.Services;
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
        /// <returns>the rooms that are available during this period with their price</returns>
        [SwaggerResponse(HttpStatusCode.OK, type: typeof(IEnumerable<AvailabilityView>))]
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

            var result = this.availabilities.GetAvailabilities(new AvailabilitySearch(from, to, null));

            switch (result)
            {
                case AvailabilitiesResult.Succeed succeed:
                    var data = succeed.Availabilities.Select(AvailabilityView.From);
                    return this.Ok(data);
                case AvailabilitiesResult.Error error:
                    return InternalServerError(error.Exception);
                default:
                    return InternalServerError();
            }
        }
    }
}