using System;

namespace Bookings.Core
{
    public abstract class BookingUpdateResult
    {
        public sealed class NotFound : BookingUpdateResult
        {
            public int BookingId { get; }

            NotFound(int bookingId)
            {
                BookingId = bookingId;
            }

            public static NotFound Create(int bookingId)
            {
                return new NotFound(bookingId);
            }
        }

        public sealed class Updated : BookingUpdateResult
        {
            public Booking Booking { get; }

            Updated(Booking booking)
            {
                Booking = booking;
            }

            public static Updated Create(Booking booking)
            {
                return new Updated(booking);
            }
        }

        public sealed class BookingUpdateIsNull : BookingUpdateResult
        {
            public BookingUpdateIsNull()
            {
            }
        }
        public sealed class Error : BookingUpdateResult
        {
            public Exception Exception { get; }

            Error(Exception exception)
            {
                Exception = exception;
            }

            public static Error Create(Exception exception)
            {
                return new Error(exception);
            }
        }
    }
}