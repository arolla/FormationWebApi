using System;
using System.Collections.Generic;
using Bookings.Core;

namespace Bookings.Services
{
    public abstract class AvailabilitiesResult
    {
        public sealed class AvailabilitySearchIsNull : AvailabilitiesResult
        {
            public AvailabilitySearchIsNull()
            {
            }
        }

        public sealed class Succeed : AvailabilitiesResult
        {
            public IReadOnlyCollection<Availability> Availabilities { get; }

            Succeed(IReadOnlyCollection<Availability> availabilities)
            {
                Availabilities = availabilities;
            }

            public static Succeed Create(params Availability[] availabilitites)
            {
                return new Succeed(availabilitites);
            }
        }
        public sealed class Error : AvailabilitiesResult
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