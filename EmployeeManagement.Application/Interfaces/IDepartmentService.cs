using EmployeeManagement.Application.DTOs;

namespace EmployeeManagement.Application.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentDto>> GetAllAsync();
    Task<DepartmentDto?> GetByIdAsync(int id);
    Task<DepartmentDto> CreateAsync(DepartmentCreateDto createDto);
    Task<DepartmentDto?> UpdateAsync(int id, DepartmentUpdateDto updateDto);
    Task<bool> DeleteAsync(int id);
}
