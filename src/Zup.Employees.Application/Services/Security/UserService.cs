using Zup.Employees.Domain.Employees.Interfaces;
using Zup.Employees.Security.Domain.DTOs;
using Zup.Employees.Security.Domain.Interfaces;

namespace Zup.Employees.Application.Services.Security;

public class UserService : IUserService
{
    private readonly IEmployeeSearcher _employeeSearcher;

    public UserService(IEmployeeSearcher employeeSearcher)
    {
        _employeeSearcher = employeeSearcher;
    }

    public async Task<UserDTO?> GetUserAsync(string email, string passwordHashed)
    {
        var employee = await _employeeSearcher.GetForLoginAsync(email, passwordHashed);

        if (employee == null)
        {
            return default;
        }

        var leader = employee.Leader;

        return new()
        {
            Id = employee.Id,
            Name = employee.Name,
            Surname = employee.Surname,
            Email = employee.Email,
            LeaderId = employee.Leader?.Id ?? Guid.Empty,
            LeaderName = leader != null ? $"{leader.Name} {leader.Surname}" : string.Empty,
        };
    }
}
