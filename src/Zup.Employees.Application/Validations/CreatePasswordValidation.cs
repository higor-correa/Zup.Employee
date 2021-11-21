using FluentValidation;
using Zup.Employees.Domain.Employees.Interfaces;
using Zup.Employees.Security.Domain.DTOs;

namespace Zup.Employees.Application.Validations;

public class CreatePasswordValidation : AbstractValidator<CreatePasswordDTO>
{
    public CreatePasswordValidation(IEmployeeSearcher employeeSearcher)
    {
        RuleFor(x => x.Password)
            .NotEmpty();

        RuleFor(x => x)
            .MustAsync(async (dto, _) => (await employeeSearcher.GetForLoginAsync(dto.Email, string.Empty)) != null)
            .WithMessage("Password already created");
    }
}
