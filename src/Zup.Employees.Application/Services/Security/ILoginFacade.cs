using Zup.Employees.Security.Domain.Interfaces;

namespace Zup.Employees.Application.Services.Security;

public interface ILoginFacade
{
    Task<string> AuthenticateAsync(LoginDTO loginDTO);
}