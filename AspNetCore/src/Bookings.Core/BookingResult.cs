using System;

namespace Bookings.Core
{
    public abstract class BookingResult
    {
        public sealed class NotFound : BookingResult
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

        public sealed class Succeed : BookingResult
        {
            public Booking Booking { get; }

            Succeed(Booking booking)
            {
                Booking = booking;
            }

            public static Succeed Create(Booking booking)
            {
                return new Succeed(booking);
            }
        }

        public sealed class Error : BookingResult
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