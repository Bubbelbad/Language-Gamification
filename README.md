# Gamification 

This is an application for learning by making and playing quiz, made with Clean Architecture with CQRS.

Featuring: 

- **CQRS with MediatR**: Implements the Command Query Responsibility Segregation (CQRS) pattern using MediatR.
- **GitHub Actions**: Automates your CI/CD pipeline with GitHub Actions for building, testing, and deploying your application.
- **Test Coverage**: Ensures code quality and reliability with comprehensive test coverage using NUnit and FluentAssertions.
- **ValidationBehavior with FluentValidation**: Integrates FluentValidation to provide a clean and extensible way to handle validation logic within your application.
- **OperationResult:** Standardizes the way results are returned from operations, encapsulating success and failure states along with relevant messages.

<br>

## Get Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) 
- [Visual Studio](https://visualstudio.microsoft.com/)
  
<br>

### Step 1: Clone the Repository
Clone the repository to your local machine:

```bash
  git clone https://github.com/Bubbelbad/Language-Gamification.git Language-Gamification
```

<br>

### Step 2: Restore Dependencies
Restore the project dependencies using the .NET CLI:

```bash
  dotnet restore
```
<br>

### Step 3: Configure the Database
1. **Create a new `appsettings.Development.json`** in API layer: Use the existing template to configure your connection string from `appsettings.Development.json.template`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YourServer\\SQLEXPRESS; Database=YourDatabaseName; Trusted_Connection=true; TrustServerCertificate=true;"
  },
  "JwtSettings": {
    "SecretKey": "your_super_long_very_secret_key_etc"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

<br>

### Step 4: Apply Migrations
Use the following commands in the Developer PowerShell to update the database to the latest migration: 

```bash
  $env:ASPNETCORE_ENVIRONMENT = "Development"
```
```bash
  cd ./Infrastructure
```
```bash
  dotnet ef database update --startup-project ../API
```
<br>

### Step 4: Verify the Setup
Open your browser and navigate to the User/GetAllUsers endpoint to verify that it returns an empty list, or create a new user to test it. 

