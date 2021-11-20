using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.EmployeeContacts.Interfaces;
using Zup.Employees.Domain.Mappings;

namespace Zup.Employees.Application.Services.EmployeeContacts;

public class EmployeeContactFacade : IEmployeeContactFacade
{
    private readonly IEmployeeContactCreator _contactCreator;
    private readonly IEmployeeContactUpdater _contactUpdater;
    private readonly IEmployeeContactRemover _contactRemover;
    private readonly IEmployeeContactSearcher _contactSearcher;

    public EmployeeContactFacade
    (
        IEmployeeContactCreator contactCreator,
        IEmployeeContactUpdater contactUpdater,
        IEmployeeContactRemover contactRemover,
        IEmployeeContactSearcher contactSearcher
    )
    {
        _contactCreator = contactCreator;
        _contactUpdater = contactUpdater;
        _contactRemover = contactRemover;
        _contactSearcher = contactSearcher;
    }

    public async Task<IEnumerable<ContactDTO>> GetAllFromEmployee(Guid employeeId)
    {
        var contacts = await _contactSearcher.GetAllFromEmployee(employeeId);
        return contacts.Select(x => x.ToDto());
    }

    public async Task<ContactDTO?> GetAsync(Guid id)
    {
        var contact = await _contactSearcher.GetAsync(id);
        return contact?.ToDto();
    }

    public async Task<ContactDTO> CreateAsync(ContactDTO createEmployeeContactDTO)
    {
        var contact = await _contactCreator.CreateAsync(createEmployeeContactDTO);
        return contact.ToDto();
    }

    public async Task<ContactDTO?> UpdateAsync(ContactDTO updateEmployeeContactDTO)
    {
        var contact = await _contactUpdater.UpdateAsync(updateEmployeeContactDTO);
        return contact?.ToDto();
    }

    public Task DeleteAsync(Guid id)
    {
        return _contactRemover.DeleteAsync(id);
    }
}
