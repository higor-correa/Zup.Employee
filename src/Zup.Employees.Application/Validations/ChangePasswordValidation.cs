using FluentValidation;
using Zup.Employees.Domain.Employees.Interfaces;
using Zup.Employees.Security.Domain.DTOs;
using Zup.Employees.Security.Domain.Interfaces;

namespace Zup.Employees.Application.Validations;

public class ChangePasswordValidation : AbstractValidator<ChangePasswordDTO>
{
    public ChangePasswordValidation(IEmployeeSearcher employeeSearcher, IPasswordHasher passwordHasher)
    {
        RuleFor(x => x)
            .MustAsync(async (dto, _) => (await employeeSearcher.GetForLoginAsync(dto.Email, passwordHasher.HashPassword(dto.OldPassword))) != null)
            .WithMessage("Email or password not found");
    }
}
