using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using Zup.Employees.Application.Validations;
using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.Employees.Entities;
using Zup.Employees.Domain.Employees.Interfaces;

namespace Zup.Employees.Tests.Application.Validations;

public class ContactValidationTests
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEmployeeSearcher _employeeSearcher;
    private readonly Fixture _fixture;

    private ContactValidation _contactValidation;

    public ContactValidationTests()
    {
        CultureInfo.CurrentCulture = new CultureInfo("en-US");
        _fixture = FixtureHelper.CreateFixture();
        _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
        _employeeSearcher = Substitute.For<IEmployeeSearcher>();

        CreateValidator();
    }

    private void CreateValidator()
    {
        _contactValidation = new ContactValidation(_httpContextAccessor, _employeeSearcher);
    }

    [Fact]
    public async Task Should_Get_Default_Errors()
    {
        var employeeDto = new ContactDTO();

        var act = await _contactValidation.ValidateAsync(employeeDto);

        act.Errors.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new string[]{
            "'Number' must not be empty.",
            "'Employee Id' must not be empty."
        });
    }

    [Fact]
    public async Task Should_Validate_When_Employee_Is_Not_Found()
    {
        var employeeDto = _fixture.Create<ContactDTO>();

        var act = await _contactValidation.ValidateAsync(employeeDto);

        act.Errors.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new string[]{
            "Employee not found",
        });
    }

    [Fact]
    public async Task Should_Be_Totally_Valid()
    {
        var employeeDto = CreateAValidScenario();

        var act = await _contactValidation.ValidateAsync(employeeDto);

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

        var act = await _contactValidation.ValidateAsync(employeeDto);

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

        var act = await _contactValidation.ValidateAsync(employeeDto);

        act.Errors.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new string[]{
            "'Id' must not be empty."
        });
    }

    private ContactDTO CreateAValidScenario()
    {
        var employee = _fixture.Create<Employee>();

        var employeeDto = _fixture.Create<ContactDTO>();
        employeeDto.EmployeeId = employee.Id;

        _employeeSearcher.GetAsync(employee.Id).Returns(employee);

        return employeeDto;
    }
}
