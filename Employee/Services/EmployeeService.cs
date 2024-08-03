using Employee.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }


        // get all employee 
    public async Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync()
    {
        return await _employeeRepository.GetAllEmployeesAsync();
    }


    // get employee by Id
    public async Task<EmployeeModel> GetEmployeeByIdAsync(string id)
    {
        return await _employeeRepository.GetEmployeeByIdAsync(id);
    }


    // add new employee
    public async Task<EmployeeModel> AddEmployeeAsync(EmployeeModel employee)
    {
        return await _employeeRepository.AddEmployeeAsync(employee);
    }

    // update a employee
    public async Task<EmployeeModel> UpdateEmployeeAsync(EmployeeModel employee)
    {
        var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(employee.ErpCifNo);
        if (existingEmployee == null)
        {
            return null; // Employee not found
        }

        UpdateProperties(existingEmployee, employee);
        
        
        return await _employeeRepository.UpdateEmployeeAsync(employee);
    }

    // delete employee
     public async Task<bool> DeleteEmployeeAsync(EmployeeModel employee)
    {
        var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(employee.ErpCifNo);

        if (existingEmployee == null)
        {
            return false;
        }
        await _employeeRepository.DeleteEmployeeAsync(existingEmployee);
        return true;
    }


    // check if employee exists
    public async Task<bool> EmployeeExistsAsync(string erpCifNo)
    {
        return await _employeeRepository.EmployeeExistsAsync(erpCifNo);
    }



    private void UpdateProperties(EmployeeModel existingEmployee, EmployeeModel updatedEmployee)
    {
        var properties = typeof(EmployeeModel).GetProperties();

        foreach (var property in properties)
        {
            // Skip properties that should not be updated or are not writable
            if (!property.CanWrite || property.Name == "ErpCifNo" || property.Name == "EmployeeId") continue;

            var newValue = property.GetValue(updatedEmployee);
            property.SetValue(existingEmployee, newValue);
        }
    }
}
