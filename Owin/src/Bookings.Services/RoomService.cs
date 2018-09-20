using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bookings.Core;

namespace Bookings.Services
{
   public class RoomService : IAvailabilityService, IBookingService
    {
        private readonly IRoomStore roomStore;
        private readonly IBookingStore bookingStore;

        public RoomService(
            IRoomStore roomStore,
            IBookingStore bookingStore)
        {
            this.roomStore = roomStore;
            this.bookingStore = bookingStore;
        }
        public IEnumerable<Availability> GetAvailabilities(AvailabilitySearch availabilitySearch)
        {
            var perdiodBookings =
                bookingStore.GetPeriodBookings(availabilitySearch.From, availabilitySearch.To)
                            .ToArray();

            Func<Room, bool> filterRoom =
                r =>
                    r.Capacity >= availabilitySearch.RoomCapacity &&
                    perdiodBookings.All(b => b.RoomId != r.Id);

            var rooms =
                roomStore.GetRooms()
                    .Where(filterRoom)
                    .ToArray();

            return Enumerable.Empty<Availability>();
        }

        public BookingCreationResult Book(BookingCreation bookingCreation)
        {
            throw new NotImplementedException();
        }

        public BookingUpdateResult Update(BookingUpdate bookingUpdate)
        {
            throw new NotImplementedException();
        }

        public BookingUpdateResult Cancel(int bookingId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Booking> Search(BookingSearch bookingSearch)
        {
            throw new NotImplementedException();
        }

        public Booking Get(int bookingId)
        {
            throw new NotImplementedException();
        }
    }
}
