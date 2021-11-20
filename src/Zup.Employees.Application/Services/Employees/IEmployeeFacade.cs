using Zup.Employees.Domain.DTOs;

namespace Zup.Employees.Application.Services.Employees
{
    public interface IEmployeeFacade
    {
        Task<EmployeeDTO> CreateAsync(EmployeeDTO createEmployeeDTO);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<EmployeeDTO>> GetAsync();
        Task<EmployeeDTO?> GetAsync(Guid id);
        Task<EmployeeDTO?> UpdateAsync(EmployeeDTO updateEmployeeDTO);
    }
}