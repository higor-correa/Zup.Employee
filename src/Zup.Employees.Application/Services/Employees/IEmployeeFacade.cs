using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.Employees.Entities;

namespace Zup.Employees.Application.Services.Employees
{
    public interface IEmployeeFacade
    {
        Task<Employee> CreateAsync(EmployeeDTO createEmployeeDTO);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Employee>> GetAsync();
        Task<Employee?> GetAsync(Guid id);
        Task<Employee> UpdateAsync(EmployeeDTO updateEmployeeDTO);
    }
}