using System;
using Zup.Employees.Application.Services.EmployeeContacts;
using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.EmployeeContacts.Entities;
using Zup.Employees.Domain.EmployeeContacts.Interfaces;

namespace Zup.Employees.Tests.Application.Services.Employees;

public class EmployeeContactFacadeTests
{
    private readonly IEmployeeContactCreator _contactCreatorMock;
    private readonly IEmployeeContactUpdater _contactUpdaterMock;
    private readonly IEmployeeContactRemover _contactRemoverMock;
    private readonly IEmployeeContactSearcher _contactSearcherMock;
    private readonly Fixture _fixture;
    private readonly EmployeeContactFacade _contactFacade;

    public EmployeeContactFacadeTests()
    {
        _contactCreatorMock = Substitute.For<IEmployeeContactCreator>();
        _contactUpdaterMock = Substitute.For<IEmployeeContactUpdater>();
        _contactRemoverMock = Substitute.For<IEmployeeContactRemover>();
        _contactSearcherMock = Substitute.For<IEmployeeContactSearcher>();

        _fixture = FixtureHelper.CreateFixture();

        _contactFacade = new EmployeeContactFacade(_contactCreatorMock, _contactUpdaterMock, _contactRemoverMock, _contactSearcherMock);
    }

    [Fact]
    public async Task Should_Call_Creator_Service_When_Creating_Contact()
    {
        var dto = _fixture.Create<ContactDTO>();
        var expected = _fixture.Create<Contact>();

        _contactCreatorMock.CreateAsync(dto).Returns(expected);

        var act = await _contactFacade.CreateAsync(dto);

        act.Should().BeEquivalentTo(expected, opt => opt.ExcludingMissingMembers());
        await _contactCreatorMock.Received(1).CreateAsync(dto);
    }

    [Fact]
    public async Task Should_Call_Updater_Service_When_Updating_Contact()
    {
        var dto = _fixture.Create<ContactDTO>();
        var expected = _fixture.Create<Contact>();

        _contactUpdaterMock.UpdateAsync(dto).Returns(expected);

        var act = await _contactFacade.UpdateAsync(dto);

        act.Should().BeEquivalentTo(expected, opt => opt.ExcludingMissingMembers());
        await _contactUpdaterMock.Received(1).UpdateAsync(dto);
    }

    [Fact]
    public async Task Should_Remove_All_Contacts_From_Contact_And_Call_Remover_When_Removing_Contact()
    {
        var id = _fixture.Create<Guid>();

        await _contactFacade.DeleteAsync(id);

        await _contactRemoverMock.Received(1).DeleteAsync(id);
    }

    [Fact]
    public async Task Should_Call_Searcher_When_Getting_All_Contacts_From_Specific_Employee()
    {
        var contacts = _fixture.CreateMany<Contact>();
        var employeeId = _fixture.Create<Guid>();

        _contactSearcherMock.GetAllFromEmployeeAsync(employeeId).Returns(contacts);

        var act = await _contactFacade.GetAllFromEmployee(employeeId);

        act.Should().BeEquivalentTo(contacts, opt => opt.ExcludingMissingMembers());
        await _contactSearcherMock.Received(1).GetAllFromEmployeeAsync(employeeId);
    }

    [Fact]
    public async Task Should_Call_Searcher_When_Getting_A_Specific_Contact()
    {
        var expected = _fixture.Create<Contact>();

        _contactSearcherMock.GetAsync(expected.Id).Returns(expected);

        var act = await _contactFacade.GetAsync(expected.Id);

        act.Should().BeEquivalentTo(expected, opt => opt.ExcludingMissingMembers());
        await _contactSearcherMock.Received(1).GetAsync(expected.Id);
    }
}
