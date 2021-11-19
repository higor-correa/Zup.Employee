using Zup.Employees.Domain.EmployeeContacts.Entities;

namespace Zup.Employees.Domain.EmployeeContacts.Interfaces
{
    public interface IEmployeeContactSearcher
    {
        Task<Contact?> GetAsync(Guid id);
        Task<IEnumerable<Contact>> GetAllFromEmployee(Guid employeeId);
    }
}
