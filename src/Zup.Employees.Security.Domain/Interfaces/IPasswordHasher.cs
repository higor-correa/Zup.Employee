namespace Zup.Employees.Security.Domain.Interfaces;

public interface IPasswordHasher
{
    string HashPassword(string password);
}