namespace Bookings.Services
{
    public class StoredRoom
    {
        public int Id { get; }
        public int Capacity { get; }
        public int Price { get; }

        public StoredRoom(int id, int capacity, int price)
        {
            Id = id;
            Capacity = capacity;
            Price = price;
        }
    }
}