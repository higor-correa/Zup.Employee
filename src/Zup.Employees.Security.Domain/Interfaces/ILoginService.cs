namespace Zup.Employees.Security.Domain.Interfaces;

public interface ILoginService
{
    Task<string> AuthenticateAsync(LoginDTO loginDTO);
}