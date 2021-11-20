using Zup.Employees.Domain.Employees.Entities;
using Zup.Employees.Domain.Employees.Interfaces;

namespace Zup.Employees.Domain.Employees.Services
{
    public class EmployeeSearcher : IEmployeeSearcher
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeSearcher(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<IEnumerable<Employee>> GetAsync()
        {
            return _employeeRepository.GetAsync();
        }

        public Task<Employee?> GetAsync(Guid id)
        {
            return id == Guid.Empty
                ? Task.FromResult(default(Employee))
                : _employeeRepository.GetAsync(id);
        }

        public Task<Employee?> GetForLoginAsync(string email, string passwordHashed)
        {
            return _employeeRepository.GetForLoginAsync(email, passwordHashed);
        }
    }
}
