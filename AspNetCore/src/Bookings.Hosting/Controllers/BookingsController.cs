using System;
using System.Linq;
using Bookings.Core;
using Bookings.Hosting.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Hosting.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public class BookingsController : ControllerBase
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
        [HttpPost(Name = Operations.Book)]
        [ProducesResponseType(typeof(BookingView), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Book([FromBody] BookingInput input)
        {
            var result = this.bookingService.Book(input.Map());
            switch (result)
            {
                case BookingCreationResult.Created created:
                    return this.Created(new Uri(Url.Link(Operations.GetBooking, new {id = 1})), BookingView.For(created.Booking));
                case BookingCreationResult.Conflict conflict:
                    return this.Conflict();
                case BookingCreationResult.Error error:
                    return this.StatusCode(StatusCodes.Status500InternalServerError, error.Exception);
                default:
                    return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Retrieve a particular booking
        /// </summary>
        /// <param name="id">the booking id</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = Operations.GetBooking)]
        [ProducesResponseType(typeof(BookingView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var result = this.bookingService.Get(id);

            switch (result)
            {
                case BookingResult.NotFound notFound:
                    return this.NotFound();
                case BookingResult.Succeed succeed:
                    return this.Ok(BookingView.For(succeed.Booking));
                case BookingResult.Error error:
                    return this.StatusCode(StatusCodes.Status500InternalServerError, error.Exception);
                default:
                    return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Search the bookings for a period
        /// </summary>
        /// <param name="query">the search criteria</param>
        /// <returns></returns>
        [HttpGet(Name = Operations.GetBookings)]
        [ProducesResponseType(typeof(BookingsView), StatusCodes.Status200OK)]
        public IActionResult Search([FromQuery]BookingsQuery query)
        {
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
                    return this.StatusCode(StatusCodes.Status500InternalServerError, error.Exception);
                default:
                    return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Partially update a booking
        /// </summary>
        /// <param name="id">the booking id</param>
        /// <param name="patch">the changes</param>
        /// <returns></returns>
        [HttpPatch("{id}", Name = Operations.UpdateBooking)]
        [ProducesResponseType(typeof(BookingView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update(int id, [FromBody]BookingPatch patch)
        {
            var updateResult = this.bookingService.Update(patch.MapFor(id));

            switch (updateResult)
            {
                case BookingUpdateResult.NotFound notFound:
                    return this.NotFound();
                case BookingUpdateResult.Updated updated:
                    return this.Ok(BookingView.For(updated.Booking));
                case BookingUpdateResult.Error error:
                    return this.StatusCode(StatusCodes.Status500InternalServerError, error.Exception);
                default:
                    return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Cancel a booking
        /// </summary>
        /// <param name="id">the booking id</param>
        /// <returns>No Content</returns>
        [HttpDelete]
        [Route("{id}", Name = Operations.CancelBooking)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Cancel(int id)
        {
            var cancelResult = this.bookingService.Cancel(id);
            switch (cancelResult)
            {
                case BookingCancelResult.NotFound notFound:
                    return this.NotFound(); 
                case BookingCancelResult.Cancelled cancelled:
                    return this.NoContent();
                case BookingCancelResult.Error error:
                    return this.StatusCode(StatusCodes.Status500InternalServerError, error.Exception);
                default:
                    return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}