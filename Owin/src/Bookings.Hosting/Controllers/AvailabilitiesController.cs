using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using Bookings.Core;
using Bookings.Hosting.Examples;
using Bookings.Hosting.Models;
using Bookings.Services;
using Swashbuckle.Examples;
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
        /// <param name="query">the filters used for querying</param>
        /// <returns>the rooms that are available during this period with their price</returns>
        [SwaggerResponse(HttpStatusCode.OK, type: typeof(AvailabilitiesView))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(AvailabilitiesViewExample))]
        [HttpGet]
        [Route(Name = Operations.GetAvailabilities)]
        public IHttpActionResult Search([FromUri] AvailabilitiesQuery query)
        {
            if (query == null)
            {
                query = new AvailabilitiesQuery();
                this.Validate(query);
            }
            
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

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
                    return InternalServerError(error.Exception);
                default:
                    return InternalServerError();
            }
        }
    }
}