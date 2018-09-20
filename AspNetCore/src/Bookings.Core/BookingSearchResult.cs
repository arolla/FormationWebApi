using System;
using System.Collections.Generic;

namespace Bookings.Core
{
    public abstract class BookingSearchResult
    {
        public sealed class Succeed : BookingSearchResult
        {
            public IReadOnlyCollection<Booking> Bookings { get; }

            Succeed(IReadOnlyCollection<Booking> bookings)
            {
                Bookings = bookings;
            }

            public static Succeed Create(params Booking[] bookings)
            {
                return new Succeed(bookings);
            }
        }

        public sealed class BookingSearchIsNull : BookingSearchResult
        {
            public BookingSearchIsNull()
            {
            }
        }

        public sealed class Error : BookingSearchResult
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