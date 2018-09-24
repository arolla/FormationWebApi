using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using Bookings.Core;
using Bookings.Hosting.Models;
using Swashbuckle.Swagger.Annotations;

namespace Bookings.Hosting.Controllers
{
    [RoutePrefix("api/v1/bookings")]
    [SwaggerResponse(HttpStatusCode.InternalServerError)]
    [SwaggerResponse(HttpStatusCode.BadRequest)]
    public class BookingsController : ApiController
    {
        private readonly IBookingService bookingService;

        public BookingsController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        /// <summary>
        /// Book a room
        /// </summary>
        /// <param name="input">the input</param>
        /// <returns></returns>
        [HttpPost]
        [Route(Name = Operations.Book)]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.Created, type: typeof(BookingView))]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        public IHttpActionResult Book([FromBody] BookingInput input)
        {
            if (input == null || !this.ModelState.IsValid)
                return this.BadRequest(this.ModelState);

            var result = this.bookingService.Book(input.Map());
            switch (result)
            {
                case BookingCreationResult.Created created:
                    return this.Created(new Uri(Url.Link(Operations.GetBooking, new {id = 1})), BookingView.For(created.Booking));
                case BookingCreationResult.Conflict conflict:
                    return this.Conflict();
                case BookingCreationResult.Error error:
                    return InternalServerError(error.Exception);
                default:
                    return InternalServerError();
            }
        }

        /// <summary>
        /// Retrieve a particular booking
        /// </summary>
        /// <param name="id">the booking id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}", Name = Operations.GetBooking)]
        [SwaggerResponse(HttpStatusCode.OK, type:typeof(BookingView))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult GetById(int id)
        {
            var result = this.bookingService.Get(id);

            switch (result)
            {
                case BookingResult.NotFound notFound:
                    return this.NotFound();
                case BookingResult.Succeed succeed:
                    return this.Ok(BookingView.For(succeed.Booking));
                case BookingResult.Error error:
                    return InternalServerError(error.Exception);
                default:
                    return InternalServerError();
            }
        }

        /// <summary>
        /// Search the bookings for a period
        /// </summary>
        /// <param name="query">the search criteria</param>
        /// <returns></returns>
        [HttpGet]
        [Route(Name = Operations.GetBookings)]
        [SwaggerResponse(HttpStatusCode.OK, type:typeof(BookingsView))]
        public IHttpActionResult Search([FromUri]BookingsQuery query)
        {
            if (query == null)
            {
                query = new BookingsQuery();
            }

            this.Validate(query);
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (query.From.Value.Date > query.To.Value.Date)
            {
                return this.BadRequest("the period is invalid");
            }

            var bookingSearchResult = this.bookingService.Search(query.Map());

            switch (bookingSearchResult)
            {
                case BookingSearchResult.Succeed succeed:
                    return this.Ok(new BookingsView(succeed.Bookings.Select(BookingView.For)));
                case BookingSearchResult.Error error:
                    return InternalServerError(error.Exception);
                default:
                    return InternalServerError();
            }
        }

        /// <summary>
        /// Partially update a booking
        /// </summary>
        /// <param name="id">the booking id</param>
        /// <param name="patch">the changes</param>
        /// <returns></returns>
        [HttpPatch]
        [Route("{id}", Name = Operations.UpdateBooking)]
        [SwaggerResponse(HttpStatusCode.OK, type:typeof(BookingView))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult Update(int id, [FromBody]BookingPatch patch)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var updateResult = this.bookingService.Update(patch.MapFor(id));

            switch (updateResult)
            {
                case BookingUpdateResult.NotFound notFound:
                    return this.NotFound();
                case BookingUpdateResult.Updated updated:
                    return this.Ok(BookingView.For(updated.Booking));
                case BookingUpdateResult.Error error:
                    return this.InternalServerError(error.Exception);
                default:
                    return InternalServerError();
            }
        }

        /// <summary>
        /// Cancel a booking
        /// </summary>
        /// <param name="id">the booking id</param>
        /// <returns>No Content</returns>
        [HttpDelete]
        [Route("{id}", Name = Operations.CancelBooking)]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NoContent)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult Cancel(int id)
        {
            var cancelResult = this.bookingService.Cancel(id);
            switch (cancelResult)
            {
                case BookingCancelResult.NotFound notFound:
                    return this.NotFound(); 
                case BookingCancelResult.Cancelled cancelled:
                   return this.StatusCode(HttpStatusCode.NoContent);
                case BookingCancelResult.Error error:
                    return this.InternalServerError(error.Exception);
                default:
                    return InternalServerError();
            }
        }
    }
}