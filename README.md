# Employee - Form

## Overview

The Application is a web application built with ASP.NET Core MVC framework that allows users to manage employee data, including personal details, photos, and signatures. It supports CRUD (Create, Read, Update, Delete) operations for employee records.

## Features

- Create, read, update, and delete employee records.
- Upload and manage employee photos and signatures.
- Secure storage of sensitive information using encryption.
- Custom logging to monitor and troubleshoot application issues.
- Custom Helpers to deal with file uploads and data security

## Prerequisites

- [.NET SDK 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) or [Visual Studio Code](https://code.visualstudio.com/)
- A supported database system (e.g., SQL Server)

## Installation

1. **Clone the repository:**

   ```bash
   https://github.com/RingMaster777/Employee-Form.git
   ```

2. **Navigate to the project directory:**

   ```bash
   cd Employee
   ```

3. **Restore the project dependencies:**

   ```bash
   dotnet restore
   ```

4. **Update the database:**

   ```bash
   dotnet ef database update
   ```

5. **Run the application:**

   ```bash
   dotnet run
   ```

## Configuration

1. **Connection Strings:**

   Update the `appsettings.json` file with your database connection string:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=your-server;Database=your-database;User Id=your-username;Password=your-password;"
   }
   ```

2. **Data Protection:**

   Ensure that data protection keys are properly configured in the `Program.cs`:

   ```csharp
   builder.Services.AddDataProtection()
   .PersistKeysToFileSystem(new DirectoryInfo(@"c:\keys"))
   .SetDefaultKeyLifetime(TimeSpan.FromDays(14));
   ```

## Usage

- **Employee Details:**
  See the employee List on the indx field.

- **Create an Employee:**
  Navigate to the `Create` page to add a new employee. Fill out the form and upload the necessary files.

- **Edit an Employee:**
  Select an employee from the list and navigate to the `Edit` page to update their details.

- **Delete an Employee:**
  On the `Edit` page press the delete button to delete. This will also remove the associated folder and files from the server.

## Middleware

The application includes custom middleware for logging:

- **Logging Middleware:**
  Logs all incoming requests and responses to a file located in the `Logs` directory.

## Helpers

The application includes custom Helpers for file upload and data security:

- **FileHelper.cs**
  Helps to upload and delete file

- **DataProtectionHelper.cs**
  Helps to encrypt and decrypt necessary data

## Acknowledgements

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)
- [Serilog Documentation](https://serilog.net/)
