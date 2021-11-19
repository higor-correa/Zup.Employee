using Zup.Employees.Domain.Employees.Entities;
using Zup.Employees.Domain.Interfaces;

namespace Zup.Employees.Domain.Employees.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
    }
}
