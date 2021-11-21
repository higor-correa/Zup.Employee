using System;
using Zup.Employees.Application.Validations;
using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.Employees.Entities;
using Zup.Employees.Domain.Employees.Interfaces;
using Zup.Employees.Security.Domain.DTOs;
using Zup.Employees.Security.Domain.Interfaces;

namespace Zup.Employees.Tests.Application.Validations;

public class ChangePasswordValidationTests
{
    private readonly IEmployeeSearcher _employeeSearcher;
    private readonly IPasswordHasher _passwordHasher;
    private readonly Fixture _fixture;
    private readonly ChangePasswordValidation _validator;

    public ChangePasswordValidationTests()
    {
        _employeeSearcher = Substitute.For<IEmployeeSearcher>();
        _passwordHasher = Substitute.For<IPasswordHasher>();
        _fixture = FixtureHelper.CreateFixture();
        _validator = new ChangePasswordValidation(_employeeSearcher, _passwordHasher);
    }

    [Fact]
    public async Task Should_Be_Valid()
    {
        var employee = _fixture.Create<Employee>();
        var changePassDto = new ChangePasswordDTO { Email = employee.Email, NewPassword = Guid.NewGuid().ToString(), OldPassword = employee.PasswordHash };

        _passwordHasher.HashPassword(changePassDto.OldPassword).Returns("hash");

        _employeeSearcher.GetForLoginAsync(employee.Email, "hash").Returns(employee);

        var act = await _validator.ValidateAsync(changePassDto);

        act.Errors.Select(x => x.ErrorMessage).Should().BeEmpty();
    }

    [Fact]
    public async Task Should_Validate_Email_Or_Password_Not_Found()
    {
        var changePassDto = _fixture.Create<ChangePasswordDTO>();

        var act = await _validator.ValidateAsync(changePassDto);

        act.Errors.Select(x => x.ErrorMessage).Should().BeEquivalentTo(new string[] { "Email or password not found" });
    }
}
