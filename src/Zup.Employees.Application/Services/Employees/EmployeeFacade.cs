using Zup.Employees.Application.Services.EmployeeContacts;
using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.Employees.Interfaces;
using Zup.Employees.Domain.Mappings;
using Zup.Employees.Security.Domain.DTOs;
using Zup.Employees.Security.Domain.Interfaces;

namespace Zup.Employees.Application.Services.Employees
{
    public class EmployeeFacade : IEmployeeFacade
    {
        private readonly IEmployeeCreator _employeeCreator;
        private readonly IEmployeeUpdater _employeeUpdater;
        private readonly IEmployeeRemover _employeeRemover;
        private readonly IEmployeeSearcher _employeeSearcher;
        private readonly IEmployeeContactFacade _employeeContactFacade;
        private readonly IPasswordHasher _passwordHasher;

        public EmployeeFacade
        (
            IEmployeeCreator employeeCreator,
            IEmployeeUpdater employeeUpdater,
            IEmployeeRemover employeeRemover,
            IEmployeeSearcher employeeSearcher,
            IEmployeeContactFacade employeeContactFacade,
            IPasswordHasher passwordHasher
        )
        {
            _employeeCreator = employeeCreator;
            _employeeUpdater = employeeUpdater;
            _employeeRemover = employeeRemover;
            _employeeSearcher = employeeSearcher;
            _employeeContactFacade = employeeContactFacade;
            _passwordHasher = passwordHasher;
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

        public async Task ChangePasswordAsync(ChangePasswordDTO changePasswordDTO)
        {
            var employee = await _employeeSearcher.GetForLoginAsync(changePasswordDTO.Email, _passwordHasher.HashPassword(changePasswordDTO.OldPassword));
            if (employee != null)
            {
                employee.UpdatePassword(_passwordHasher.HashPassword(changePasswordDTO.NewPassword));
            }
        }

        public async Task CreatePasswordAsync(CreatePasswordDTO createPasswordDTO)
        {
            var employee = await _employeeSearcher.GetForLoginAsync(createPasswordDTO.Email, string.Empty);
            if (employee != null)
            {
                employee.UpdatePassword(_passwordHasher.HashPassword(createPasswordDTO.Password));
            }
        }
    }
}
