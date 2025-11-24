using Employee.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEmployeeService
{
    // Core CRUD operations
    Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync();
    Task<EmployeeModel?> GetEmployeeByIdAsync(string id);
    Task<EmployeeModel> AddEmployeeAsync(EmployeeModel employee);
    Task<EmployeeModel?> UpdateEmployeeAsync(EmployeeModel employee);
    Task<bool> DeleteEmployeeAsync(EmployeeModel employee);
    Task<bool> EmployeeExistsAsync(string erpCifNo);

    // File operations
    Task<string?> SavePhotoAsync(IFormFile? photo, string erpCifNo, string webRootPath);
    Task<string?> SaveSignatureAsync(IFormFile? signature, string erpCifNo, string webRootPath);
    Task<bool> DeleteEmployeeFilesAsync(string erpCifNo, string webRootPath);

    // Encryption operations
    string EncryptSensitiveData(string? data);
    string DecryptSensitiveData(string? data);

    // Higher-level operations that encapsulate file handling and encryption
    Task<EmployeeModel> CreateEmployeeAsync(EmployeeModel employee, IFormFile? photo, IFormFile? signature, string webRootPath);
    Task<EmployeeModel?> GetEmployeeForEditAsync(string id);
    Task<EmployeeModel?> UpdateEmployeeWithFilesAsync(EmployeeModel employee, IFormFile? photo, IFormFile? signature, string webRootPath);
}

