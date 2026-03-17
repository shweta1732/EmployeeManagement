using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
    {
        var departments = await _departmentRepository.GetAllAsync();
        return departments.Select(d => new DepartmentDto
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description
        }).ToList();
    }

    public async Task<DepartmentDto?> GetByIdAsync(int id)
    {
        var department = await _departmentRepository.GetByIdAsync(id);
        if (department == null)
            return null;

        return new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description
        };
    }

    public async Task<DepartmentDto> CreateAsync(DepartmentCreateDto createDto)
    {
        var department = new Department
        {
            Name = createDto.Name,
            Description = createDto.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _departmentRepository.AddAsync(department);
        await _departmentRepository.SaveChangesAsync();

        return new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description
        };
    }

    public async Task<DepartmentDto?> UpdateAsync(int id, DepartmentUpdateDto updateDto)
    {
        var department = await _departmentRepository.GetByIdAsync(id);
        if (department == null)
            return null;

        department.Name = updateDto.Name;
        department.Description = updateDto.Description;
        department.UpdatedAt = DateTime.UtcNow;

        _departmentRepository.Update(department);
        await _departmentRepository.SaveChangesAsync();

        return new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var department = await _departmentRepository.GetByIdAsync(id);
        if (department == null)
            return false;

        _departmentRepository.Delete(department);
        await _departmentRepository.SaveChangesAsync();

        return true;
    }
}

