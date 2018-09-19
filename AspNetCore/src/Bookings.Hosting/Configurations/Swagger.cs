using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Bookings.Hosting.Configurations
{
    static class Swagger
    {
        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSwagger()
                .UseSwaggerUI(c =>
                {
                   
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Une super API de réservation de chambre");
                });

            return applicationBuilder;
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info {Title = "Une super API de réservation de chambre", Version = "v1"});
                    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
                    var commentsFile = Path.Combine(baseDirectory, commentsFileName);
                    c.IncludeXmlComments(commentsFile);
                });
        }
    }
}