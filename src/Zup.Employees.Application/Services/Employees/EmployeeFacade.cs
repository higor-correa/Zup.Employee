using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.Employees.Interfaces;
using Zup.Employees.Domain.Mappings;

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

        public async Task<IEnumerable<EmployeeDTO>> GetAsync()
        {
            var employees = await _employeeSearcher.GetAsync();
            return employees.Select(x => x.ToDto());

        }

        public async Task<EmployeeDTO?> GetAsync(Guid id)
        {
            var employee = await _employeeSearcher.GetAsync(id);
            return employee?.ToDto();
        }

        public async Task<EmployeeDTO> CreateAsync(EmployeeDTO createEmployeeDTO)
        {
            var employee = await _employeeCreator.CreateAsync(createEmployeeDTO);
            return employee.ToDto();
        }

        public async Task<EmployeeDTO?> UpdateAsync(EmployeeDTO updateEmployeeDTO)
        {
            var employee = await _employeeUpdater.UpdateAsync(updateEmployeeDTO);
            return employee?.ToDto();
        }

        public Task DeleteAsync(Guid id)
        {
            return _employeeRemover.DeleteAsync(id);
        }
    }
}
