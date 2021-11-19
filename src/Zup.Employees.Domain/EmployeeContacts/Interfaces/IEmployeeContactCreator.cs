using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.EmployeeContacts.Entities;

namespace Zup.Employees.Domain.EmployeeContacts.Interfaces
{
    public interface IEmployeeContactCreator
    {
        Task<Contact> CreateAsync(ContactDTO contactDTO);
    }
}
