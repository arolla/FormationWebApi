using System;
using System.Collections.Generic;
using Bookings.Core;

namespace Bookings.Services
{
    public interface IBookingStore
    {
        IEnumerable<StoredBooking> GetPeriodBookings(DateTimeOffset from, DateTimeOffset to);
        StoredBooking Get(int bookingId);
        IEnumerable<StoredBooking> Find(DateTimeOffset @from, DateTimeOffset to, int? roomId = null);
        void Delete(int bookingId);
        StoredBooking Update(int id, string comment);
        StoredBooking Create(int roomId, string @by, string comment, DateTimeOffset @from, DateTimeOffset to, int price);
    }
}