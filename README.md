# monthly-costs

Fixed monthly costs microservice.

---

## Table of contents

- [Overview](#overview)
- [Features](#features)
- [Architecture & project structure](#architecture--project-structure)
- [Technologies](#technologies)
- [Requirements](#requirements)
- [Getting started](#getting-started)
  - [Clone the repo](#clone-the-repo)
  - [Run locally (dotnet)](#run-locally-dotnet)
  - [Run with Docker](#run-with-docker)
- [Configuration](#configuration)
- [API documentation](#api-documentation)
- [Database migrations](#database-migrations)
- [Running tests](#running-tests)
- [Contributing](#contributing)
- [License & contact](#license--contact)

---

## Overview

monthly-costs is a .NET 7 microservice that manages fixed monthly costs and their values. It follows a layered architecture (Presentation, Application, Domain, Infrastructure) and includes both SQL and NoSQL repositories, API versioning, Swagger documentation, health checks and a simple message-bus wiring.

Repository: https://github.com/otaviano/monthly-costs

---

## Features

- REST API (ASP.NET Core)
- API versioning
- Swagger/OpenAPI documentation with enum schema customization
- Health checks
- SQL Server and MongoDB repository implementations
- Migrations are executed on startup
- AutoMapper profiles for domain <-> view model mapping
- Unit tests scaffolded (xUnit / AutoFixture / NSubstitute style helpers)
- Dockerfile for containerized execution

---

## Architecture & project structure

Top-level source tree (important projects/folders):

- src/01 - Presentation/MonthlyCosts.API
  - ASP.NET Core Web API project, Program.cs, Dockerfile
- src/02 - Application/MonthlyCost.Application
  - Application services, view models, AutoMapper profiles
- src/03 - Domain
  - Domain entities, events, settings (e.g. SwaggerSettings)
- src/04 - Infrastructure
  - IoC/Startup extensions, repositories (SqlServer & MongoDB), Swagger customization, message broker wiring
- src/tests/MonthlyCosts.UnitTests
  - Unit tests and test helpers

This solution uses the usual layered approach:
- Presentation (controllers) -> Application (use cases / services) -> Domain (entities, enums, events) -> Infrastructure (repositories, migrations, message bus, IoC)

---

## Technologies

- .NET 7 (ASP.NET Core)
- AutoMapper
- Swashbuckle/Swagger
- Humanizer (used to produce friendly enum values)
- MongoDB (NoSQL repository)
- SQL Server (SQL repository)
- Fluent migrations (migrations runner used on startup)
- Docker

---

## Requirements

- .NET 7 SDK
- (Optional) Docker
- If using persistence:
  - SQL Server available and reachable (for SQL repository)
  - MongoDB available and reachable (for NoSQL repository)

---

## Getting started

### Clone the repo

```bash
git clone https://github.com/otaviano/monthly-costs.git
cd monthly-costs
```

### Run locally (dotnet)

1. Open a terminal and navigate to the API project:

```bash
cd "src/01 - Presentation/MonthlyCosts.API"
```

2. Restore and run:

```bash
dotnet restore
dotnet build
dotnet run
```

By default the app will start and map controllers. Migrations are configured to run at startup (see Configuration section below). Use the Swagger UI to explore the available endpoints.

### Run with Docker

From repository root (or the API project folder) you can build the Docker image and run it:

```bash
# from repo root
docker build -f "src/01 - Presentation/MonthlyCosts.API/Dockerfile" -t monthly-costs:local .

# run the container (map ports as needed)
docker run --rm -p 80:80 -e ASPNETCORE_ENVIRONMENT=Development monthly-costs:local
```

Adjust environment variables / connection strings as described in Configuration.

---

## Configuration

The API reads configuration from `appsettings.json` and environment variables.

Important configuration areas:
- SwaggerSettings (section name: `SwaggerSettings`) — contains Swagger URL template, name and description. The project expects this section to exist; missing it will throw at startup.
- Connection strings for:
  - SQL Server (used by the SQL repository and migrations)
  - MongoDB (used by NoSQL repository)
- Message broker settings — the project wires either an in-memory broker or a configured broker through the IoC extensions.

Before running in an environment with real persistence, update `appsettings.json` or provide environment variables for the connection strings and other required settings.

Example (appsettings.json partial placeholders):

```json
{
  "SwaggerSettings": {
    "Url": "/swagger/{0}/swagger.json",
    "Name": "monthly-costs",
    "Description": "Monthly Costs API"
  },
  "ConnectionStrings": {
    "SqlServer": "Server=...;Database=...;User Id=...;Password=...;",
    "MongoDb": "mongodb://user:pass@host:port/database"
  }
}
```

---

## API documentation

The project configures Swagger/OpenAPI and API versioning. When the API is running, the Swagger UI will be available (by default) at:

- http://localhost:{port}/swagger

Use the UI to inspect available versions and endpoints. The project also registers a schema filter to present enum values (PaymentMethod) as friendly strings.

---

## Database migrations

Migrations are configured to run at startup via the migration runner. On application startup the code executes migration listing and `MigrateUp()`; if an error occurs it attempts to rollback.

Ensure the SQL Server connection string is valid before starting the app in an environment where migrations should be applied.

---

## Running tests

Unit tests are located under `src/tests/MonthlyCosts.UnitTests`. Run them with:

```bash
dotnet test src/tests/MonthlyCosts.UnitTests
```

The test project contains helpers using AutoFixture, AutoNSubstitute and AutoMapper profiles to make unit testing easier.

---

## Contact
- Maintainer / author: [otaviano](https://github.com/otaviano)

If you need help or want to discuss architecture, open an issue in the repository.
