using System;
using Zup.Employees.Application.Validations;
using Zup.Employees.Domain.Employees.Entities;
using Zup.Employees.Domain.Employees.Interfaces;
using Zup.Employees.Security.Domain.DTOs;

namespace Zup.Employees.Tests.Application.Validations;

public class CreatePasswordValidationTests
{
    private readonly IEmployeeSearcher _employeeSearcher;
    private readonly Fixture _fixture;
    private readonly CreatePasswordValidation _validator;

    public CreatePasswordValidationTests()
    {
        _employeeSearcher = Substitute.For<IEmployeeSearcher>();
        _fixture = FixtureHelper.CreateFixture();
        _validator = new CreatePasswordValidation(_employeeSearcher);
    }

    [Fact]
    public async Task Should_Be_Valid()
    {
        var employee = _fixture.Create<Employee>();
        employee.PasswordHash = string.Empty;
        var changePassDto = new CreatePasswordDTO { Email = employee.Email, Password = Guid.NewGuid().ToString() };

        _employeeSearcher.GetForLoginAsync(employee.Email, string.Empty).Returns(employee);

        var act = await _validator.ValidateAsync(changePassDto);

        act.Errors.Select(x => x.ErrorMessage).Should().BeEmpty();
    }

    [Fact]
    public async Task Should_Validate_Password_Already_Set()
    {
        var changePassDto = _fixture.Create<CreatePasswordDTO>();

        var act = await _validator.ValidateAsync(changePassDto);

        act.Errors.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new string[] { "Password already created" });
    }

    [Fact]
    public async Task Should_Validate_Password_Empty()
    {
        var changePassDto = _fixture.Create<CreatePasswordDTO>();
        changePassDto.Password = string.Empty;

        var act = await _validator.ValidateAsync(changePassDto);

        act.Errors.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new string[]
        {
            "'Password' must not be empty.",
            "Password already created"
        });
    }
}
