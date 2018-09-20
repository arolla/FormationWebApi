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
        /// <param name="query">the filters used for querying</param>
        /// <returns>the rooms that are available during this period with their price</returns>
        [SwaggerResponse(HttpStatusCode.OK, type: typeof(AvailabilitiesView))]
        [HttpGet]
        [Route(Name = Operations.GetAvailabilities)]
        public IHttpActionResult Get([FromUri] AvailabilitiesQuery query)
        {
            if (query == null)
            {
                query = new AvailabilitiesQuery();
            }

            this.Validate(query);
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (query.From.Value.Date >= query.To.Value.Date)
            {
                return this.BadRequest("the period is invalid");
            }

            var availabilities = new[] {new AvailabilityView {RoomCapacity = 1, RoomId = 1, RoomPrice = 50}};
            return this.Ok(new AvailabilitiesView(availabilities));
        }
    }
}