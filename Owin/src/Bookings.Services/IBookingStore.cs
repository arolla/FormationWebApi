using System;
using System.Collections.Generic;
using Bookings.Core;

namespace Bookings.Services
{
    public interface IBookingStore
    {
        IEnumerable<Booking> GetPeriodBookings(DateTimeOffset from, DateTimeOffset to);
    }
}