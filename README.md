# Gamification 

This is an application for language learning, made with Clean Architecture with CQRS.

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
1. **Create a Database**: Create a new database in your SQL Server instance.
2. **Update `appsettings.Development.json`**: Use the following template to configure your connection string in `appsettings.Development.json`:

```csharp
{ 
  "ConnectionStrings": { 
    "Boilerplate_DefaultConnection": "Server=yourServerAddress; Database=yourDatabaseName; Trusted_Connection=true; TrustServerCertificate=true;"
  }
}
```

<br>

### Step 4: Apply Migrations
Create a new branch and use Entity Framework Core to apply migrations and create the database schema:

```bash
  git checkout -b test/new-migration-yourName
  cd ./Infrastructure
  dotnet ef migrations add InitialMigration --startup-project ../API
  dotnet ef database update --startup-project ../API
```
<br>

### Step 4: Verify the Setup
Open your browser and navigate to the appropriate endpoint to verify that the application is running correctly.

