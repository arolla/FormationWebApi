namespace Bookings.Core
{
    public class BookingUpdate
    {
        public BookingUpdate(int bookingId, string comment)
        {
            BookingId = bookingId;
            Comment = comment;
        }

        public int BookingId { get; }
        public string Comment { get; }
    }
}