using System;
using System.Net.Http;
using Microsoft.Owin.Hosting;

namespace Bookings.Hosting
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/"; 

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress)) 
            { 
                Console.WriteLine($"Server started at {baseAddress}");
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient(); 

                var response = client.GetAsync(baseAddress + "api/values").Result; 

                Console.WriteLine(response); 
                Console.WriteLine(response.Content.ReadAsStringAsync().Result); 
                Console.ReadLine(); 
            } 
        }
    }
}
