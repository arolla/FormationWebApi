# Comment activer Swagger sur une Api

## Pour Owin

> :warning: les actions en ligne de commande sont exécuter depuis la racine de la solution Owin

Il faut d'abord installer le package [Swashbuckle](https://www.nuget.org/packages/Swashbuckle/)

Comme il y a un conflit de version il faut aussi installer le package [Microsoft.AspNet.WebApi.WebHost](https://www.nuget.org/packages/Microsoft.AspNet.WebApi.WebHost/)


```
dotnet add src/Bookings.Hosting/Bookings.Hosting.csproj package Swashbuckle
dotnet add src/Bookings.Hosting/Bookings.Hosting.csproj package Microsoft.AspNet.WebApi.WebHost
```

Créer un répertoire Configurations
```
mkdir src/Bookings.Hosting/Configurations
```

Y créer une nouvelle classe dans ce répertoire ```Swagger.cs```

```csharp
using System.Web.Http;
using Swashbuckle.Application;

namespace Bookings.Hosting.Configurations
{
    static class Swagger
    {
        public static HttpConfiguration ConfigureSwagger(this HttpConfiguration httpConfiguration)
        {
           httpConfiguration
                .EnableSwagger(c => c.SingleApiVersion("v1", "Une super API de Bookings"))
                .EnableSwaggerUi();
            return httpConfiguration;
        } 
    }
}
```
Dans la class ```Startup``` ajouter une ligne avant ``` appBuilder.UseWebApi(config);```

>```config.ConfigureSwagger();```

vous pouvez ensuite run l'application depuis Visual Studio ou avec la commande :
> ```dotnet run --project src/Bookings.Hosting/Bookings.Hosting.csproj```

Et accéder à la page swagger via [http://localhost:9000/swagger](http://localhost:9000/swagger)

## Pour AspNetCore

> :warning: les actions en ligne de commande sont exécuter depuis la racine de la solution AspNetCore

Il faut d'abord installer le package [Swashbuckle.AspNetCore](https://www.nuget.org/packages/Swashbuckle.AspNetCore/)

```
dotnet add src/Bookings.Hosting/Bookings.Hosting.csproj package Swashbuckle.AspNetCore
```

Créer un répertoire Configurations
```
mkdir src/Bookings.Hosting/Configurations
```

Y créer une nouvelle classe dans ce répertoire ```Swagger.cs```

```csharp
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
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Une super API de Bookings");
                });

            return applicationBuilder;
        }

        public static IServiceCollection ConfigureSwagger(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new Info { Title = "Une super API de Bookings", Version = "v1" }));
        }
    }
}
```
Dans la class ```Startup``` 
* dans la méthode ```ConfigureServices```
ajouter la ligne (l'ordre n'est pas important dans cette méthode)
>```services.ConfigureSwagger();```
* dans la méthode ```Configure```
ajouter la ligne suivante avant ```app.UseMvc()``` (l'ordre est important ici)
>```app.ConfigureSwagger();```

### _Modification de la configuration ```launchSettings.json```_
> _Afin de pouvoir démarrer l'application directement sur swagger depuis Visual Studio il faut modifier le fichier ```launchSettings.json``` dans le répertoire Properties du projet et changer les valeurs de launchUrl par swagger ainsi que l'applicationUrl en https://localhost:5001;http://localhost:5000 ()_


Vous pouvez ensuite run l'application depuis Visual Studio ou avec la commande :
> ```dotnet run --project src/Bookings.Hosting/Bookings.Hosting.csproj```

Et accéder à la page swagger via [https://localhost:5001/swagger](https://localhost:5001/swagger)
