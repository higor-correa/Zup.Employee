
namespace Zup.Employees.Application.Services.Security;

public interface ILoginFacade
{
    Task<string> AuthenticateAsync(string email, string password, IEnumerable<string> roles);
}