using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Employee.Models;


namespace Employee.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeService _employeeService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ILogger<HomeController> logger, IEmployeeService employeeService, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _employeeService = employeeService;
            _hostEnvironment = hostEnvironment;
        }

        // Gets all employees from database
        public async Task<IActionResult> Index()
        {
            IEnumerable<EmployeeModel> objEmployeeList = await _employeeService.GetAllEmployeesAsync();
            return View(objEmployeeList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create a new employee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeModel obj, IFormFile? photo, IFormFile? signature)
        {
            try
            {
                // Delegate creation (files + encryption + persistence) to service
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Model State is invalid";
                    return View(obj);
                }

                try
                {
                    await _employeeService.CreateEmployeeAsync(obj, photo, signature, _hostEnvironment.WebRootPath);
                    TempData["successMessage"] = "A new Employee Data Created Successfully";
                    ModelState.Clear();
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException iox)
                {
                    // expected business rule (e.g. duplicate Erp)
                    TempData["errorMessage"] = iox.Message;
                    return View(obj);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating employee");
                TempData["errorMessage"] = ex.Message;
                return View(obj);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var employee = await _employeeService.GetEmployeeForEditAsync(id);
            if (employee != null)
            {
                return View(employee);
            }

            TempData["errorMessage"] = $"Employee details not found with Id : {id}";
            return RedirectToAction(nameof(Index));
        }

        // Update employee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedEmployee = await _employeeService.UpdateEmployeeWithFilesAsync(employee, null, null, _hostEnvironment.WebRootPath);
                    if (updatedEmployee == null)
                    {
                        TempData["errorMessage"] = "Employee not found.";
                        return RedirectToAction(nameof(Index));
                    }

                    TempData["successMessage"] = "Employee updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating employee");
                    TempData["errorMessage"] = ex.Message;
                    return View(employee);
                }
            }
            else
            {
                TempData["errorMessage"] = "Model State is invalid";
                return View(employee);
            }
        }

        // Delete employee
        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeModel employee)
        {
            try
            {
                // Delete employee from database
                var result = await _employeeService.DeleteEmployeeAsync(employee);
                if (!result)
                {
                    TempData["errorMessage"] = "Failed to delete the employee.";
                    return RedirectToAction(nameof(Index));
                }

                // Delete associated files and folder (if ErpCifNo available)
                var erp = employee?.ErpCifNo;
                if (!string.IsNullOrWhiteSpace(erp))
                {
                    bool isDeleted = await _employeeService.DeleteEmployeeFilesAsync(erp, _hostEnvironment.WebRootPath);
                    if (!isDeleted)
                    {
                        _logger.LogWarning("Could not delete files for employee {EmployeeId}", erp);
                    }
                }

                TempData["successMessage"] = "Employee deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting employee");
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }


}