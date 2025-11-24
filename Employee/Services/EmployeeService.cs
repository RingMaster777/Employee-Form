using Employee.Data;
using Employee.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly DataProtectionHelper _dataProtectionHelper;
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<EmployeeService> _logger;

    public EmployeeService(
        IEmployeeRepository employeeRepository,
        DataProtectionHelper dataProtectionHelper,
        ApplicationDbContext dbContext,
        ILogger<EmployeeService> logger)
    {
        _employeeRepository = employeeRepository;
        _dataProtectionHelper = dataProtectionHelper;
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync()
    {
        try
        {
            return await _employeeRepository.GetAllEmployeesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all employees");
            throw;
        }
    }

    public async Task<EmployeeModel?> GetEmployeeByIdAsync(string id)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Employee ID cannot be null or empty", nameof(id));

            return await _employeeRepository.GetEmployeeByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving employee with ID: {EmployeeId}", id);
            throw;
        }
    }

    public async Task<EmployeeModel> AddEmployeeAsync(EmployeeModel employee)
    {
        try
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee), "Employee cannot be null");

            if (string.IsNullOrWhiteSpace(employee.ErpCifNo))
                throw new ArgumentException("ERP CIF No cannot be empty", nameof(employee.ErpCifNo));

            return await _employeeRepository.AddEmployeeAsync(employee);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database error while adding employee");
            throw new InvalidOperationException("Failed to add employee. Please check the input data.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding employee with ID: {ErpCifNo}", employee?.ErpCifNo);
            throw;
        }
    }

    // High-level create that handles file saves, encryption and transaction
    public async Task<EmployeeModel> CreateEmployeeAsync(EmployeeModel employee, IFormFile? photo, IFormFile? signature, string webRootPath)
    {
        if (employee == null) throw new ArgumentNullException(nameof(employee));
        if (string.IsNullOrWhiteSpace(employee.ErpCifNo)) throw new ArgumentException("ERP CIF No cannot be empty", nameof(employee.ErpCifNo));

        // Ensure unique ErpCifNo
        if (await EmployeeExistsAsync(employee.ErpCifNo))
            throw new InvalidOperationException("This Erp No is already occupied by an Employee");

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        string? savedPhoto = null;
        string? savedSignature = null;
        try
        {
            if (photo != null)
            {
                savedPhoto = await SavePhotoAsync(photo, employee.ErpCifNo, webRootPath);
                employee.PhotoPath = savedPhoto;
            }

            if (signature != null)
            {
                savedSignature = await SaveSignatureAsync(signature, employee.ErpCifNo, webRootPath);
                employee.SignaturePath = savedSignature;
            }

            // encrypt
            employee.BankAccountNo = EncryptSensitiveData(employee.BankAccountNo);
            employee.PassportNumber = EncryptSensitiveData(employee.PassportNumber);

            var created = await _employeeRepository.AddEmployeeAsync(employee);
            await transaction.CommitAsync();
            return created;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Error during CreateEmployeeAsync for {Erp}", employee?.ErpCifNo);
            // cleanup saved files when transaction fails
            try { if (!string.IsNullOrEmpty(savedPhoto)) await FileHelper.DeleteFolderAsync(Path.Combine(webRootPath, "uploads", employee.ErpCifNo)); } catch { }
            throw;
        }
    }

    public async Task<EmployeeModel?> UpdateEmployeeAsync(EmployeeModel employee)
    {
        try
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee), "Employee cannot be null");

            if (string.IsNullOrWhiteSpace(employee.ErpCifNo))
                throw new ArgumentException("ERP CIF No cannot be empty", nameof(employee.ErpCifNo));

            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(employee.ErpCifNo);
            if (existingEmployee == null)
            {
                _logger.LogWarning("Employee not found with ID: {ErpCifNo}", employee.ErpCifNo);
                return null;
            }

            UpdateProperties(existingEmployee, employee);
            return await _employeeRepository.UpdateEmployeeAsync(employee);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Database error while updating employee");
            throw new InvalidOperationException("Failed to update employee. Please check the input data.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating employee with ID: {ErpCifNo}", employee?.ErpCifNo);
            throw;
        }
    }

    // High-level update which may include replacing files and handles encryption
    public async Task<EmployeeModel?> UpdateEmployeeWithFilesAsync(EmployeeModel employee, IFormFile? photo, IFormFile? signature, string webRootPath)
    {
        if (employee == null) throw new ArgumentNullException(nameof(employee));
        if (string.IsNullOrWhiteSpace(employee.ErpCifNo)) throw new ArgumentException("ERP CIF No cannot be empty", nameof(employee.ErpCifNo));

        var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(employee.ErpCifNo);
        if (existingEmployee == null)
        {
            _logger.LogWarning("Employee not found with ID: {Erp}", employee.ErpCifNo);
            return null;
        }

        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            // handle files - save new ones and set paths
            if (photo != null)
            {
                var newPhoto = await SavePhotoAsync(photo, employee.ErpCifNo, webRootPath);
                existingEmployee.PhotoPath = newPhoto;
            }
            if (signature != null)
            {
                var newSignature = await SaveSignatureAsync(signature, employee.ErpCifNo, webRootPath);
                existingEmployee.SignaturePath = newSignature;
            }

            // copy other properties and encrypt
            UpdateProperties(existingEmployee, employee);
            existingEmployee.BankAccountNo = EncryptSensitiveData(existingEmployee.BankAccountNo);
            existingEmployee.PassportNumber = EncryptSensitiveData(existingEmployee.PassportNumber);

            var updated = await _employeeRepository.UpdateEmployeeAsync(existingEmployee);
            await transaction.CommitAsync();
            return updated;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Error during UpdateEmployeeWithFilesAsync for {Erp}", employee?.ErpCifNo);
            throw;
        }
    }

    // Return a decrypted employee for editing/viewing
    public async Task<EmployeeModel?> GetEmployeeForEditAsync(string id)
    {
        var employee = await GetEmployeeByIdAsync(id);
        if (employee == null) return null;

        employee.BankAccountNo = DecryptSensitiveData(employee.BankAccountNo);
        employee.PassportNumber = DecryptSensitiveData(employee.PassportNumber);
        return employee;
    }

    public async Task<bool> DeleteEmployeeAsync(EmployeeModel employee)
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            if (employee == null)
                throw new ArgumentNullException(nameof(employee), "Employee cannot be null");

            if (string.IsNullOrWhiteSpace(employee.ErpCifNo))
                throw new ArgumentException("ERP CIF No cannot be empty", nameof(employee.ErpCifNo));

            var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(employee.ErpCifNo);
            if (existingEmployee == null)
            {
                _logger.LogWarning("Employee not found for deletion with ID: {ErpCifNo}", employee.ErpCifNo);
                return false;
            }

            await _employeeRepository.DeleteEmployeeAsync(existingEmployee);
            await transaction.CommitAsync();
            _logger.LogInformation("Employee deleted successfully: {ErpCifNo}", employee.ErpCifNo);
            return true;
        }
        catch (DbUpdateException ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Database error while deleting employee");
            throw new InvalidOperationException("Failed to delete employee.", ex);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Error deleting employee with ID: {ErpCifNo}", employee?.ErpCifNo);
            throw;
        }
    }

    public async Task<bool> EmployeeExistsAsync(string erpCifNo)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(erpCifNo))
                return false;

            return await _employeeRepository.EmployeeExistsAsync(erpCifNo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if employee exists with ID: {ErpCifNo}", erpCifNo);
            throw;
        }
    }

    public async Task<string?> SavePhotoAsync(IFormFile? photo, string erpCifNo, string webRootPath)
    {
        try
        {
            if (photo == null || photo.Length == 0)
                return null;

            if (string.IsNullOrWhiteSpace(erpCifNo))
                throw new ArgumentException("ERP CIF No cannot be empty", nameof(erpCifNo));

            string uploadsFolder = Path.Combine(webRootPath, "uploads", erpCifNo);
            return await FileHelper.SaveFileAsync(photo, uploadsFolder);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving photo for employee: {ErpCifNo}", erpCifNo);
            throw;
        }
    }

    public async Task<string?> SaveSignatureAsync(IFormFile? signature, string erpCifNo, string webRootPath)
    {
        try
        {
            if (signature == null || signature.Length == 0)
                return null;

            if (string.IsNullOrWhiteSpace(erpCifNo))
                throw new ArgumentException("ERP CIF No cannot be empty", nameof(erpCifNo));

            string uploadsFolder = Path.Combine(webRootPath, "uploads", erpCifNo);
            return await FileHelper.SaveFileAsync(signature, uploadsFolder);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving signature for employee: {ErpCifNo}", erpCifNo);
            throw;
        }
    }

    public async Task<bool> DeleteEmployeeFilesAsync(string erpCifNo, string webRootPath)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(erpCifNo))
                throw new ArgumentException("ERP CIF No cannot be empty", nameof(erpCifNo));

            string folderPath = Path.Combine(webRootPath, "uploads", erpCifNo);
            bool isDeleted = await FileHelper.DeleteFolderAsync(folderPath);

            if (!isDeleted)
                _logger.LogWarning("Could not delete files for employee: {ErpCifNo}", erpCifNo);

            return isDeleted;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting files for employee: {ErpCifNo}", erpCifNo);
            throw;
        }
    }

    public string EncryptSensitiveData(string? data)
    {
        try
        {
            if (string.IsNullOrEmpty(data))
                return data ?? string.Empty;

            return _dataProtectionHelper.Encrypt(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error encrypting sensitive data");
            throw new InvalidOperationException("Failed to encrypt data.", ex);
        }
    }

    public string DecryptSensitiveData(string? data)
    {
        try
        {
            if (string.IsNullOrEmpty(data))
                return data ?? string.Empty;

            return _dataProtectionHelper.Decrypt(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error decrypting sensitive data");
            throw new InvalidOperationException("Failed to decrypt data.", ex);
        }
    }

    private void UpdateProperties(EmployeeModel existingEmployee, EmployeeModel updatedEmployee)
    {
        var properties = typeof(EmployeeModel).GetProperties();

        foreach (var property in properties)
        {
            if (!property.CanWrite || property.Name == "ErpCifNo" || property.Name == "EmployeeId")
                continue;

            var newValue = property.GetValue(updatedEmployee);
            property.SetValue(existingEmployee, newValue);
        }
    }
}
