using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.EmployeeContacts.Entities;

namespace Zup.Employees.Domain.Mappings;

public static class ContactMappings
{
    public static ContactDTO ToDto(this Contact contact)
    {
        return new ContactDTO
        {
            Id = contact.Id,
            EmployeeId = contact.EmployeeId,
            Number = contact.Number
        };
    }
}
