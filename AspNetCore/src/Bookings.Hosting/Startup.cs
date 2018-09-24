using Bookings.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Bookings.Hosting.Configurations;
using Bookings.Services;

namespace Bookings.Hosting
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<RoomService>();
            services.AddSingleton<IAvailabilityService>(provider => provider.GetService<RoomService>());
            services.AddSingleton<IBookingService>(provider => provider.GetService<RoomService>());
            services.AddSingleton<IRoomStore, InMemoryRoomStore>();
            services.AddSingleton<IBookingStore, InMemoryBookingStore>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.ConfigureSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.ConfigureSwagger();
            app.UseMvc();
        }
    }
}
