using System;
using Bookings.Hosting.Models;
using Swashbuckle.Examples;

namespace Bookings.Hosting.Examples
{
    public class BookingInputExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new BookingInput
            {
                By = "person@mail.com",
                Comment = "No comment",
                From = DateTimeOffset.Parse("2018-01-01"), 
                To = DateTimeOffset.Parse("2018-01-03"),
                RoomId = 1
            };
        }
    }
}