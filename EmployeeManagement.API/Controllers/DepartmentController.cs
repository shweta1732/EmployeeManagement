using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Controllers;

[ApiController]
[Route("api/departments")]
[Authorize]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAll()
    {
        var departments = await _departmentService.GetAllAsync();
        return Ok(departments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentDto>> GetById(int id)
    {
        var department = await _departmentService.GetByIdAsync(id);
        if (department == null)
        {
            return NotFound();
        }

        return Ok(department);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<DepartmentDto>> Create(DepartmentCreateDto createDto)
    {
        var department = await _departmentService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, DepartmentUpdateDto updateDto)
    {
        var department = await _departmentService.UpdateAsync(id, updateDto);
        if (department == null)
        {
            return NotFound();
        }

        return Ok(department);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _departmentService.DeleteAsync(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
