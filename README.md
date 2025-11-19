Secure User Identity Service (SUIS)

Developed by: Ans Abdullah Malik

This repository contains a full-stack, decoupled Authentication and Authorization system built using ASP.NET Core (.NET 9) for the backend API and Angular for the Single Page Application (SPA) frontend.

It implements a modern JWT (JSON Web Token)-based authentication flow, ensuring stateless and secure identity management for any consumer application.

🚀 Key Features

Decoupled Architecture: Separate API and UI for flexible deployment.

Persistent Database: Configured for SQL Server (LocalDB for development) using Entity Framework Core.

Role-Based Access Control (RBAC): Supports user roles for differentiated authorization.

CORS Configuration: Properly configured to allow secure communication between the Angular frontend and the ASP.NET Core backend on local development ports.

🛠️ Prerequisites

Before running the application, ensure you have the following installed:

.NET SDK 9.0 (or the latest version compatible with your project).

Node.js and npm (LTS version is recommended).

[SQL Server LocalDB] (usually installed with Visual Studio or SQL Server Express).

📦 Project Structure

The solution is divided into three main folders under src:

Auth.Api: The ASP.NET Core API entry point. Handles routing, authentication middleware, and configuration.

Auth.Infrastructure: Contains database context, Entity Framework migrations, repositories, and service implementations.

Auth.Frontend: The Angular client application (SPA).

⚙️ Setup and Running Locally

Step 1: Backend Setup (Auth.Api)

Navigate to the API Directory:

cd D:\Projects\Authapi\AuthApi\src\Auth.Api


Install EF Core Tool (if necessary):
If the dotnet ef command fails, you need to install the tool (or reference it locally, as discussed):

dotnet tool install --global dotnet-ef


Ensure SQL Server Package is Installed:
If you faced errors before, use the explicit version for .NET 9.0:

dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.0-rc.2.24529.15


Apply Database Migrations:
This command reads the schema and creates the AuthDb database and its tables on your LocalDB instance:

dotnet ef database update


Run the API:

dotnet run


The API should start and listen on http://localhost:5166.

Step 2: Frontend Setup (Auth.Frontend)

Navigate to the Frontend Directory:

cd D:\Projects\Authapi\AuthApi\src\Auth.Frontend


Install Dependencies:

npm install


Run the Angular Application:

ng serve --open


The frontend should open in your browser at http://localhost:4200.

🧪 Testing and Verification

Register: Navigate to the registration page (/register) and create a new user.

Database Check: Because you have a persistent database, you can stop the API and restart it.

Login: Log in with the account you just created.

Dashboard: Successful login should redirect you to a dashboard showing the protected user information, confirming successful authentication and authorization.

🔒 Configuration Notes

Connection String: The default connection string is configured in Auth.Api/appsettings.json to use SQL Server LocalDB. This must be updated for any production or shared environment.

JWT Key: The key in Auth.Api/appsettings.json is a placeholder. It must be updated to a secure, long, randomly generated key before deployment.

CORS URLs: The CORS section is currently set to allow only localhost:4200. This must be updated to include the client's production domain when deployin