using FluentValidation;
using Microsoft.AspNetCore.Http;
using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.Employees.Interfaces;

namespace Zup.Employees.Application.Validations;

public class ContactValidation : AbstractValidator<ContactDTO>
{
    public ContactValidation
    (
        IHttpContextAccessor httpContextAccessor,
        IEmployeeSearcher employeeSearcher
    )
    {
        if (httpContextAccessor!.HttpContext!.Request.Method == HttpMethod.Post.Method)
        {
            RuleFor(x => x.Id).Empty();
        }

        if (httpContextAccessor!.HttpContext!.Request.Method == HttpMethod.Put.Method)
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }

        RuleFor(x => x.Number)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.EmployeeId)
            .NotEmpty()
            .MustAsync(async (employeeId, _) => await employeeSearcher.GetAsync(employeeId) != null)
            .WithMessage("Employee not found");
    }
}
