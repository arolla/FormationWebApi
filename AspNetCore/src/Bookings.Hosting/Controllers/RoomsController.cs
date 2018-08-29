using System;
using Bookings.Hosting.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookings.Hosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        [HttpGet("availability/list/{from}/{to}")]
        public IActionResult Availabalities(
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

        [HttpGet("booking/book/{from}/{to}/{roomId}/{customerEmail}/{comment}")]
        public IActionResult Book(DateTime from, DateTime to, int roomId, string customerEmail, string comment)
        {
            return this.Ok(new Booking());
        }

        [HttpGet("booking/update/{bookingId}/{comment}")]
        public IActionResult Update(int bookingId, string comment)
        {
            return this.Ok();
        }

        [HttpGet("booking/cancel/{bookingId}")]
        public IActionResult Cancel(int bookingId)
        {
            return this.Ok();
        }

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
