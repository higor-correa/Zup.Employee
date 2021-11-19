using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.Employees.Entities;
using Zup.Employees.Domain.Employees.Interfaces;

namespace Zup.Employees.Domain.Employees.Services
{
    public class EmployeeUpdater : IEmployeeUpdater
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeSearcher _employeeSearcher;

        public EmployeeUpdater(IEmployeeRepository employeeRepository, IEmployeeSearcher employeeSearcher)
        {
            _employeeRepository = employeeRepository;
            _employeeSearcher = employeeSearcher;
        }

        public async Task<Employee> UpdateAsync(EmployeeDTO updateEmployeeDTO)
        {
            var employee = await _employeeRepository.GetAsync(updateEmployeeDTO.Id);

            await UpdatePropertiesAsync(updateEmployeeDTO, employee!);

            await _employeeRepository.UpdateAsync(employee!);

            return employee!;
        }

        private async Task UpdatePropertiesAsync(EmployeeDTO updateEmployeeDTO, Employee employee)
        {
            employee.Name = updateEmployeeDTO.Name;
            employee.Surname = updateEmployeeDTO.Surname;
            employee.PlateNumber = updateEmployeeDTO.PlateNumber;
            employee.Email = updateEmployeeDTO.Email;
            employee.IsLeader = updateEmployeeDTO.IsLeader;
            employee.Leader = await _employeeSearcher.GetAsync(updateEmployeeDTO.LeaderId);
        }
    }
}
