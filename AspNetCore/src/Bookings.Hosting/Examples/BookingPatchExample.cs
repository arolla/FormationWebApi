using Bookings.Hosting.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Bookings.Hosting.Examples
{
    public class BookingPatchExample : IExamplesProvider<BookingPatch>
    {
        public BookingPatch GetExamples()
        {
            return new BookingPatch {Comment = "A new comment"};
        }
    }
}