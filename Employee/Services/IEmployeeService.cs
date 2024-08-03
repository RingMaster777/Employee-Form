using Employee.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync();
    Task<EmployeeModel> GetEmployeeByIdAsync(string id);
    Task<EmployeeModel> AddEmployeeAsync(EmployeeModel employee);
    Task<EmployeeModel> UpdateEmployeeAsync(EmployeeModel employee);
    Task<bool> DeleteEmployeeAsync(EmployeeModel employee);
    Task<bool> EmployeeExistsAsync(string erpCifNo);
}
