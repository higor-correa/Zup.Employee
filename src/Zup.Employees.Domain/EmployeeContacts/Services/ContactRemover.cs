using Zup.Employees.Domain.EmployeeContacts.Interfaces;

namespace Zup.Employees.Domain.EmployeeContacts.Services
{
    public class ContactRemover : IEmployeeContactRemover
    {
        private readonly IEmployeeContactRepository _contactRepository;

        public ContactRemover()
        {
        }

        public ContactRemover(IEmployeeContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task DeleteAllFromEmployee(Guid employeeId)
        {
            var contacts = await _contactRepository.GetAllFromEmployeeAsync(employeeId);
            foreach (var contact in contacts)
            {
                await _contactRepository.DeleteAsync(contact);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _contactRepository.GetAsync(id);
            if (entity != null)
            {
                await _contactRepository.DeleteAsync(entity);
            }
        }
    }
}
