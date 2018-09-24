using Bookings.Hosting.Models;
using Swashbuckle.Examples;

namespace Bookings.Hosting.Examples
{
    public class BookingPatchExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new BookingPatch {Comment = "A new comment"};
        }
    }
}