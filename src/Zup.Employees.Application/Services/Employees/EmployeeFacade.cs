using Zup.Employees.Application.Services.EmployeeContacts;
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
        private readonly IEmployeeContactFacade _employeeContactFacade;

        public EmployeeFacade
        (
            IEmployeeCreator employeeCreator,
            IEmployeeUpdater employeeUpdater,
            IEmployeeRemover employeeRemover,
            IEmployeeSearcher employeeSearcher,
            IEmployeeContactFacade employeeContactFacade
        )
        {
            _employeeCreator = employeeCreator;
            _employeeUpdater = employeeUpdater;
            _employeeRemover = employeeRemover;
            _employeeSearcher = employeeSearcher;
            _employeeContactFacade = employeeContactFacade;
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

        public async Task DeleteAsync(Guid id)
        {
            await _employeeContactFacade.DeleteAllFromEmployee(id);

            await _employeeRemover.DeleteAsync(id);
        }
    }
}
