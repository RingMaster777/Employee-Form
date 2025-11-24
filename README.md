# Employee Management System

## Overview

A modern ASP.NET Core MVC web application for managing employee data with secure storage, file uploads, and comprehensive data validation. Built with clean architecture principles and best practices.

## Key Features

- **Employee Management:** Create, read, update, and delete employee records
- **File Management:** Upload and manage employee photos and digital signatures
- **Security:** Encrypted storage of sensitive information (Bank Account No, Passport Number)
- **Logging:** Comprehensive request/response logging with Serilog
- **Validation:** Data annotations for robust input validation
- **Testing:** Complete unit test coverage with mocked dependencies
- **Transaction Management:** Database transaction support for delete operations

## Technology Stack

- **.NET Framework:** .NET 9.0 (Latest LTS)
- **Web Framework:** ASP.NET Core MVC
- **Database:** Entity Framework Core with SQL Server
- **Encryption:** ASP.NET Core Data Protection API
- **Logging:** Serilog with File sink
- **Testing:** xUnit + Moq
- **Languages:** C# 12+

## Prerequisites

- [.NET SDK 9.0](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/downloads/) or [Visual Studio Code](https://code.visualstudio.com/)
- [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads) (any version)

## Installation & Setup

### 1. Clone the Repository

```bash
git clone https://github.com/RingMaster777/Employee-Form.git
cd Employee-Form
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Configure Database Connection

Update `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=Employee;Integrated Security=true;TrustServerCertificate=true;"
}
```

### 4. Create Database

```bash
dotnet ef database update
```

### 5. Run the Application

```bash
dotnet run
```

The application will be available at `https://localhost:5001`

## Project Structure

```
Employee/
├── Controllers/          # MVC Controllers
│   └── HomeController.cs
├── Services/            # Business Logic Layer
│   ├── IEmployeeService.cs
│   └── EmployeeService.cs
├── Repositories/        # Data Access Layer
│   ├── IEmployeeRepository.cs
│   └── EmployeeRepository.cs
├── Models/              # Data Models
│   ├── EmployeeModel.cs
│   └── ErrorViewModel.cs
├── Data/                # Database Context
│   └── ApplicationDbContext.cs
├── Helpers/             # Utility Classes
│   ├── FileHelper.cs
│   └── DataProtectionHelper.cs
├── Middleware/          # Custom Middleware
│   └── LoggingMiddleware.cs
├── Views/               # Razor Views
└── wwwroot/             # Static Files

EmployeeTest/
├── EmployeeServiceTests.cs    # Comprehensive Unit Tests
```

## Architecture & Design Patterns

### Clean Architecture

- **Separation of Concerns:** Controller → Service → Repository → Database
- **Dependency Injection:** All dependencies injected via constructor
- **Interface-Based Design:** IEmployeeService and IEmployeeRepository abstractions

### Best Practices Implemented

- ✅ Null checking and validation
- ✅ Async/await for all I/O operations
- ✅ Transaction management for delete operations
- ✅ Comprehensive error handling with logging
- ✅ Meaningful exception messages
- ✅ Repository pattern for data access
- ✅ Service layer for business logic
- ✅ Constructor injection for testability

## Key Components

### EmployeeService

- Handles all business logic operations
- Includes transaction management for delete operations
- Encrypts/decrypts sensitive data
- Manages file uploads and deletions
- Comprehensive error handling with logging

### FileHelper

- Validates file types (jpg, jpeg, png only)
- Manages file uploads to organized folders
- Handles folder deletion with error recovery

### DataProtectionHelper

- Encrypts/decrypts using ASP.NET Core DPAPI
- Protects sensitive fields (Bank Account, Passport)

### LoggingMiddleware

- Logs all HTTP requests and response times
- Uses Serilog for structured logging

## Testing

### Run All Tests

```bash
dotnet test
```

### Unit Tests Included

- ✅ GetAllEmployeesAsync
- ✅ GetEmployeeByIdAsync
- ✅ AddEmployeeAsync
- ✅ UpdateEmployeeAsync
- ✅ DeleteEmployeeAsync
- ✅ EmployeeExistsAsync
- ✅ Encryption/Decryption
- ✅ Input Validation

## Configuration

### Logging Configuration

Configure in `appsettings.json`:

```json
"Logging": {
  "LogLevel": {
    "Default": "Information",
    "Microsoft.AspNetCore": "Warning",
    "Employee": "Information"
  }
}
```

Logs are stored in the `logs/` directory with daily rolling intervals.

### Data Protection Keys

Keys are stored in `c:\keys\` (configurable in Program.cs):

```csharp
.PersistKeysToFileSystem(new DirectoryInfo(@"c:\keys"))
.SetDefaultKeyLifetime(TimeSpan.FromDays(14))
```

## API Endpoints

### Employee Operations

- `GET /` - List all employees
- `GET /Create` - Employee creation form
- `POST /Create` - Create new employee
- `GET /Edit/{id}` - Employee edit form
- `POST /Edit` - Update employee
- `POST /Delete` - Delete employee

## Security Considerations

1. **Data Encryption:** Sensitive fields are encrypted using DPAPI
2. **File Validation:** Only image files (.jpg, .jpeg, .png) accepted
3. **SQL Injection Protection:** Entity Framework parameterized queries
4. **CSRF Protection:** ValidateAntiForgeryToken attributes
5. **Transaction Safety:** Delete operations wrapped in transactions

## Error Handling

All operations include:

- Input validation
- Null checking
- Exception logging
- User-friendly error messages
- Rollback on transaction failure

## Performance Considerations

- Async/await for non-blocking I/O
- Transaction isolation for data integrity
- Efficient logging without blocking
- Proper resource disposal

## Troubleshooting

### Database Connection Issues

- Verify connection string in `appsettings.json`
- Ensure SQL Server is running and accessible
- Check Windows Authentication or SQL Server credentials

### File Upload Errors

- Verify `wwwroot/uploads/` directory exists and is writable
- Check file size and format (jpg, jpeg, png only)

### Encryption Errors

- Ensure `c:\keys\` directory exists and is accessible
- Check DPAPI key permissions
- Verify data wasn't modified outside the application

## Future Enhancements

- [ ] API endpoints for mobile apps
- [ ] Advanced search and filtering
- [ ] Batch employee import/export
- [ ] Email notifications
- [ ] Role-based access control
- [ ] Audit logging
- [ ] Performance metrics dashboard

## License

This project is open source and available under the MIT License.

## Author

**Rashik Mahmud**

- GitHub: [@RingMaster777](https://github.com/RingMaster777)

## Support

For issues, questions, or suggestions, please create an issue in the GitHub repository.
