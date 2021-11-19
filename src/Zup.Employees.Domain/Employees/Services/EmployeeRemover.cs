using Zup.Employees.Domain.Employees.Interfaces;

namespace Zup.Employees.Domain.Employees.Services
{
    public class EmployeeRemover : IEmployeeRemover
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeRemover(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task DeleteAsync(Guid id)
        {
            return _employeeRepository.DeleteAsync(id);
        }
    }
}
