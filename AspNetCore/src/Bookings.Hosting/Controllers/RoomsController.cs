using System;
using System.Collections.Generic;
using System.Net;
using Bookings.Hosting.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Hosting.Controllers
{
    /// <summary>
    /// A controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        /// <summary>
        /// Allow to search the availabilities between 2 dates
        /// </summary>
        /// <param name="from">the start of the period</param>
        /// <param name="to">the end of the period</param>
        /// <param name="roomCapacity">the capacity you are looking for</param>
        /// <param name="roomMinPrice">the minimal price</param>
        /// <param name="roomMaxPrice">the maximum price</param>
        /// <param name="page">the page of the search you want to display (0-based index)</param>
        /// <param name="size">the number of item per page</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(IEnumerable<Availability>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(500)]
        [HttpGet("availability/list/{from}/{to}")]
        public IActionResult Availabilities(
            DateTime from,
            DateTime to,
            int roomCapacity,
            int roomMinPrice,
            int roomMaxPrice,
            int page,
            int size
            )
        {
            var availabilities = new[] {new Availability()};
            return this.Ok(availabilities);
        }

        /// <summary>
        /// Book a room
        /// </summary>
        /// <param name="from">the start of the period</param>
        /// <param name="to">the end of the period</param>
        /// <param name="roomId">the room id</param>
        /// <param name="customerEmail">the email of the customer</param>
        /// <param name="comment">a comment</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(Booking), (int)HttpStatusCode.OK)]
        [ProducesResponseType(500)]
        [HttpGet("booking/book/{from}/{to}/{roomId}/{customerEmail}/{comment}")]
        public IActionResult Book(DateTime from, DateTime to, int roomId, string customerEmail, string comment)
        {
            return this.Ok(new Booking());
        }

        /// <summary>
        /// Allow to update your comment on the booking
        /// </summary>
        /// <param name="bookingId">the id of the booking</param>
        /// <param name="comment">the new comment</param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(500)]
        [HttpGet("booking/update/{bookingId}/{comment}")]
        public IActionResult Update(int bookingId, string comment)
        {
            return this.Ok();
        }

        /// <summary>
        /// Allow to cancel a booking
        /// </summary>
        /// <param name="bookingId">the booking id</param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(500)]
        [HttpGet("booking/cancel/{bookingId}")]
        public IActionResult Cancel(int bookingId)
        {
            return this.Ok();
        }

        /// <summary>
        /// Allow to search the bookings between 2 dates
        /// </summary>
        /// <param name="from">the start of the period</param>
        /// <param name="to">the end of the period</param>
        /// <param name="page">the page of the search you want to display (0-based index)</param>
        /// <param name="size">the number of item per page</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(IEnumerable<Booking>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(500)]
        [HttpGet("booking/list/{from}/{to}")]
        public IActionResult Bookings(DateTime from,
            DateTime to,int page,
            int size)
        {
            var bookings = new[] {new Booking()};
            return this.Ok(bookings);
        }

    }
}
