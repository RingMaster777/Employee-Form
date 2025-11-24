using Xunit;
using Moq;
using Employee.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeTest
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _mockRepository;
        private readonly Mock<DataProtectionHelper> _mockDataProtection;
        private readonly EmployeeService _employeeService;

        public EmployeeServiceTests()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
            _mockDataProtection = new Mock<DataProtectionHelper>(null!);
            _employeeService = new EmployeeService(
                _mockRepository.Object,
                _mockDataProtection.Object,
                null!,
                null!);
        }

        #region GetAllEmployeesAsync Tests

        [Fact]
        public async Task GetAllEmployeesAsync_ReturnsAllEmployees_WhenDataExists()
        {
            // Arrange
            var employees = new List<EmployeeModel>
            {
                new EmployeeModel { EmployeeId = 1, ErpCifNo = "001", FirstName = "John", LastName = "Doe" },
                new EmployeeModel { EmployeeId = 2, ErpCifNo = "002", FirstName = "Jane", LastName = "Smith" }
            };
            _mockRepository.Setup(r => r.GetAllEmployeesAsync()).ReturnsAsync(employees);

            // Act
            var result = await _employeeService.GetAllEmployeesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            _mockRepository.Verify(r => r.GetAllEmployeesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllEmployeesAsync_ReturnsEmptyList_WhenNoDataExists()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetAllEmployeesAsync()).ReturnsAsync(new List<EmployeeModel>());

            // Act
            var result = await _employeeService.GetAllEmployeesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        #endregion

        #region GetEmployeeByIdAsync Tests

        [Fact]
        public async Task GetEmployeeByIdAsync_ReturnsEmployee_WhenEmployeeExists()
        {
            // Arrange
            var employee = new EmployeeModel { EmployeeId = 1, ErpCifNo = "001", FirstName = "John", LastName = "Doe" };
            _mockRepository.Setup(r => r.GetEmployeeByIdAsync("001")).ReturnsAsync(employee);

            // Act
            var result = await _employeeService.GetEmployeeByIdAsync("001");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("001", result.ErpCifNo);
            Assert.Equal("John", result.FirstName);
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_ReturnsNull_WhenEmployeeNotFound()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetEmployeeByIdAsync(It.IsAny<string>())).ReturnsAsync((EmployeeModel?)null);

            // Act
            var result = await _employeeService.GetEmployeeByIdAsync("999");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_ThrowsException_WhenIdIsNullOrEmpty()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _employeeService.GetEmployeeByIdAsync(null!));
            await Assert.ThrowsAsync<ArgumentException>(() => _employeeService.GetEmployeeByIdAsync(string.Empty));
        }

        #endregion

        #region AddEmployeeAsync Tests

        [Fact]
        public async Task AddEmployeeAsync_AddsEmployee_WhenValidDataProvided()
        {
            // Arrange
            var newEmployee = new EmployeeModel { ErpCifNo = "003", FirstName = "Bob", LastName = "Johnson" };
            _mockRepository.Setup(r => r.AddEmployeeAsync(It.IsAny<EmployeeModel>())).ReturnsAsync(newEmployee);

            // Act
            var result = await _employeeService.AddEmployeeAsync(newEmployee);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("003", result.ErpCifNo);
            _mockRepository.Verify(r => r.AddEmployeeAsync(It.IsAny<EmployeeModel>()), Times.Once);
        }

        [Fact]
        public async Task AddEmployeeAsync_ThrowsException_WhenEmployeeIsNull()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _employeeService.AddEmployeeAsync(null!));
        }

        [Fact]
        public async Task AddEmployeeAsync_ThrowsException_WhenErpCifNoIsEmpty()
        {
            // Arrange
            var employee = new EmployeeModel { FirstName = "Test", LastName = "User" };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _employeeService.AddEmployeeAsync(employee));
        }

        #endregion

        #region UpdateEmployeeAsync Tests

        [Fact]
        public async Task UpdateEmployeeAsync_UpdatesEmployee_WhenEmployeeExists()
        {
            // Arrange
            var existingEmployee = new EmployeeModel { EmployeeId = 1, ErpCifNo = "001", FirstName = "John", LastName = "Doe" };
            var updatedEmployee = new EmployeeModel { EmployeeId = 1, ErpCifNo = "001", FirstName = "John", LastName = "Smith" };

            _mockRepository.Setup(r => r.GetEmployeeByIdAsync("001")).ReturnsAsync(existingEmployee);
            _mockRepository.Setup(r => r.UpdateEmployeeAsync(It.IsAny<EmployeeModel>())).ReturnsAsync(updatedEmployee);

            // Act
            var result = await _employeeService.UpdateEmployeeAsync(updatedEmployee);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("001", result.ErpCifNo);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_ReturnsNull_WhenEmployeeNotFound()
        {
            // Arrange
            var employee = new EmployeeModel { ErpCifNo = "999", FirstName = "Unknown" };
            _mockRepository.Setup(r => r.GetEmployeeByIdAsync("999")).ReturnsAsync((EmployeeModel?)null);

            // Act
            var result = await _employeeService.UpdateEmployeeAsync(employee);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateEmployeeAsync_ThrowsException_WhenEmployeeIsNull()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _employeeService.UpdateEmployeeAsync(null!));
        }

        #endregion

        #region DeleteEmployeeAsync Tests

        [Fact]
        public async Task DeleteEmployeeAsync_DeletesEmployee_WhenEmployeeExists()
        {
            // Arrange
            var employee = new EmployeeModel { EmployeeId = 1, ErpCifNo = "001", FirstName = "John" };
            _mockRepository.Setup(r => r.GetEmployeeByIdAsync("001")).ReturnsAsync(employee);
            _mockRepository.Setup(r => r.DeleteEmployeeAsync(It.IsAny<EmployeeModel>())).Returns(Task.CompletedTask);

            // Act
            var result = await _employeeService.DeleteEmployeeAsync(employee);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(r => r.DeleteEmployeeAsync(It.IsAny<EmployeeModel>()), Times.Once);
        }

        [Fact]
        public async Task DeleteEmployeeAsync_ReturnsFalse_WhenEmployeeNotFound()
        {
            // Arrange
            var employee = new EmployeeModel { ErpCifNo = "999" };
            _mockRepository.Setup(r => r.GetEmployeeByIdAsync("999")).ReturnsAsync((EmployeeModel?)null);

            // Act
            var result = await _employeeService.DeleteEmployeeAsync(employee);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteEmployeeAsync_ThrowsException_WhenEmployeeIsNull()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _employeeService.DeleteEmployeeAsync(null!));
        }

        #endregion

        #region EmployeeExistsAsync Tests

        [Fact]
        public async Task EmployeeExistsAsync_ReturnsTrue_WhenEmployeeExists()
        {
            // Arrange
            _mockRepository.Setup(r => r.EmployeeExistsAsync("001")).ReturnsAsync(true);

            // Act
            var result = await _employeeService.EmployeeExistsAsync("001");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task EmployeeExistsAsync_ReturnsFalse_WhenEmployeeDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(r => r.EmployeeExistsAsync("999")).ReturnsAsync(false);

            // Act
            var result = await _employeeService.EmployeeExistsAsync("999");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task EmployeeExistsAsync_ReturnsFalse_WhenIdIsNullOrEmpty()
        {
            // Act
            var result1 = await _employeeService.EmployeeExistsAsync(null!);
            var result2 = await _employeeService.EmployeeExistsAsync(string.Empty);

            // Assert
            Assert.False(result1);
            Assert.False(result2);
        }

        #endregion

        #region Encryption/Decryption Tests

        [Fact]
        public void EncryptSensitiveData_EncryptsData_WhenValidStringProvided()
        {
            // Arrange
            var plainText = "1234567890";
            var encryptedText = "encrypted_value";
            _mockDataProtection.Setup(d => d.Encrypt(plainText)).Returns(encryptedText);

            // Act
            var result = _employeeService.EncryptSensitiveData(plainText);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(plainText, result);
        }

        [Fact]
        public void EncryptSensitiveData_ReturnsEmpty_WhenNullOrEmptyStringProvided()
        {
            // Act
            var result1 = _employeeService.EncryptSensitiveData(null);
            var result2 = _employeeService.EncryptSensitiveData(string.Empty);

            // Assert
            Assert.Empty(result1 ?? string.Empty);
            Assert.Empty(result2);
        }

        [Fact]
        public void DecryptSensitiveData_DecryptsData_WhenValidStringProvided()
        {
            // Arrange
            var encryptedText = "encrypted_value";
            var plainText = "1234567890";
            _mockDataProtection.Setup(d => d.Decrypt(encryptedText)).Returns(plainText);

            // Act
            var result = _employeeService.DecryptSensitiveData(encryptedText);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(plainText, result);
        }

        [Fact]
        public void DecryptSensitiveData_ReturnsEmpty_WhenNullOrEmptyStringProvided()
        {
            // Act
            var result1 = _employeeService.DecryptSensitiveData(null);
            var result2 = _employeeService.DecryptSensitiveData(string.Empty);

            // Assert
            Assert.Empty(result1 ?? string.Empty);
            Assert.Empty(result2);
        }

        #endregion
    }
}
