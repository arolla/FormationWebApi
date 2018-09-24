using Microsoft.Extensions.DependencyInjection;
using Bookings.Core;
using NSubstitute;
using NSubstitute.ClearExtensions;

namespace Bookings.Hosting.Tests.Controllers.BookingsControllerTests
{
    public class BookingControllerTests : BaseControllerTest
    {
        protected readonly IBookingService BookingService = Substitute.For<IBookingService>();

        protected override void ConfigureService(IServiceCollection services)
        {
            BookingService.ClearSubstitute();
            services.AddSingleton(BookingService);
        }
    }
}