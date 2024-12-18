# Boilerplate - Clean Architecture ✨

This is a boilerplate for Clean Architecture with CQRS.

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
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or any other supported database
- [Visual Studio](https://visualstudio.microsoft.com/)
  
<br>

### Step 1: Clone the Repository
Clone the repository to your local machine:

```bash
  git clone https://github.com/bubbelbad/clean-architecture-boilerplate.git cd your-repo-name
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

### Step 4: Add Your Entities
1. **Create Entity Classes**: Add your entity classes in the `Domain` or `Entities` folder. Or just use the predefined `User` entity.
2. **Update DbContext**: Update your DbContext class to include DbSet properties for your entities. For example:

```csharp
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Additional configuration
    }
}
```

<br>

### Step 5: Apply Migrations
Use Entity Framework Core to apply migrations and create the database schema:

```bash
  cd ./Infrastructure
  dotnet ef migrations add InitialMigration --startup-project ../API
  dotnet ef database update --startup-project ../API
```
<br>

### Step 6: Verify the Setup
Open your browser and navigate to the appropriate endpoint to verify that the application is running correctly.

<br>

**By following these steps, you will have your CQRS boilerplate application up and running with a configured database.**


![68747470733a2f2f692e7974696d672e636f6d2f76692f354f74556d31424c6d47302f6d617872657364656661756c742e6a7067](https://github.com/user-attachments/assets/4a66c2a8-b012-4e12-b5c6-21b51054bba2)
