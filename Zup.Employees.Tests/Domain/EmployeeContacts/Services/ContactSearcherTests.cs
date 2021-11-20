using System;
using Zup.Employees.Domain.EmployeeContacts.Entities;
using Zup.Employees.Domain.EmployeeContacts.Interfaces;
using Zup.Employees.Domain.EmployeeContacts.Services;

namespace Zup.Employees.Tests.Domain.EmployeeContacts.Services;

public class ContactSearcherTests
{
    private readonly IEmployeeContactRepository _employeeContactRepository;
    private readonly Fixture _fixture;
    private readonly ContactSearcher _contactSearcher;

    public ContactSearcherTests()
    {
        _employeeContactRepository = Substitute.For<IEmployeeContactRepository>();
        _fixture = FixtureHelper.CreateFixture();

        _contactSearcher = new ContactSearcher(_employeeContactRepository);
    }

    [Fact]
    public async Task Should_Call_Repository_Asking_All_Contacts_From_Employee()
    {
        var employeeId = _fixture.Create<Guid>();
        var contacts = _fixture.CreateMany<Contact>();

        _employeeContactRepository.GetAllFromEmployeeAsync(employeeId).Returns(contacts);

        var act = await _contactSearcher.GetAllFromEmployeeAsync(employeeId);

        act.Should().BeEquivalentTo(contacts);

        await _employeeContactRepository.Received(1).GetAllFromEmployeeAsync(employeeId);
    }

    [Fact]
    public async Task Should_Call_Repository_Asking_The_Specific_Contact()
    {
        var contact = _fixture.Create<Contact>();

        _employeeContactRepository.GetAsync(contact.Id).Returns(contact);

        var act = await _contactSearcher.GetAsync(contact.Id);

        act.Should().BeEquivalentTo(contact);

        await _employeeContactRepository.Received(1).GetAsync(contact.Id);
    }
}
