using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.EmployeeContacts.Entities;
using Zup.Employees.Domain.EmployeeContacts.Interfaces;

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

    public Task<IEnumerable<Contact>> GetAllFromEmployee(Guid employeeId)
    {
        return _contactSearcher.GetAllFromEmployee(employeeId);
    }

    public Task<Contact?> GetAsync(Guid id)
    {
        return _contactSearcher.GetAsync(id);
    }

    public Task<Contact> CreateAsync(ContactDTO createEmployeeContactDTO)
    {
        return _contactCreator.CreateAsync(createEmployeeContactDTO);
    }

    public Task<Contact?> UpdateAsync(ContactDTO updateEmployeeContactDTO)
    {
        return _contactUpdater.UpdateAsync(updateEmployeeContactDTO);
    }

    public Task DeleteAsync(Guid id)
    {
        return _contactRemover.DeleteAsync(id);
    }
}
