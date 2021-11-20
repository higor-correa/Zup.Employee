using Zup.Employees.Domain.DTOs;

namespace Zup.Employees.Application.Services.EmployeeContacts;

public interface IEmployeeContactFacade
{
    Task<ContactDTO> CreateAsync(ContactDTO createEmployeeContactDTO);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<ContactDTO>> GetAllFromEmployee(Guid employeeId);
    Task<ContactDTO?> GetAsync(Guid id);
    Task<ContactDTO?> UpdateAsync(ContactDTO updateEmployeeContactDTO);
    Task DeleteAllFromEmployee(Guid employeeId);
}