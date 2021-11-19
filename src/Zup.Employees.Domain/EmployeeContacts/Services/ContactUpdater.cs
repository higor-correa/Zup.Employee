using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.EmployeeContacts.Entities;
using Zup.Employees.Domain.EmployeeContacts.Interfaces;

namespace Zup.Employees.Domain.EmployeeContacts.Services
{
    public class ContactUpdater : IEmployeeContactUpdater
    {
        private readonly IEmployeeContactRepository _contactRepository;

        public ContactUpdater(IEmployeeContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<Contact?> UpdateAsync(ContactDTO updateContactDTO)
        {
            var contact = await _contactRepository.GetAsync(updateContactDTO.Id);

            if (contact is null)
            {
                return default;
            }

            contact.Number = updateContactDTO.Number;

            await _contactRepository.UpdateAsync(contact);

            return contact;
        }
    }
}
