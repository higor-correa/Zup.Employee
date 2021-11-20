using Zup.Employees.Security.Domain.DTOs;

namespace Zup.Employees.Security.Domain.Interfaces;

public interface IUserService
{
    Task<UserDTO?> GetUserAsync(string email, string passwordHashed);
}
