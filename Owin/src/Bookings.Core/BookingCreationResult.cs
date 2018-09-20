using System;
using System.Collections.Generic;

namespace Bookings.Core
{
    public abstract class BookingCreationResult
    {
        public sealed class Created : BookingCreationResult
        {
            public Booking Booking { get; }

            Created(Booking booking)
            {
                Booking = booking;
            }

            public static Created Create(Booking booking)
            {
                return new Created(booking);
            }
        }

        public sealed class BookingCreationIsNull : BookingCreationResult
        {
            public BookingCreationIsNull()
            {
            }
        }

        public sealed class Conflict : BookingCreationResult
        {
            public IReadOnlyCollection<int> BookingIds { get; }

            Conflict(IReadOnlyCollection<int> bookingIds)
            {
                BookingIds = bookingIds;
            }

            public static Conflict Create(params int[] bookingIds)
            {
                return new Conflict(bookingIds);
            }
        }

        public sealed class Error : BookingCreationResult
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