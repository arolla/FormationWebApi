using System;
using System.Net;
using System.Web.Http;
using Bookings.Hosting.Models;
using Swashbuckle.Swagger.Annotations;

namespace Bookings.Hosting.Controllers
{
    [RoutePrefix("api/v1/bookings")]
    [SwaggerResponse(HttpStatusCode.InternalServerError)]
    [SwaggerResponse(HttpStatusCode.BadRequest)]
    public class BookingsController : ApiController
    {
        [HttpPost]
        [Route(Name = Operations.Book)]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.Created, type: typeof(BookingView))]
        public IHttpActionResult Book([FromBody] BookingInput input)
        {
            if (input == null || !this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            return this.Created(new Uri(Url.Link(Operations.GetBooking, new {id = 1})),
                new BookingView(1, input.From.Value, input.To.Value, input.RoomId, 50));
        }

        [HttpGet]
        [Route(Name = Operations.GetBooking)]
        [SwaggerResponse(HttpStatusCode.OK, type:typeof(BookingView))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult GetById(int id)
        {
            return this.Ok(new BookingView(1, DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1), 1, 50));
        }
    }
}