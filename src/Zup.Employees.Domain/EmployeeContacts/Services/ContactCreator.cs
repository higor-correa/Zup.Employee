using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.EmployeeContacts.Entities;
using Zup.Employees.Domain.EmployeeContacts.Interfaces;

namespace Zup.Employees.Domain.EmployeeContacts.Services
{
    public class ContactCreator : IEmployeeContactCreator
    {
        private readonly IEmployeeContactRepository _contactRepository;

        public ContactCreator(IEmployeeContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<Contact> CreateAsync(ContactDTO createContactDTO)
        {
            var contact = new Contact(createContactDTO.Number, createContactDTO.EmployeeId);

            return await _contactRepository.CreateAsync(contact);
        }
    }
}
