using Employee.Data;
using Employee.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;


// service class which uses IEmployeeRepository interface
public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _db;

    public EmployeeRepository(ApplicationDbContext db)
    {
        _db = db;
    }


    // to get employee list
    public async Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync()
    {
        return await _db.Employees.ToListAsync();
    }


    // to get the employee using the Id
    public async Task<EmployeeModel?> GetEmployeeByIdAsync(string id)
    {   
        return await _db.Employees.FirstOrDefaultAsync(e => e.ErpCifNo == id);
    }


    // add new employee
    public async Task<EmployeeModel> AddEmployeeAsync(EmployeeModel employee)
    {
        _db.Employees.Add(employee);
        await _db.SaveChangesAsync();
        return employee;
    }


    // update existing Employee
    public async Task<EmployeeModel> UpdateEmployeeAsync(EmployeeModel employee)
    {
        var existingEmployee = await _db.Employees.FindAsync(employee.EmployeeId);
        if (existingEmployee == null)
        {
            return null; // Handle the case where the entity is not found
        }

        // Update properties
        _db.Entry(existingEmployee).CurrentValues.SetValues(employee);

        await _db.SaveChangesAsync();
        return existingEmployee;
    }


    public async Task DeleteEmployeeAsync(EmployeeModel employee)
    {
       
        // Find the employee 
        var existingEmployee = await _db.Employees.FindAsync(employee.EmployeeId);
        
        if (existingEmployee != null)
        {
            _db.Employees.Remove(employee); //  remove it from db
            await _db.SaveChangesAsync();
        }
    }

    public async Task<bool> EmployeeExistsAsync(string erpCifNo)
    {
        return await _db.Employees.AnyAsync(e => e.ErpCifNo == erpCifNo); // to check if employee exists
    }
}
