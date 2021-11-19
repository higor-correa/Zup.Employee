using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.Employees.Entities;

namespace Zup.Employees.Domain.Employees.Interfaces
{
    public interface IEmployeeCreator
    {
        Task<Employee> CreateAsync(EmployeeDTO createEmployeeDTO);
    }
}
