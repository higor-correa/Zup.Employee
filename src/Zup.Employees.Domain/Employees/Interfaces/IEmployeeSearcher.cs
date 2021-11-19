using Zup.Employees.Domain.Employees.Entities;

namespace Zup.Employees.Domain.Employees.Interfaces
{
    public interface IEmployeeSearcher
    {
        Task<IEnumerable<Employee>> GetAsync();
        Task<Employee?> GetAsync(Guid id);
    }
}
