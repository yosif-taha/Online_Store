# Online Store API

A .NET 8 Web API for an online food-ordering store. The API provides a product catalog, Redis-backed shopping baskets, account management, order creation, delivery methods, and Stripe PaymentIntent integration.

## Contents

- [Features](#features)
- [Architecture](#architecture)
- [Project structure](#project-structure)
- [Technology stack](#technology-stack)
- [Prerequisites](#prerequisites)
- [Configuration](#configuration)
- [Run locally](#run-locally)
- [Database and seed data](#database-and-seed-data)
- [API reference](#api-reference)
- [Authentication](#authentication)
- [Checkout and payments](#checkout-and-payments)
- [Development notes](#development-notes)

## Features

- Browse products with optional brand/type filters, text search, sorting, and pagination.
- Retrieve product details, product brands, and product types.
- Create, retrieve, update, and delete time-limited shopping baskets stored in Redis.
- Register and sign in users with ASP.NET Core Identity and JWT bearer tokens.
- Retrieve and update the authenticated user's address.
- Retrieve delivery methods and create orders from a basket.
- Revalidate basket item prices against the product catalog during order and payment processing.
- Create or update Stripe PaymentIntents and return their client secrets through the basket response.
- Cache product-controller responses in Redis.
- Apply pending database migrations and seed catalog and delivery data on application startup.

## Architecture

The solution is organized as a layered, Onion-style application. The web host composes the layers through dependency injection; controllers delegate to application services, which depend on domain contracts and infrastructure implementations.

```text
Online_Store.Web (API host)
  |
  +-- Online_Store.Presentation (controllers and filters)
  |     |
  |     +-- Online_Store.Services (application services)
  |             |
  |             +-- Online_Store.Domain (entities and contracts)
  |                     |
  |                     +-- Online_Store.Persistence (EF Core and Redis)
  |
  +-- Online_Store.Shared (DTOs, pagination, errors, options)
```

Key implementation patterns include a service layer, generic repository and Unit of Work, specifications for query composition, AutoMapper profiles, dependency injection, and centralized exception handling middleware.

## Project structure

| Path | Purpose |
| --- | --- |
| `Online_Store.Web` | ASP.NET Core host, startup pipeline, configuration, middleware, Swagger setup, and static files. |
| `Infrastructure/Online_Store.Presentation` | API controllers and Redis response-cache action filter. |
| `Core/Online_Store.Services` | Product, basket, authentication, order, payment, and cache application services; mappings and specifications. |
| `Core/Online_Store.Services.Abstractions` | Service interfaces and service-manager contract. |
| `Core/Online_Store.Domain` | Entities, repository contracts, and domain exceptions. |
| `Infrastructure/Online_Store.Persistence` | EF Core contexts, mappings, migrations, database initialization, SQL repositories, and Redis repositories. |
| `Online_Store.Shared` | Request/response DTOs, pagination, error responses, and JWT options. |

## Technology stack

- .NET 8 and ASP.NET Core Web API
- Entity Framework Core 8 with SQL Server
- ASP.NET Core Identity and JWT Bearer authentication
- StackExchange.Redis for baskets and response caching
- Stripe.net for PaymentIntent integration
- AutoMapper for DTO mapping
- Swashbuckle / Swagger for development API documentation

## Prerequisites

- [.NET SDK 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server reachable by the API
- Redis reachable by the API
- A Stripe account and secret API key when using payment-intent endpoints

## Configuration

The API reads its settings from the standard ASP.NET Core configuration system. Do not commit production credentials, JWT signing keys, connection strings, Stripe keys, or webhook secrets.

Provide the following settings through environment variables, a local secrets provider, or an environment-specific configuration file that is excluded from source control:

| Setting | Purpose |
| --- | --- |
| `ConnectionStrings__DefaultConnection` | SQL Server connection for catalog, orders, and delivery data. |
| `ConnectionStrings__IdentityConnection` | SQL Server connection for ASP.NET Core Identity data. |
| `ConnectionStrings__RedisConnection` | Redis server connection. |
| `JwtOptions__issuer` | JWT issuer. |
| `JwtOptions__auduance` | JWT audience (the spelling reflects the current configuration binding). |
| `JwtOptions__key` | JWT signing key. |
| `JwtOptions__durationindays` | JWT lifetime in days. |
| `BaseUrl` | Base URL used when mapping product image URLs. |
| `StripeOptions__Secretkey` | Stripe secret API key used to create and update PaymentIntents. |

Example PowerShell configuration for a local session (use your own values):

```powershell
$env:ConnectionStrings__DefaultConnection = "<sql-server-connection>"
$env:ConnectionStrings__IdentityConnection = "<identity-sql-server-connection>"
$env:ConnectionStrings__RedisConnection = "<redis-connection>"
$env:JwtOptions__issuer = "https://localhost:<port>"
$env:JwtOptions__auduance = "<client-audience>"
$env:JwtOptions__key = "<secure-development-signing-key>"
$env:JwtOptions__durationindays = "2"
$env:BaseUrl = "https://localhost:<port>"
$env:StripeOptions__Secretkey = "<stripe-secret-key>"
```

The repository contains a development-oriented `appsettings.json`. Review and replace its environment-specific values before using the application outside local development.

## Run locally

From the repository root:

```powershell
dotnet restore
dotnet build .\Online_Store.sln
Set-Location .\Online_Store.Web
dotnet run
```

The launch profile opens Swagger in the Development environment. With the checked-in launch settings, the HTTPS profile uses `https://localhost:7103` and the HTTP profile uses `http://localhost:5088`.

Swagger is registered only when `ASPNETCORE_ENVIRONMENT` is `Development`. When running with the HTTPS development profile, browse to:

```text
https://localhost:7103/swagger
```

## Database and seed data

At startup, the application:

1. Applies pending EF Core migrations for the store and identity databases.
2. Seeds product brands, product types, products, and delivery methods from JSON files under `Infrastructure/Online_Store.Persistence/Data/Data Seeding` when the corresponding tables are empty.
3. Seeds the `SuperAdmin` and `Admin` roles if no roles exist.

Ensure both SQL Server databases are available and configured before starting the API. EF Core migration files are maintained under `Infrastructure/Online_Store.Persistence/Data/Migrations` and `Infrastructure/Online_Store.Persistence/Identity/Migrations`.

## API reference

In Development, Swagger provides the authoritative interactive API contract at `/swagger`.

### Products

| Method | Route | Description |
| --- | --- | --- |
| `GET` | `/api/Products` | Lists products. Supports `BrandId`, `TypeId`, `Search`, `Sort`, `PageSize`, and `PageIndex` query parameters. |
| `GET` | `/api/Products/{id}` | Gets a product by ID. |
| `GET` | `/api/Products/brands` | Lists product brands. |
| `GET` | `/api/Products/types` | Lists product types. |

Example product query:

```http
GET /api/Products?Search=chicken&Sort=priceasc&PageIndex=1&PageSize=5
```

### Baskets

| Method | Route | Description |
| --- | --- | --- |
| `GET` | `/api/Baskets?id={id}` | Retrieves a basket by ID. |
| `POST` | `/api/Baskets` | Creates or replaces a basket. Stored baskets expire after one day. |
| `DELETE` | `/api/Baskets?id={id}` | Deletes a basket. |

The basket request supports an ID, items, an optional delivery method ID, and payment-intent fields. Refer to Swagger for the complete request schema.

### Authentication and user profile

| Method | Route | Authentication | Description |
| --- | --- | --- | --- |
| `POST` | `/api/Auth/login` | No | Signs in a user and returns a JWT response. |
| `POST` | `/api/Auth/Register` | No | Registers a user and returns a JWT response. |
| `GET` | `/api/Auth/EmailExists?email={email}` | No | Checks whether an email is registered. |
| `GET` | `/api/Auth/CurrentUser` | Bearer token | Retrieves the current user and a refreshed JWT response. |
| `GET` | `/api/Auth/Address` | Bearer token | Retrieves the current user's address. |
| `PUT` | `/api/Auth/Address` | Bearer token | Updates the current user's address. |

### Orders and payments

| Method | Route | Authentication | Description |
| --- | --- | --- | --- |
| `POST` | `/api/Orders` | Bearer token | Creates an order from a basket and shipping address. |
| `GET` | `/api/Orders/{id}` | Not enforced by controller attribute | Retrieves a specific user's order in the service layer. |
| `GET` | `/api/Orders` | Not enforced by controller attribute | Retrieves orders for the current user in the service layer. |
| `GET` | `/api/Orders/DeliveryMethods` | No | Lists delivery methods. |
| `POST` | `/api/Payments/{basketId}` | Bearer token | Creates or updates a Stripe PaymentIntent for a basket. |
| `POST` | `/api/Payments/webhook` | No | Receives Stripe webhook events. |

## Authentication

Authenticated endpoints expect a bearer token:

```http
Authorization: Bearer <token>
```

Tokens are issued after registration and login. They include email, display-name, and any assigned Identity roles as claims. Roles are seeded, but the current controllers do not enforce role- or policy-based authorization.

## Checkout and payments

1. Create or update a basket and choose a delivery method.
2. Call `POST /api/Payments/{basketId}` with a bearer token. The API reloads item prices, calculates delivery cost, creates or updates the Stripe PaymentIntent, and persists the PaymentIntent data in Redis.
3. Use the returned client secret with a compatible Stripe client integration to collect payment details.
4. Call `POST /api/Orders` with the basket ID, shipping address, and delivery-method ID to create an order.

The webhook endpoint parses Stripe success and failure events, but it does not currently persist an order-status transition. Treat webhook-driven order-status updates as incomplete functionality.

## Development notes

- Product responses are cached in Redis for 50 seconds by the `ProductsController` cache filter.
- Product image files are served from `Online_Store.Web/wwwroot/images/products`.
- CORS currently allows any origin, header, and method. Restrict this policy for production deployments.
- Global error middleware maps custom not-found, bad-request, and unauthorized exceptions to JSON responses.
- No automated test project or CI workflow is included in this repository.
- The repository contains no frontend application; integrate the API with a separate client as needed.
