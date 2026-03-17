using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Application.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [Route("api/employees")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeReadDto>> Get()
        {
            var list = await _employeeService.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeReadDto>>(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeReadDto>> Get(int id)
        {
            var emp = await _employeeService.GetByIdAsync(id);
            if (emp == null) return NotFound();
            return _mapper.Map<EmployeeReadDto>(emp);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EmployeeReadDto>> Post(EmployeeCreateDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            var created = await _employeeService.CreateAsync(employee);
            var read = _mapper.Map<EmployeeReadDto>(created);
            return CreatedAtAction(nameof(Get), new { id = read.Id }, read);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, EmployeeUpdateDto updateDto)
        {
            if (id != updateDto.Id) return BadRequest();
            var employee = _mapper.Map<Employee>(updateDto);
            await _employeeService.UpdateAsync(employee);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeService.DeleteAsync(id);
            return NoContent();
        }
    }
}