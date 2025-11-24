# Secure User Identity Service (SUIS)

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![Angular](https://img.shields.io/badge/Angular-16%2B-DD0031?style=flat-square&logo=angular&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?style=flat-square&logo=microsoft-sql-server&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-blue.svg?style=flat-square)

**Developed by: Ans Abdullah Malik**

A production-ready, full-stack Authentication and Authorization system architected with .NET 9 and Angular. This solution provides a secure, decoupled foundation for identity management using JWT (JSON Web Tokens) and Role-Based Access Control (RBAC), migrated from in-memory storage to a persistent SQL Server database using Entity Framework Core.

## Key Features

* **Clean Architecture:** Implements a strictly decoupled layered architecture (Core, Infrastructure, API) to ensure maintainability, testability, and separation of concerns.
* **Secure Authentication:** Features a stateless JWT implementation with custom claims, secure signing, and expiration handling.
* **Role-Based Access Control (RBAC):** granular permission management system distinguishing between standard Users and Administrators.
* **Persistent Storage:** Fully integrated with SQL Server using Entity Framework Core 9.0, replacing previous in-memory implementations for production reliability.
* **Result Pattern:** Utilizes a generic Result wrapper for consistent error handling and API response standardization.
* **CORS Configuration:** Securely configured Cross-Origin Resource Sharing to facilitate seamless communication between the Angular SPA and .NET Web API.

## Technology Stack

### Backend
* **.NET 9 SDK** (C# 13)
* **ASP.NET Core Web API**
* **Entity Framework Core 9.0** (Code-First approach)
* **SQL Server** (Relational Database)
* **Swagger/OpenAPI** (API Documentation)

### Frontend
* **Angular** (Single Page Application)
* **TypeScript**
* **Bootstrap 5** (Responsive UI)
* **RxJS** (Reactive Extensions for state management)

## Project Structure

The solution adheres to clean architecture principles:

src/
├── Auth.Api/             # Application entry point, Controllers, and DI Configuration
├── Auth.Core/            # Domain Entities, Interfaces, and shared Result logic
├── Auth.Infrastructure/  # Database Context, EF Core Migrations, and Repository implementations
└── Auth.Frontend/        # Angular Client Application

## Getting Started

Follow these instructions to set up the project locally for development and testing.

### Prerequisites
* .NET SDK 9.0
* Node.js (LTS version)
* SQL Server (Express, Developer, or LocalDB)

### Step 1: Backend Setup
1.  Clone the repository.
2.  Navigate to the API directory:
    cd src/Auth.Api
3.  Configure the database connection string in `appsettings.json` to point to your local SQL Server instance.
4.  Apply database migrations to create the schema:
    dotnet ef database update --project ../Auth.Infrastructure
5.  Start the API:
    dotnet run

The API will initialize and listen on the configured local port (e.g., http://localhost:5166).

### Step 2: Frontend Setup
1.  Navigate to the frontend directory:
    cd ../Auth.Frontend
2.  Install dependencies:
    npm install
3.  Launch the application:
    ng serve --open

The application will automatically open in your default browser at http://localhost:4200.

## Testing & Verification

* **Swagger UI:** Access the interactive API documentation at `/swagger` on the API port.
* **Registration:** Use the `/register` route on the frontend to create a new user entity.
* **Persistence Check:** Verify data integrity by querying the `Users` table in SQL Server after restarting the application.
* **Authentication:** Log in with valid credentials to receive a JWT and access protected routes on the dashboard.

## Configuration Notes

* **JWT Secret:** The `Jwt:Key` in `appsettings.json` is a placeholder for development environments. For production deployment, ensure this is replaced with a secure, high-entropy key managed via environment variables or a secrets manager.
* **CORS:** The current configuration explicitly trusts `localhost:4200`. Update the `CORS:urls` setting in `appsettings.json` to reflect your production client domain.

## Contributing

Contributions are welcome. Please fork the repository and submit a Pull Request for review.

## License

Distributed under the MIT License. See `LICENSE.md` for more information.