using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bookings.Core;
using static Bookings.Core.BookingCancelResult;
using static Bookings.Core.BookingCreationResult;
using static Bookings.Core.BookingResult;
using static Bookings.Core.BookingSearchResult;

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

        public AvailabilitiesResult GetAvailabilities(AvailabilitySearch availabilitySearch)
        {
            return
                Safe<AvailabilitiesResult>
                (
                    () =>
                    {
                        if (availabilitySearch == null)
                            return new AvailabilitiesResult.AvailabilitySearchIsNull();

                        var perdiodBookings =
                            bookingStore.GetPeriodBookings(availabilitySearch.From, availabilitySearch.To)
                                        .ToArray();

                        bool FilterRoom(StoredRoom r) =>
                            (!availabilitySearch.RoomCapacity.HasValue || r.Capacity >= availabilitySearch.RoomCapacity.Value) &&
                            perdiodBookings.All(b => b.RoomId != r.Id);

                        var rooms =
                            roomStore.GetRooms()
                                .Where(FilterRoom)
                                .ToArray();

                        var nbDays = (availabilitySearch.To - availabilitySearch.From).Days;
                        
                        var availabilities =
                            rooms.Select(r =>
                                new Availability(new Room(r.Id, r.Capacity, r.Price), availabilitySearch.From, availabilitySearch.To, nbDays * r.Price))
                                .ToArray();

                        return AvailabilitiesResult.Succeed.Create(availabilities);
                    },
                    onError: AvailabilitiesResult.Error.Create
                );
        }

        public BookingCreationResult Book(BookingCreation bookingCreation)
        {
            return
                Safe<BookingCreationResult>
                (
                    () =>
                    {
                        if (bookingCreation == null)
                            return new BookingCreationIsNull();

                        var bs =
                            bookingStore
                                .Find(bookingCreation.From, bookingCreation.To, bookingCreation.RoomId)
                                .Select(ToBooking)
                                .ToArray();

                        if (bs.Length > 0)
                            return Conflict.Create(bs.Select(s => s.Id).ToArray());

                        var room =
                            roomStore.GetRoom(bookingCreation.RoomId);

                        var nbDays = (bookingCreation.To - bookingCreation.From).Days;

                        var price =
                            room.Price * nbDays;

                        var storedBooking = 
                            bookingStore.Create(
                                bookingCreation.RoomId,
                                bookingCreation.By,
                                bookingCreation.Comment,
                                bookingCreation.From,
                                bookingCreation.To,
                                price
                                );

                        var booking = ToBooking(storedBooking);
                        return Created.Create(booking);
                    },
                    onError: BookingCreationResult.Error.Create
                );
        }

        public BookingUpdateResult Update(BookingUpdate bookingUpdate)
        {
            return 
                Safe<BookingUpdateResult>
                    (
                        () =>
                        {
                            if (bookingUpdate == null)
                                return new BookingUpdateResult.BookingUpdateIsNull();

                            var b = bookingStore.Get(bookingUpdate.BookingId);

                            if (b == null)
                                return BookingUpdateResult.NotFound.Create(bookingUpdate.BookingId);

                            var storedBooking =
                                bookingStore.Update(
                                    bookingUpdate.BookingId,
                                    bookingUpdate.Comment);

                            var booking = ToBooking(storedBooking);
                            return BookingUpdateResult.Updated.Create(booking);
                        },
                        onError: BookingUpdateResult.Error.Create
                    );
        }

        static T Safe<T>(Func<T> f, Func<Exception, T> onError)
        {
            try
            {
                return f();
            }
            catch (Exception e)
            {
                return onError(e);
            }
        }

        public BookingCancelResult Cancel(int bookingId)
        {
            return
                Safe<BookingCancelResult>
                (
                    () =>
                    {
                        var b = bookingStore.Get(bookingId);

                        if (b == null)
                            return BookingCancelResult.NotFound.Create(bookingId);

                        bookingStore.Delete(bookingId);

                        return Cancelled.Create(bookingId);
                    },
                    onError: BookingCancelResult.Error.Create
                );
        }

        public BookingSearchResult Search(BookingSearch bookingSearch)
        {
            return
                Safe<BookingSearchResult>
                (
                    () =>
                    {
                        if (bookingSearch == null)
                            return new BookingSearchIsNull();

                        var bs =
                            bookingStore
                                .Find(bookingSearch.From, bookingSearch.To)
                                .Select(ToBooking)
                                .ToArray();

                        return BookingSearchResult.Succeed.Create(bs);
                    },
                    onError: BookingSearchResult.Error.Create
                );
        }

        private static Booking ToBooking(StoredBooking storedBooking)
        {
            return new Booking(storedBooking.Id, storedBooking.RoomId, storedBooking.By, storedBooking.Comment, storedBooking.From, storedBooking.To, storedBooking.Price);
        }

        public BookingResult Get(int bookingId)
        {
            return
                Safe<BookingResult>
                (
                    () =>
                    {
                        var b = bookingStore.Get(bookingId);

                        if (b == null)
                            return BookingResult.NotFound.Create(bookingId);

                        return BookingResult.Succeed.Create(ToBooking(b));
                    },
                    onError: BookingResult.Error.Create
                );
        }
    }
}
