using Zup.Employees.Security.Domain.DTOs;

namespace Zup.Employees.Security.Domain.Tokens.Interfaces;

public interface ITokenService
{
    string BuildToken(UserDTO user, IEnumerable<string> roles);
}