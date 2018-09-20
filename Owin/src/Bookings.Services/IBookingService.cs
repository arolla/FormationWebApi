using System.Collections.Generic;
using Bookings.Core;

namespace Bookings.Services
{
    public interface IBookingService
    {
        BookingCreationResult Book(BookingCreation bookingCreation);
        BookingUpdateResult Update(BookingUpdate bookingUpdate);
        BookingUpdateResult Cancel(int bookingId);
        IEnumerable<Booking> Search(BookingSearch bookingSearch);
        Booking Get(int bookingId);
    }
}