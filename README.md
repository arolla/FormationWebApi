# Formation WebApi Arolla

## Requis:
* Visual studio 2017.8
* Dotnet core sdk 2.1.400 (normalement installé avec VS2017 update 8)
* Docker for windows
  * Run ```docker run -e KEYCLOAK_USER=admin -e KEYCLOAK_PASSWORD=admin -e DB_VENDOR=H2 -p 8080:8080 --name sso jboss/keycloak``` 
