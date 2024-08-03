using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Employee.Models;
using Employee.Data;
using Microsoft.EntityFrameworkCore;


using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

public class HomeController : Controller
{

    private readonly ILogger<HomeController> _logger; // to initialize the default logger
    private readonly IEmployeeService _employeeService; // to initialize the service class
    private readonly IWebHostEnvironment _hostEnvironment; // to get and save the file path
    private readonly DataProtectionHelper _dataProtectionHelper; // to initialize the Encryption Decryption helper class

    public HomeController(ILogger<HomeController> logger, IEmployeeService employeeService, IWebHostEnvironment hostEnvironment, DataProtectionHelper dataProtectionHelper)
    {
        _logger = logger;
        _employeeService = employeeService;
        _hostEnvironment = hostEnvironment;
        _dataProtectionHelper = dataProtectionHelper;
    }


    
    // initialize at the start of the project . Gets all the Employee List in the database
    public async Task<IActionResult> Index(){

        IEnumerable<EmployeeModel> objEmployeeList =  await _employeeService.GetAllEmployeesAsync();
        return View(objEmployeeList);
    }



    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }


    // To Create a new employee data
    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmployeeModel obj, IFormFile photo, IFormFile signature)
    {

        try
        {
            // to check if the employee id already exits or not
            bool exists  =  await _employeeService.EmployeeExistsAsync(obj.ErpCifNo);

            if(exists){

                TempData["errorMessage"] = "This Erp No is already occupied by an Employee ";
                ModelState.Clear();
                return View();

            }

            // gets the file folder path
            string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "uploads", obj.ErpCifNo);

            if(photo != null) {

                // File Helper class to handle the file
                obj.PhotoPath = await FileHelper.SaveFileAsync(photo, uploadsFolder);
                
                if(obj.PhotoPath == null){
                    return View(obj);
                }
            }
            if(signature != null) {

                obj.SignaturePath = await FileHelper.SaveFileAsync(signature, uploadsFolder);
                if(obj.SignaturePath == null){
                    return View(obj);
                }
            }

            if(ModelState.IsValid){

                // Calls the Data Protection Helper class to Encrypt the data
                obj.BankAccountNo = _dataProtectionHelper.Encrypt(obj.BankAccountNo);
                obj.PassportNumber = _dataProtectionHelper.Encrypt(obj.PassportNumber);


                // creates an employee
                await _employeeService.AddEmployeeAsync(obj);
                TempData["successMessage"] = "A new Employee Data Created Successfully";
                ModelState.Clear();
                return RedirectToAction(nameof(Index));

            }else{
                TempData["errorMessage"] = "Model State is invalid";
                return View();
            }
        }
        catch (System.Exception ex)
        {
            
            TempData["errorMessage"] = ex.Message;
            return View();
        }
        
    }


    
    [HttpGet]
    public async Task<IActionResult> Edit(string? id)
    {
        if(id == null){
            return RedirectToAction(nameof(Index));
        }

        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        
        if (employee != null)
        {
            // To Decrypt data to show on the form if exits
            employee.BankAccountNo = _dataProtectionHelper.Decrypt(employee.BankAccountNo);
            employee.PassportNumber = _dataProtectionHelper.Decrypt(employee.PassportNumber);
            return View(employee);
        } 

        TempData["errorMessage"] = $"Employee details not found with Id : {id}";

        return RedirectToAction(nameof(Index));
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EmployeeModel employee)
    {

            if(ModelState.IsValid){

                try
                {

                    // Encrypt sensitive data to save them back
                    employee.BankAccountNo = _dataProtectionHelper.Encrypt(employee.BankAccountNo);
                    employee.PassportNumber = _dataProtectionHelper.Encrypt(employee.PassportNumber);
                    var updatedEmployee = await _employeeService.UpdateEmployeeAsync(employee);
                    if (updatedEmployee == null)
                    {
                        TempData["errorMessage"] = "Employee not found.";
                        
                    }

                    TempData["successMessage"] = "Employee updated successfully.";
                    return RedirectToAction(nameof(Index));
                    
                }
                catch (System.Exception ex)
                {           
                    TempData["errorMessage"] = ex.Message;
                    return View();
        
                }
                // return RedirectToAction(nameof(Index));
            }else{
                TempData["errorMessage"] = "Model State is invalid";
                return View(employee);
            }
       
        
    }

    [HttpPost]
    public async Task<IActionResult> Delete(EmployeeModel employee)
    {

        // To delete the file 
        var result = await _employeeService.DeleteEmployeeAsync(employee);
        if (!result)
        {
            TempData["errorMessage"] = "Failed to delete the employee.";
            return RedirectToAction(nameof(Index));
        }

        // if delete successful then delete the corresponding file and folder
        // Construct the folder path
        string folderPath = Path.Combine(_hostEnvironment.WebRootPath, "uploads", employee.ErpCifNo);

        // Delete the folder
        bool isDeleted = await FileHelper.DeleteFolderAsync(folderPath);
        if(!isDeleted){
            TempData["errorMessage"] = "Can not delete the Images.";
        }
        TempData["successMessage"] = "Employee deleted successfully.";
        return RedirectToAction(nameof(Index));
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
