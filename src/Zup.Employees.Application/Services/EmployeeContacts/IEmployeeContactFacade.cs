using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.EmployeeContacts.Entities;

namespace Zup.Employees.Application.Services.EmployeeContacts;

public interface IEmployeeContactFacade
{
    Task<Contact> CreateAsync(ContactDTO createEmployeeContactDTO);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Contact>> GetAllFromEmployee(Guid employeeId);
    Task<Contact?> GetAsync(Guid id);
    Task<Contact?> UpdateAsync(ContactDTO updateEmployeeContactDTO);
}