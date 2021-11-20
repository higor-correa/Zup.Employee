namespace Zup.Employees.Security.Domain.Interfaces;

public interface ILoginService
{
    Task<string> AuthenticateAsync(string email, string password, IEnumerable<string> roles);
}