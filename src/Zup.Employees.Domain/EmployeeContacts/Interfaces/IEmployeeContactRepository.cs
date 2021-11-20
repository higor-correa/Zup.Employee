using Zup.Employees.Domain.EmployeeContacts.Entities;
using Zup.Employees.Domain.Interfaces;

namespace Zup.Employees.Domain.EmployeeContacts.Interfaces
{
    public interface IEmployeeContactRepository : IRepository<Contact>
    {
        Task<IEnumerable<Contact>> GetAllFromEmployeeAsync(Guid employeeId);
    }
}
