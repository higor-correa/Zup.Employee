using Microsoft.EntityFrameworkCore;
using Zup.Employees.Domain.Employees.Entities;
using Zup.Employees.Domain.Employees.Interfaces;

namespace Zup.Employees.Infra.Repositories;

public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
{
    public EmployeeRepository(EmployeeContext employeeContext) : base(employeeContext)
    {
    }

    public Task<Employee?> GetForLoginAsync(string email, string passwordHashed)
    {
        return Set.Where(x => x.Email == email && x.PasswordHash == passwordHashed).FirstOrDefaultAsync();
    }
}
