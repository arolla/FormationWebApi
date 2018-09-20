using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bookings.Services
{
    public class InMemoryBookingStore : IBookingStore
    {
        private static readonly IDictionary<int, StoredBooking> bookings = new Dictionary<int, StoredBooking>();
        private int count = 0;
        public IEnumerable<StoredBooking> GetPeriodBookings(DateTimeOffset @from, DateTimeOffset to)
        {
            return
                bookings.Values.Where(b => b.From >= from && b.To <= to);
        }

        public StoredBooking Get(int bookingId)
        {
            if (bookings.TryGetValue(bookingId, out var b))
                return b;

            return null;
        }

        public IEnumerable<StoredBooking> Find(DateTimeOffset @from, DateTimeOffset to, int? roomId)
        {
            return
                bookings
                    .Values
                    .Where(b =>
                        b.From == @from &&
                        b.To == to &&
                        (!roomId.HasValue || b.RoomId == roomId));
        }

        public void Delete(int bookingId)
        {
            bookings.Remove(bookingId);
        }

        public StoredBooking Update(int id, string comment)
        {
            var booking = bookings[id];
            var storedBooking = new StoredBooking(id, booking.RoomId, booking.By, comment, booking.From, booking.To, booking.Price);
            bookings[id] = storedBooking;
            return storedBooking;
        }

        public StoredBooking Create(int roomId, string @by, string comment, DateTimeOffset @from, DateTimeOffset to, int price)
        {
            var id = ++count;
            var storedBooking = new StoredBooking(id, roomId, @by, comment, @from, to, price);
            bookings.Add(id, storedBooking);
            return storedBooking;
        }
    }
}
