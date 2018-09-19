using System.Web.Http;
using Swashbuckle.Application;

namespace Bookings.Hosting.Configurations
{
    static class Swagger
    {
        public static HttpConfiguration ConfigureSwagger(this HttpConfiguration httpConfiguration)
        {
           httpConfiguration
                .EnableSwagger(c => c.SingleApiVersion("v1", "Une super API de réservation de chambre"))
                .EnableSwaggerUi();
            return httpConfiguration;
        } 
    }
    
}