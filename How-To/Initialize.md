# Comment créer une solution de WebApi

Il faut faire les actions suivantes

```
dotnet new global --sdk-version 2.1.400
```

## Pour Owin
> :warning: Note pour raison de simplification le projet est une console et non un 
web host car ces projets ne sont pas encore supporter par le nouveau format 
de projet arrivé avec visual studio 2017

Setup de la solution

```
mdkir Owin 
cd Owin
dotnet new sln -n Bookings.Owin
mkdir src
cd src
dotnet new console -n Bookings.Hosting
```

> Changer la propriété TargetFramework du projet créé en ```net461```

```
dotnet sln ../Bookings.Owin.sln add Bookings.Hosting/Bookings.Hosting.csproj
cd Bookings.Hosting
dotnet add Bookings.Hosting.csproj package Microsoft.AspNet.WebApi.OwinSelfHost -v 5.2.6
dotnet add Bookings.Hosting.csproj package Microsoft.Owin.Hosting -v 4.0.0
mkdir Controllers
```

Ajouter une class ```Startup.cs``` à la racine du projet

```csharp
using System.Web.Http;
using Owin;

namespace Bookings.Hosting
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration(); 
            config.Routes.MapHttpRoute( 
                name: "DefaultApi", 
                routeTemplate: "api/{controller}/{id}", 
                defaults: new { id = RouteParameter.Optional } 
            ); 

            appBuilder.UseWebApi(config); 
        }
    }
}

```

Modifier la class ```Program.cs```

```csharp
using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;

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
```

Enfin ajouter dans le répertoire ```Controllers``` et un controller ```ValuesController.cs``` comme suit

```csharp
using System.Web.Http;

namespace Bookings.Hosting.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        [HttpGet]
        public IHttpActionResult Get()
        {
            return this.Ok(new string[] { "value1", "value2" });
        }

        // GET api/values/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return this.Ok("value");
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}
```

Ensuite pour exécuter le programme depuis le répertoire de la solution Owin (*\FormationWebApi\Owin)
```
dotnet run --project src/Bookings.Hosting/Bookings.Hosting.csproj
```

## Pour AspNetCore

```
mdkir AspNetCore
cd AspNetCore
dotnet new sln -n Bookings.AspNetCore
mkdir src
cd src
dotnet new webapi -n Bookings.Hosting
dotnet sln ../Bookings.AspNetCore.sln add Bookings.Hosting/Bookings.Hosting.csproj
```

Ensuite pour exécuter le programme depuis le répertoire de la solution AspNetCore
```
dotnet run --project src/Bookings.Hosting/Bookings.Hosting.csproj
```