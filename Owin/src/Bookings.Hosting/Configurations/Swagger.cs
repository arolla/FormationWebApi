using System;
using System.IO;
using System.Reflection;
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
                    c.PrettyPrint();
                    c.SingleApiVersion("v1", "Une super API de réservation de chambre");
                    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                    var commentsFile = Path.Combine(baseDirectory, commentsFileName);
                    c.IncludeXmlComments(commentsFile);
                })
                 .EnableSwaggerUi();
            return httpConfiguration;
        }
    }

}