using System;
using System.Linq;
using Zup.Employees.Domain.EmployeeContacts.Entities;
using Zup.Employees.Domain.EmployeeContacts.Interfaces;
using Zup.Employees.Domain.EmployeeContacts.Services;

namespace Zup.Employees.Tests.Domain.EmployeeContacts.Services;

public class ContactRemoverTests
{
    private readonly IEmployeeContactRepository _employeeContactRepository;
    private readonly Fixture _fixture;
    private readonly ContactRemover _contactRemover;

    public ContactRemoverTests()
    {
        _employeeContactRepository = Substitute.For<IEmployeeContactRepository>();
        _fixture = FixtureHelper.CreateFixture();

        _contactRemover = new ContactRemover(_employeeContactRepository);
    }

    [Fact]
    public async Task Should_Call_Delete_From_Repository_With_Entity_Retrieved()
    {
        var contact = _fixture.Create<Contact>();

        _employeeContactRepository.GetAsync(contact.Id).Returns(contact);

        await _contactRemover.DeleteAsync(contact.Id);

        await _employeeContactRepository.Received(1).DeleteAsync(contact);
    }

    [Fact]
    public async Task Should_Not_Call_Repository_When_No_Contact_Is_Found()
    {
        var id = _fixture.Create<Guid>();

        await _contactRemover.DeleteAsync(id);

        await _employeeContactRepository.Received(0).DeleteAsync(Arg.Any<Contact>());
    }

    [Fact]
    public async Task Should_Call_Repository_Asking_All_Contacts_From_Employee_And_Delete_Them()
    {
        var employeeId = _fixture.Create<Guid>();
        var contacts = _fixture.CreateMany<Contact>();

        _employeeContactRepository.GetAllFromEmployeeAsync(employeeId).Returns(contacts);

        await _contactRemover.DeleteAllFromEmployee(employeeId);

        await _employeeContactRepository.Received(1).GetAllFromEmployeeAsync(employeeId);
        await _employeeContactRepository.Received(contacts.Count()).DeleteAsync(Arg.Is<Contact>(x => contacts.Contains(x)));
    }
}
