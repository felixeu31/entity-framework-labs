# Documentation
[Npgsql Entity Framework Core Provider](https://www.npgsql.org/efcore/index.html)

# Journal

I followed this microsoft [tutorial](https://docs.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli) for creating a simple console application with a DB context and performing some operations

## Create console app

## Add package Npgsql

Version installed: 6.0.5 (last stable at 07/2022)

Installing the nugget Npgsql.EntityFrameworkCore.PostgreSQL another dependencies are installed:

- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Abstractions
- Microsoft.EntityFrameworkCore.Relational
- Npgsql

## Add BloggingContext
## Add Program

In this project a new instance of the DB Context is created directly without DI

## Create DB

With de psql shell I created a new DB

```postgresql
Server [localhost]:
Database [postgres]:
Port [5432]:
Username [postgres]:
Contraseña para usuario postgres:
psql (14.4)
ADVERTENCIA: El código de página de la consola (850) difiere del código
            de página de Windows (1252).
            Los caracteres de 8 bits pueden funcionar incorrectamente.
            Vea la página de referencia de psql «Notes for Windows users»
            para obtener más detalles.
Digite «help» para obtener ayuda.

postgres=# CREATE DATABASE postgresgetstarted;
CREATE DATABASE
postgres=#
```

## Add ef design package

```
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
```
## Initialize and Run migrations

```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Debug console app

Debugging the application I can see the actual operations performing to the DB