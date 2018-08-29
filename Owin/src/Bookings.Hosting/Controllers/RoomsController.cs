using System;
using System.Web.Http;
using Bookings.Hosting.Models;

namespace Bookings.Hosting.Controllers
{
    [RoutePrefix("api/rooms")]
    public class RoomsController : ApiController
    {
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

        [Route("booking/book/{from}/{to}/{roomId}/{customerEmail}/{comment}")]
        public IHttpActionResult Book(DateTime from, DateTime to, int roomId, string customerEmail, string comment)
        {
            return this.Ok(new Models.Booking());
        }

        [Route("booking/update/{bookingId}/{comment}")]
        public IHttpActionResult Update(int bookingId, string comment)
        {
            return this.Ok();
        }

        [Route("booking/cancel/{bookingId}")]
        public IHttpActionResult Cancel(int bookingId)
        {
            return this.Ok();
        }

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
