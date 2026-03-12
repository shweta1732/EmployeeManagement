using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Services;
using EmployeeManagement.Domain.Entities;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagement.Tests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public async Task CreateAsync_Adds_And_Returns_Employee()
        {
            var mockRepo = new Mock<IRepository<Employee>>();
            var emp = new Employee { Id = 1, FirstName = "John", LastName = "Doe" };
            mockRepo.Setup(r => r.AddAsync(emp)).Returns(Task.CompletedTask);
            mockRepo.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);

            var service = new EmployeeService(mockRepo.Object);
            var result = await service.CreateAsync(emp);

            Assert.Equal(emp, result);
            mockRepo.Verify(r => r.AddAsync(emp), Times.Once);
            mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_Returns_Employee()
        {
            var mockRepo = new Mock<IRepository<Employee>>();
            var emp = new Employee { Id = 1, FirstName = "Jane" };
            mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(emp);

            var service = new EmployeeService(mockRepo.Object);
            var result = await service.GetByIdAsync(1);

            Assert.Equal(emp, result);
        }
    }
}