using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.Employees.Entities;
using Zup.Employees.Domain.Employees.Interfaces;

namespace Zup.Employees.Application.Services.Employees
{
    public class EmployeeFacade : IEmployeeFacade
    {
        private readonly IEmployeeCreator _employeeCreator;
        private readonly IEmployeeUpdater _employeeUpdater;
        private readonly IEmployeeRemover _employeeRemover;
        private readonly IEmployeeSearcher _employeeSearcher;

        public EmployeeFacade
        (
            IEmployeeCreator employeeCreator,
            IEmployeeUpdater employeeUpdater,
            IEmployeeRemover employeeRemover,
            IEmployeeSearcher employeeSearcher
        )
        {
            _employeeCreator = employeeCreator;
            _employeeUpdater = employeeUpdater;
            _employeeRemover = employeeRemover;
            _employeeSearcher = employeeSearcher;
        }

        public Task<IEnumerable<Employee>> GetAsync()
        {
            return _employeeSearcher.GetAsync();
        }

        public Task<Employee?> GetAsync(Guid id)
        {
            return _employeeSearcher.GetAsync(id);
        }

        public Task<Employee> CreateAsync(EmployeeDTO createEmployeeDTO)
        {
            return _employeeCreator.CreateAsync(createEmployeeDTO);
        }

        public Task<Employee> UpdateAsync(EmployeeDTO updateEmployeeDTO)
        {
            return _employeeUpdater.UpdateAsync(updateEmployeeDTO);
        }

        public Task DeleteAsync(Guid id)
        {
            return _employeeRemover.DeleteAsync(id);
        }
    }
}
