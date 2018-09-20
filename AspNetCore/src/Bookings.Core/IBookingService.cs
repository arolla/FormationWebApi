using System.Collections.Generic;

namespace Bookings.Core
{
    public interface IBookingService
    {
        BookingCreationResult Book(BookingCreation bookingCreation);
        BookingUpdateResult Update(BookingUpdate bookingUpdate);
        BookingCancelResult Cancel(int bookingId);
        BookingSearchResult Search(BookingSearch bookingSearch);
        BookingResult Get(int bookingId);
    }
}