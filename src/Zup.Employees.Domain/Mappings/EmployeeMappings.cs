using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.Employees.Entities;

namespace Zup.Employees.Domain.Mappings;

public static class EmployeeMappings
{
    public static EmployeeDTO ToDto(this Employee employee)
    {
        return new EmployeeDTO
        {
            Email = employee.Email,
            Name = employee.Name,
            Surname = employee.Surname,
            Id = employee.Id,
            IsLeader = employee.IsLeader,
            LeaderId = employee.Leader?.Id ?? Guid.Empty,
            LeaderName = employee.Leader?.Name,
            PlateNumber = employee.PlateNumber,
        };
    }
}
