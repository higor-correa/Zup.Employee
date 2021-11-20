using System;
using Zup.Employees.Domain.Employees.Entities;
using Zup.Employees.Domain.Employees.Interfaces;
using Zup.Employees.Domain.Employees.Services;

namespace Zup.Employees.Tests.Domain.Employees.Services;

public class EmployeeRemoverTests
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly Fixture _fixture;
    private readonly EmployeeRemover _employeeRemover;

    public EmployeeRemoverTests()
    {
        _employeeRepository = Substitute.For<IEmployeeRepository>();
        _fixture = FixtureHelper.CreateFixture();

        _employeeRemover = new EmployeeRemover(_employeeRepository);
    }

    [Fact]
    public async Task Should_Call_Delete_From_Repository_With_Entity_Retrieved()
    {
        var employee = _fixture.Create<Employee>();

        _employeeRepository.GetAsync(employee.Id).Returns(employee);

        await _employeeRemover.DeleteAsync(employee.Id);

        await _employeeRepository.Received(1).DeleteAsync(employee);
    }

    [Fact]
    public async Task Should_Not_Call_Repository_When_No_Employee_Is_Found()
    {
        var id = _fixture.Create<Guid>();

        await _employeeRemover.DeleteAsync(id);

        await _employeeRepository.Received(0).DeleteAsync(Arg.Any<Employee>());
    }
}
