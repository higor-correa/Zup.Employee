using Zup.Employees.Domain.Employees.Entities;
using Zup.Employees.Domain.Employees.Interfaces;
using Zup.Employees.Domain.Employees.Services;

namespace Zup.Employees.Tests.Domain.Employees.Services;

public class EmployeeSearcherTests
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly Fixture _fixture;
    private readonly EmployeeSearcher _employeeSearcher;

    public EmployeeSearcherTests()
    {
        _employeeRepository = Substitute.For<IEmployeeRepository>();
        _fixture = FixtureHelper.CreateFixture();

        _employeeSearcher = new EmployeeSearcher(_employeeRepository);
    }

    [Fact]
    public async Task Should_Call_Repository_Asking_All_Employees()
    {
        var employees = _fixture.CreateMany<Employee>();

        _employeeRepository.GetAsync().Returns(employees);

        var act = await _employeeSearcher.GetAsync();

        act.Should().BeEquivalentTo(employees);

        await _employeeRepository.Received(1).GetAsync();
    }

    [Fact]
    public async Task Should_Call_Repository_Asking_The_Specific_Employee()
    {
        var employee = _fixture.Create<Employee>();

        _employeeRepository.GetAsync(employee.Id).Returns(employee);

        var act = await _employeeSearcher.GetAsync(employee.Id);

        act.Should().BeEquivalentTo(employee);

        await _employeeRepository.Received(1).GetAsync(employee.Id);
    }
}
