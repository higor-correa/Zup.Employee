using Zup.Employees.Security.Domain.Interfaces;

namespace Zup.Employees.Application.Services.Security;

public class LoginFacade : ILoginFacade
{
    private readonly ILoginService _loginService;

    public LoginFacade(ILoginService loginService)
    {
        _loginService = loginService;
    }

    public Task<string> AuthenticateAsync(string email, string password, IEnumerable<string> roles)
    {
        return _loginService.AuthenticateAsync(email, password, roles);
    }
}
