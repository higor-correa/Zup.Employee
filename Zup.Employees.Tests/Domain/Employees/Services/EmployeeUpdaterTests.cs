using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.Employees.Entities;
using Zup.Employees.Domain.Employees.Interfaces;
using Zup.Employees.Domain.Employees.Services;

namespace Zup.Employees.Tests.Domain.Employees.Services;

public class EmployeeUpdaterTests
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeSearcher _employeeSearcher;
    private readonly Fixture _fixture;
    private readonly EmployeeUpdater _employeeUpdater;

    public EmployeeUpdaterTests()
    {
        _employeeRepository = Substitute.For<IEmployeeRepository>();
        _employeeSearcher = Substitute.For<IEmployeeSearcher>();
        _fixture = FixtureHelper.CreateFixture();

        _employeeUpdater = new EmployeeUpdater(_employeeRepository, _employeeSearcher);
    }

    [Fact]
    public async Task Should_Update_Fields_And_Leader()
    {
        var leader = _fixture.Create<Employee>();
        var employee = _fixture.Create<Employee>();
        var employeeDto = _fixture.Create<EmployeeDTO>();
        employeeDto.LeaderId = leader.Id;

        _employeeRepository.GetAsync(employeeDto.Id).Returns(employee);
        _employeeSearcher.GetAsync(leader.Id).Returns(leader);

        var act = await _employeeUpdater.UpdateAsync(employeeDto);

        act.Should().BeEquivalentTo(employeeDto, opt => opt.Excluding(x => x.Id).ExcludingMissingMembers());
        act.Leader.Should().Be(leader);
    }

    [Fact]
    public async Task Should_Update_Fields_But_Null_On_Leader_That_Is_Not_Found()
    {
        var employee = _fixture.Create<Employee>();
        var employeeDto = _fixture.Create<EmployeeDTO>();

        _employeeRepository.GetAsync(employeeDto.Id).Returns(employee);

        var act = await _employeeUpdater.UpdateAsync(employeeDto);

        act.Should().BeEquivalentTo(employeeDto, opt => opt.Excluding(x => x.Id).ExcludingMissingMembers());
        act.Leader.Should().BeNull();
    }

    [Fact]
    public async Task Should_Return_Null_When_Specified_Employee_Is_Not_Found()
    {
        var employeeDto = _fixture.Create<EmployeeDTO>();

        var act = await _employeeUpdater.UpdateAsync(employeeDto);

        act.Should().BeNull();
    }
}
