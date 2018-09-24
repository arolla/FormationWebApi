using Bookings.Core;

namespace Bookings.Hosting.Models
{
    /// <summary>
    /// A room availability for fixed period
    /// </summary>
    public class AvailabilityView
    {
        public AvailabilityView(int roomId, int roomCapacity, int roomPrice)
        {
            RoomId = roomId;
            RoomCapacity = roomCapacity;
            RoomPrice = roomPrice;
        }

        /// <summary>
        /// The identifier of the room
        /// </summary>
        public int RoomId { get; }
        /// <summary>
        /// The capacity of the room
        /// </summary>
        public int RoomCapacity { get; }
        /// <summary>
        /// The price of the room per night
        /// </summary>
        public int RoomPrice { get; }


        internal static AvailabilityView From(Availability availability)
        {
            return new AvailabilityView(availability.Room.Id, availability.Room.Capacity, availability.Room.Price);
        }
    }
}