using System.IO;
using System.Web.Http;
using Swashbuckle.Application;

namespace Bookings.Hosting.Configurations
{
    static class Swagger
    {
        public static HttpConfiguration ConfigureSwagger(this HttpConfiguration httpConfiguration)
        {
           httpConfiguration
                .EnableSwagger(c =>
               {
                   c.SingleApiVersion("v1", "Une super API de réservation de chambre");
                   var filePath = Path.Combine(System.AppContext.BaseDirectory, "Bookings.Hosting.xml");
                   c.IncludeXmlComments(filePath);
               })
                .EnableSwaggerUi();
            return httpConfiguration;
        } 
    }
    
}