namespace Bookings.Hosting.Models
{
    /// <summary>
    /// A room availability for fixed period
    /// </summary>
    public class Availability
    {
        /// <summary>
        /// The identifier of the room
        /// </summary>
        public int RoomId { get; set; }
        /// <summary>
        /// The capacity of the room
        /// </summary>
        public int RoomCapacity { get; set; }
        /// <summary>
        /// The price of the room per night
        /// </summary>
        public int RoomPrice { get; set; }
    }
}