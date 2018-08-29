{
﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Bookings.Hosting.Models;
using Swashbuckle.Swagger.Annotations;

namespace Bookings.Hosting.Controllers
{
    /// <summary>
    /// A controller
    /// </summary>
    [RoutePrefix("api/rooms")]
    public class RoomsController : ApiController
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
        [SwaggerResponse(HttpStatusCode.OK, type:typeof(IEnumerable<Availability>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [HttpGet]
        [Route("availability/list/{from}/{to}")]
        public IHttpActionResult Availabalities(
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
        [SwaggerResponse(HttpStatusCode.OK, type:typeof(Booking))]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [Route("booking/book/{from}/{to}/{roomId}/{customerEmail}/{comment}")]
        public IHttpActionResult Book(DateTime from, DateTime to, int roomId, string customerEmail, string comment)
        {
            return this.Ok(new Models.Booking());
        }

        /// <summary>
        /// Allow to update your comment on the booking
        /// </summary>
        /// <param name="bookingId">the id of the booking</param>
        /// <param name="comment">the new comment</param>
        /// <returns></returns>
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [Route("booking/update/{bookingId}/{comment}")]
        public IHttpActionResult Update(int bookingId, string comment)
        {
            return this.Ok();
        }

        /// <summary>
        /// Allow to cancel a booking
        /// </summary>
        /// <param name="bookingId">the booking id</param>
        /// <returns></returns>
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [Route("booking/cancel/{bookingId}")]
        public IHttpActionResult Cancel(int bookingId)
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
        [SwaggerResponse(HttpStatusCode.OK, type:typeof(IEnumerable<Booking>))]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [Route("booking/list/{from}/{to}")]
        public IHttpActionResult Bookings(DateTime from,
            DateTime to,int page,
            int size)
        {
            var bookings = new[] {new Models.Booking()};
            return this.Ok(bookings);
        }

    }
}
