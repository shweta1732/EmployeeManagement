using AutoMapper;
using EmployeeManagement.API.Controllers;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagement.Tests
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeService> _serviceMock;
        private readonly IMapper _mapper;
        private readonly EmployeeController _controller;

        public EmployeeControllerTests()
        {
            _serviceMock = new Mock<IEmployeeService>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeReadDto>();
                cfg.CreateMap<EmployeeCreateDto, Employee>();
                cfg.CreateMap<EmployeeUpdateDto, Employee>();
            });
            _mapper = config.CreateMapper();
            _controller = new EmployeeController(_serviceMock.Object, _mapper);
        }

        [Fact]
        public async Task Get_Returns_List_Of_Employees()
        {
            var list = new List<Employee> { new Employee { Id = 1, FirstName = "A" } };
            _serviceMock.Setup(s => s.GetAllAsync()).ReturnsAsync(list);

            var result = await _controller.Get();
            Assert.IsType<List<EmployeeReadDto>>(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task Get_ById_NotFound_Returns_NotFound()
        {
            _serviceMock.Setup(s => s.GetByIdAsync(5)).ReturnsAsync((Employee)null);
            var action = await _controller.Get(5);
            Assert.IsType<NotFoundResult>(action.Result);
        }
    }
}