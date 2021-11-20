using FluentValidation;
using Microsoft.AspNetCore.Http;
using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.Employees.Entities;
using Zup.Employees.Domain.Employees.Interfaces;

namespace Zup.Employees.Application.Validations;

public class EmployeeValidation : AbstractValidator<EmployeeDTO>
{
    public EmployeeValidation
    (
        IHttpContextAccessor httpContextAccessor,
        IEmployeeSearcher employeeSearcher
    )
    {
        Employee? leader = null;
        if (httpContextAccessor!.HttpContext!.Request.Method == HttpMethod.Post.Method)
        {
            RuleFor(x => x.Id).Empty();
        }

        if (httpContextAccessor!.HttpContext!.Request.Method == HttpMethod.Put.Method)
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }

        RuleFor(x => x.LeaderId)
            .MustAsync(async (leaderId, _) =>
            {
                leader = await employeeSearcher.GetAsync(leaderId);
                return leader != null;
            })
            .When(x => x.LeaderId != Guid.Empty)
            .WithMessage("Leader not found")
            .DependentRules(() => RuleFor(x => x.LeaderId)
                                    .Must(x => leader!.IsLeader)
                                    .When(x => x.LeaderId != Guid.Empty)
                                    .WithMessage("The selected leader is not a leader")
            );


        RuleFor(x => x.PlateNumber)
                .GreaterThan(0)
                .NotNull()
                .NotEmpty();

        RuleFor(x => x.Surname)
                    .NotNull()
                    .NotEmpty();

        RuleFor(x => x.Name)
                    .NotNull()
                    .NotEmpty();

        RuleFor(x => x.Email)
                    .NotNull()
                    .NotEmpty()
                    .EmailAddress();
    }
}
