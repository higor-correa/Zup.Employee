using Zup.Employees.Domain.Employees.Entities;
using Zup.Employees.Domain.Employees.Interfaces;

namespace Zup.Employees.Infra.Repositories;

public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
{
    public EmployeeRepository(EmployeeContext employeeContext) : base(employeeContext)
    {
    }
}
