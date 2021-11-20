using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.Employees.Entities;
using Zup.Employees.Domain.Employees.Interfaces;
using Zup.Employees.Domain.Employees.Services;

namespace Zup.Employees.Tests.Domain.Employees.Services;

public class EmployeeCreatorTests
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEmployeeSearcher _employeeSearcher;
    private readonly Fixture _fixture;

    private readonly EmployeeCreator _employeeCreator;

    public EmployeeCreatorTests()
    {
        _employeeRepository = Substitute.For<IEmployeeRepository>();
        _employeeSearcher = Substitute.For<IEmployeeSearcher>();
        _fixture = FixtureHelper.CreateFixture();
        _employeeCreator = new EmployeeCreator(_employeeRepository, _employeeSearcher);
    }

    [Fact]
    public async Task Should_Call_Create_From_Repository_With_Created_Entity_Mapped()
    {
        Employee? mappedEntity = null;
        var employee = _fixture.Create<Employee>();
        var leader = _fixture.Create<Employee>();
        var employeeDto = _fixture.Create<EmployeeDTO>();
        employeeDto.LeaderId = leader.Id;

        _employeeSearcher.GetAsync(employeeDto.LeaderId).Returns(leader);
        await _employeeRepository.CreateAsync(Arg.Do<Employee>(x => mappedEntity = x));
        _employeeRepository.CreateAsync(Arg.Any<Employee>()).Returns(employee);

        var act = await _employeeCreator.CreateAsync(employeeDto);

        mappedEntity.Should().BeEquivalentTo(employeeDto, opt => opt.Excluding(x => x.Id).ExcludingMissingMembers());
        mappedEntity.Leader.Should().Be(leader);

        act.Should().Be(employee);
    }

    [Fact]
    public async Task Should_Call_Create_From_Repository_With_Created_Entity_Mapped_But_Without_Leader()
    {
        Employee? mappedEntity = null;
        var employee = _fixture.Create<Employee>();
        var employeeDto = _fixture.Create<EmployeeDTO>();

        await _employeeRepository.CreateAsync(Arg.Do<Employee>(x => mappedEntity = x));
        _employeeRepository.CreateAsync(Arg.Any<Employee>()).Returns(employee);

        var act = await _employeeCreator.CreateAsync(employeeDto);

        mappedEntity.Should().BeEquivalentTo(employeeDto, opt => opt.Excluding(x => x.Id).ExcludingMissingMembers());
        mappedEntity.Leader.Should().BeNull();

        act.Should().Be(employee);
    }
}
