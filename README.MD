# Anime API
## Description
Technical challenge for an anime library created using Clean Architecture.

## Components

### Class Libraries
 - Animes.Application
 - Animes.Domain
 - Animes.Infra.Data
 - Animes.Infrastructure.IoC

### Web Application
 - Animes.Web.Mvc

### XUnit
 - Animes.Application.Test
 - Animes.Domain.Test

## Technologies and Tools

### Database Used for Testing
 - 10.4.32-MariaDB

### Authentication Type
 - JWT

### Frameworks
 - dotNet 8
 - Entity Framework Core 8

### Documentation
 - Swagger

## Diagrams

### Class
 - ![<Diagram 1>](Docs/Diagrams/d1-classe.png)

## Instructions

### Setup
 - Go to Animes.Web.Mvc/appsettings.json
 - Change the DefaultConnection value to match your MySQL or MariaDB database
 - Use the Entity Framework Core migration command in the Animes.Infra.Data project:
 
       dotnet ef database update --startup-project ../Animes.Web.Mvc --project .

 - In Animes.Web.Mvc, use the command:

       dotnet run

## References
 - https://www.macoratti.net/20/10/aspnc_impclean1.htm
 - https://medium.com/@microclip.lakeesha/asp-net-core-6-restful-web-api-with-mariadb-9cdb48d46b9c