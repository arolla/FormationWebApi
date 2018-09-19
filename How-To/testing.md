# Comment tester une api

La meilleur manière est d'utiliser les librairies respectives de tests car vous serez sure que toutes les informations du context sont fourni

## Setup des projets de tests

### Pour Owin

> :warning: les actions en ligne de commande sont exécuter depuis la racine de la solution Owin

Ajouter un répertoire tests dans le répertoire de la solution
```
mkdir tests
cd tests
```

Créer un projet de tests:
```
dotnet new nunit -n Bookings.Hosting.Tests
```

> Changer la propriété TargetFramework du projet créé en ```net461```

Ajouter le projet à la solution
```
dotnet sln ../Bookings.Owin.sln add Bookings.Hosting.Tests/Bookings.Hosting.Tests.csproj
```

Ajouter les références nécessaires
```
cd Bookings.Hosting.Tests
dotnet add Bookings.Hosting.Tests.csproj package Microsoft.Owin.Testing -v 4.0.0
dotnet add Bookings.Hosting.Tests.csproj reference ../../src/Bookings.Hosting/Bookings.Hosting.csproj
```

Créer un classe TestsStartup

```csharp
public class TestsStartup
{
    public void Configuration(IAppBuilder appBuilder)
    {
        HttpConfiguration config = new HttpConfiguration();

        config.MapHttpAttributeRoutes();

        config.Services.Replace(typeof(IAssembliesResolver), new TestWebApiResolver());

        appBuilder.UseWebApi(config);
    }
}

/// cette class permet de load des assemblies depuis autre part mais la le trick viens juste du fait que l'on utilise l'assembly contenant les controllers 
/// il suffirait juste d'utiliser un objet de cette assembly pour pouvoir s'en passer.
class TestWebApiResolver : IAssembliesResolver
{
    public ICollection<Assembly> GetAssemblies()
    {
        return new Assembly[]
        {
            Assembly.GetAssembly(typeof(Bookings.Hosting.Startup))
        };
    }
}
```

Enfin renommé la class ```UnitTest1.cs``` en ```GetAvailabilitiesShould.cs```

Et remplacer le contenu par 
```csharp

using Microsoft.Owin.Testing;
using NUnit.Framework;
using System.Net;
using System.Threading.Tasks;

namespace Bookings.Hosting.Tests
{
    public class GetAvailabilitiesShould
    {
        [Test]
        public async Task Return_Ok_when_valid_request()
        {
            using (var server = TestServer.Create<TestsStartup>())
            using (var client = server.HttpClient)
            {
                var response = await client.GetAsync("api/v1/availabilities?from=2018-01-01&to=2018-01-02");
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}
```

Ensuite depuis la racine de la solution run la commande suivante 
```
dotnet test Bookings.Owin.sln
```

### Pour AspNetCore

> :warning: les actions en ligne de commande sont exécuter depuis la racine de la solution AspNetCore

Ajouter un répertoire tests dans le répertoire de la solution
```
mkdir tests
cd tests
```

Créer un projet de tests:
```
dotnet new nunit -n Bookings.Hosting.Tests
```
> Changer la propriété TargetFramework du projet créé en ```netcoreapp2.1```

Ajouter le projet à la solution
```
dotnet sln ../Bookings.AspNetCore.sln add Bookings.Hosting.Tests/Bookings.Hosting.Tests.csproj
```

Ajouter les références nécessaires
```
cd Bookings.Hosting.Tests
dotnet add Bookings.Hosting.Tests.csproj package Microsoft.AspNetCore.TestHost -v 2.1.1
dotnet add Bookings.Hosting.Tests.csproj package Microsoft.AspNetCore.Mvc -v 2.1.2
dotnet add Bookings.Hosting.Tests.csproj reference ../../src/Bookings.Hosting/Bookings.Hosting.csproj
```

Créer un classe TestsStartup

```csharp
public class TestsStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                    // same thing than the custom assembly provider for owin 
                    .AddApplicationPart(typeof(RoomsController).Assembly)
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
```

Enfin renommé la class ```UnitTest1.cs``` en ```GetAvailabilitiesShould.cs```

Et remplacer le contenu par 
```csharp
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace Bookings.Hosting.Tests
{
    public class GetAvailabilitiesShould
    {
        [Test]
        public async Task Return_Ok_when_valid_request()
        {
            using (var server = new TestServer(new WebHostBuilder().UseStartup<TestsStartup>()))
            using (var client = server.CreateClient())
            {
                var response = await client.GetAsync("api/v1/availabilities?from=2018-01-01&to=2018-01-02");
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}
```

Ensuite depuis la racine de la solution run la commande suivante 
```
dotnet test Bookings.AspNetCore.sln
```