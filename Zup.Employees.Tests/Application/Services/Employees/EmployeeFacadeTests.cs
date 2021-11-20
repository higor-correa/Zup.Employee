using System;
using Zup.Employees.Application.Services.EmployeeContacts;
using Zup.Employees.Application.Services.Employees;
using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.Employees.Entities;
using Zup.Employees.Domain.Employees.Interfaces;

namespace Zup.Employees.Tests.Application.Services.Employees;

public class EmployeeFacadeTests
{
    private readonly IEmployeeCreator _employeeCreatorMock;
    private readonly IEmployeeUpdater _employeeUpdaterMock;
    private readonly IEmployeeRemover _employeeRemoverMock;
    private readonly IEmployeeSearcher _employeeSearcherMock;
    private readonly IEmployeeContactFacade _employeeContactFacade;
    private readonly Fixture _fixture;
    private readonly EmployeeFacade _employeeFacade;

    public EmployeeFacadeTests()
    {
        _employeeCreatorMock = Substitute.For<IEmployeeCreator>();
        _employeeUpdaterMock = Substitute.For<IEmployeeUpdater>();
        _employeeRemoverMock = Substitute.For<IEmployeeRemover>();
        _employeeSearcherMock = Substitute.For<IEmployeeSearcher>();
        _employeeContactFacade = Substitute.For<IEmployeeContactFacade>();

        _fixture = FixtureHelper.CreateFixture();

        _employeeFacade = new EmployeeFacade(_employeeCreatorMock, _employeeUpdaterMock, _employeeRemoverMock, _employeeSearcherMock, _employeeContactFacade);
    }

    [Fact]
    public async Task Should_Call_Creator_Service_When_Creating_Employee()
    {
        var dto = _fixture.Create<EmployeeDTO>();
        var expected = _fixture.Create<Employee>();

        _employeeCreatorMock.CreateAsync(dto).Returns(expected);

        var act = await _employeeFacade.CreateAsync(dto);

        act.Should().BeEquivalentTo(expected, opt => opt.ExcludingMissingMembers());
        await _employeeCreatorMock.Received(1).CreateAsync(dto);
    }

    [Fact]
    public async Task Should_Call_Updater_Service_When_Updating_Employee()
    {
        var dto = _fixture.Create<EmployeeDTO>();
        var expected = _fixture.Create<Employee>();

        _employeeUpdaterMock.UpdateAsync(dto).Returns(expected);

        var act = await _employeeFacade.UpdateAsync(dto);

        act.Should().BeEquivalentTo(expected, opt => opt.ExcludingMissingMembers());
        await _employeeUpdaterMock.Received(1).UpdateAsync(dto);
    }

    [Fact]
    public async Task Should_Remove_All_Contacts_From_Employee_And_Call_Remover_When_Removing_Employee()
    {
        var id = _fixture.Create<Guid>();

        await _employeeFacade.DeleteAsync(id);

        await _employeeRemoverMock.Received(1).DeleteAsync(id);
        await _employeeContactFacade.Received(1).DeleteAllFromEmployee(id);
    }

    [Fact]
    public async Task Should_Call_Searcher_When_Getting_All_Employees()
    {
        var employees = _fixture.CreateMany<Employee>();

        _employeeSearcherMock.GetAsync().Returns(employees);

        var act = await _employeeFacade.GetAsync();

        act.Should().BeEquivalentTo(employees, opt => opt.ExcludingMissingMembers());
        await _employeeSearcherMock.Received(1).GetAsync();
    }

    [Fact]
    public async Task Should_Call_Searcher_When_Getting_A_Specific_Employee()
    {
        var expected = _fixture.Create<Employee>();

        _employeeSearcherMock.GetAsync(expected.Id).Returns(expected);

        var act = await _employeeFacade.GetAsync(expected.Id);

        act.Should().BeEquivalentTo(expected, opt => opt.ExcludingMissingMembers());
        await _employeeSearcherMock.Received(1).GetAsync(expected.Id);
    }
}
