using Zup.Employees.Domain.EmployeeContacts.Interfaces;

namespace Zup.Employees.Domain.EmployeeContacts.Services
{
    public class ContactRemover : IEmployeeContactRemover
    {
        private readonly IEmployeeContactRepository _contactRepository;

        public ContactRemover(IEmployeeContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public Task DeleteAsync(Guid id)
        {
            return _contactRepository.DeleteAsync(id);
        }
    }
}
