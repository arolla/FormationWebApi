using System;

namespace Bookings.Core
{
    public abstract class BookingCancelResult
    {
        public sealed class NotFound : BookingCancelResult
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

        public sealed class Cancelled : BookingCancelResult
        {
            public int BookingId { get; }

            Cancelled(int bookingId)
            {
                BookingId = bookingId;
            }

            public static Cancelled Create(int bookingId)
            {
                return new Cancelled(bookingId);
            }
        }

        public sealed class Error : BookingCancelResult
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