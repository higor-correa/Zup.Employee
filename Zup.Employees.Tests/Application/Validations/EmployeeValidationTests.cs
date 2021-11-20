using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using Zup.Employees.Application.Validations;
using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.Employees.Entities;
using Zup.Employees.Domain.Employees.Interfaces;

namespace Zup.Employees.Tests.Application.Validations;

public class EmployeeValidationTests
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEmployeeSearcher _employeeSearcher;
    private readonly Fixture _fixture;

    private EmployeeValidation _employeeValidation;

    public EmployeeValidationTests()
    {
        CultureInfo.CurrentCulture = new CultureInfo("en-US");
        _fixture = FixtureHelper.CreateFixture();
        _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
        _employeeSearcher = Substitute.For<IEmployeeSearcher>();

        CreateValidator();
    }

    private void CreateValidator()
    {
        _employeeValidation = new EmployeeValidation(_httpContextAccessor, _employeeSearcher);
    }

    [Fact]
    public async Task Should_Get_Default_Errors()
    {
        var employeeDto = new EmployeeDTO();

        var act = await _employeeValidation.ValidateAsync(employeeDto);

        act.Errors.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new string[]{
            "'Plate Number' must be greater than '0'.",
            "'Plate Number' must not be empty.",
            "'Surname' must not be empty.",
            "'Surname' must not be empty.",
            "'Name' must not be empty.",
            "'Name' must not be empty.",
            "'Email' must not be empty.",
            "'Email' must not be empty.",
        });
    }

    [Fact]
    public async Task Should_Validade_When_Email_Is_Invalid()
    {
        var leader = _fixture.Create<Employee>();
        leader.IsLeader = true;

        var employeeDto = _fixture.Create<EmployeeDTO>();
        employeeDto.LeaderId = leader.Id;

        _employeeSearcher.GetAsync(leader.Id).Returns(leader);

        var act = await _employeeValidation.ValidateAsync(employeeDto);

        act.Errors.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new string[]{
            "'Email' is not a valid email address."
        });
    }

    [Fact]
    public async Task Should_Validade_When_Leader_Not_Found()
    {
        var employeeDto = _fixture.Create<EmployeeDTO>();
        employeeDto.Email = "teste@teste.com";

        var act = await _employeeValidation.ValidateAsync(employeeDto);

        act.Errors.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new string[]{
            "Leader not found"
        });
    }

    [Fact]
    public async Task Should_Validade_When_Leader_Does_Not_Have_Leader_Flag_Enabled()
    {
        var leader = _fixture.Create<Employee>();
        leader.IsLeader = false;

        var employeeDto = _fixture.Create<EmployeeDTO>();
        employeeDto.Email = "teste@teste.com";
        employeeDto.IsLeader = false;

        _employeeSearcher.GetAsync(leader.Id).Returns(leader);

        var act = await _employeeValidation.ValidateAsync(employeeDto);

        act.Errors.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new string[]{
            "Leader not found"
        });
    }

    [Fact]
    public async Task Should_Be_Totally_Valid()
    {
        var employeeDto = CreateAValidScenario();

        var act = await _employeeValidation.ValidateAsync(employeeDto);

        act.Errors.Select(x => x.ErrorMessage).Should().BeEmpty();
    }

    [Fact]
    public async Task Should_Validade_Id_Empty_When_Using_Post_Method()
    {
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Method = HttpMethods.Post;
        _httpContextAccessor.HttpContext.Returns(httpContext);

        var employeeDto = CreateAValidScenario();

        CreateValidator();

        var act = await _employeeValidation.ValidateAsync(employeeDto);

        act.Errors.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new string[]{
            "'Id' must be empty."
        });
    }

    [Fact]
    public async Task Should_Validade_Id_Not_Empty_When_Using_Put_Method()
    {
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Method = HttpMethods.Put;
        _httpContextAccessor.HttpContext.Returns(httpContext);

        var employeeDto = CreateAValidScenario();
        employeeDto.Id = Guid.Empty;
        CreateValidator();

        var act = await _employeeValidation.ValidateAsync(employeeDto);

        act.Errors.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new string[]{
            "'Id' must not be empty."
        });
    }

    private EmployeeDTO CreateAValidScenario()
    {
        var leader = _fixture.Create<Employee>();
        leader.IsLeader = true;

        var employeeDto = _fixture.Create<EmployeeDTO>();
        employeeDto.Email = "teste@teste.com";
        employeeDto.IsLeader = true;
        employeeDto.LeaderId = leader.Id;

        _employeeSearcher.GetAsync(leader.Id).Returns(leader);

        return employeeDto;
    }
}
