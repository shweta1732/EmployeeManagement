using EmployeeManagement.Application.DTOs;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> AuthenticateAsync(string username, string password);
    }
}