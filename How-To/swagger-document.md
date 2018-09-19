# Comment documenter son Api avec Swagger

## Activer la documentation xml du projet

### Avant:

Via Visual Studio en éditant les propriétés du projet vous pouvez activer la documentation mais souvent cela entraîne un oubli car on ne le fait que pour le mode debug et du coup en release ce n'est pas activé.

### Maintenant:

Il suffit d'ajouter la simple balise 

```<GenerateDocumentationFile>true</GenerateDocumentationFile>```

Dans un des PropertyGroup de votre csproj.

## Configurer Swagger pour prendre en compte cette documentation

### Pour Owin

Il suffit d'ajouter dans la configuration de swagger ```Configurations\Swagger.cs```

```csharp
.EnableSwagger(c => {
    ...
var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
var commentsFile = Path.Combine(baseDirectory, commentsFileName);
c.IncludeXmlComments(commentsFile);
```

Ce code va utiliser le fichier xml générer par le projet.


### Pour AspNetCore

Il suffit d'ajouter dans la configuration des services pour swagger ```Configurations\Swagger.cs```

```csharp
.services.AddSwaggerGen(c =>
    {
    ...
var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
var commentsFileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
var commentsFile = Path.Combine(baseDirectory, commentsFileName);
c.IncludeXmlComments(commentsFile);
```

Ce code va utiliser le fichier xml générer par le projet.

## Commenter par le xml son controller et les models en input et output 

```csharp
/// <summary>
/// Allow to search the availabilities between 2 dates
/// </summary>
/// <param name="from">the start of the period</param>
/// <param name="to">the end of the period</param>
/// <returns>the rooms that are available during this period with their price</returns>
[HttpGet]
[Route]
public IHttpActionResult Get(DateTimeOffset from, DateTimeOffset to)
```

```csharp
/// <summary>
/// A room availability for fixed period
/// </summary>
public class Availability
{
    /// <summary>
    /// The identifier of the room
    /// </summary>
    public int RoomId { get; set; }
    /// <summary>
    /// The capacity of the room
    /// </summary>
    public int RoomCapacity { get; set; }
    /// <summary>
    /// The price of the room per night
    /// </summary>
    public int RoomPrice { get; set; }
}
```

## Ajouter des attributs spécifiques pour swagger

Afin de pouvoir définir les StatusCode que peux renvoyé une route de votre controller ainsi que le type de retour associé si il y en a un, nous pouvons utiliser un attribut.

### Pour Owin

il faut ajouter l'attribute de swashbuckle ```SwaggerResponse``` de la manière suivante sur la method Get
```csharp 
[SwaggerResponse(HttpStatusCode.OK, type:typeof(IEnumerable<Availability>))]
[SwaggerResponse(HttpStatusCode.InternalServerError)]
```
> :warning: Par défaut swasbuckle spécifie un ```HttpStatusCode.OK``` or dans le cas d'un Post on renvoi un ```HttpStatusCode.Created``` donc il faut 
> utiliser l'attribut ```SwaggerResponseRemoveDefaults```

### Pour AspNetCore

il faut ajouter l'attribute  de la stack mvc ```ProducesResponseType``` de la manière suivante sur la method Get
```csharp 
[ProducesResponseType(typeof(IEnumerable<Availability>), (int)HttpStatusCode.OK)]
[ProducesResponseType(500)]
```
