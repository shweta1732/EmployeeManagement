using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}