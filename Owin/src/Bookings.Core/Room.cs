namespace Bookings.Core
{
    public class Room
    {
        public int Id { get; }
        public int Capacity { get; }
        public int Price { get; }

        public Room(int id, int capacity, int price)
        {
            Id = id;
            Capacity = capacity;
            Price = price;
        }
    }
}