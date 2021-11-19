using Zup.Employees.Domain.EmployeeContacts.Entities;
using Zup.Employees.Domain.EmployeeContacts.Interfaces;

namespace Zup.Employees.Domain.EmployeeContacts.Services
{
    public class ContactSearcher : IEmployeeContactSearcher
    {
        private readonly IEmployeeContactRepository _contactRepository;

        public ContactSearcher(IEmployeeContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public Task<IEnumerable<Contact>> GetAllFromEmployee(Guid employeeId)
        {
            return _contactRepository.GetAllFromEmployee(employeeId);
        }

        public Task<Contact?> GetAsync(Guid id)
        {
            return id == Guid.Empty
                ? Task.FromResult(default(Contact))
                : _contactRepository.GetAsync(id);
        }
    }
}
