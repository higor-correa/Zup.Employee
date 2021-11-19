using Zup.Employees.Domain.DTOs;
using Zup.Employees.Domain.Employees.Entities;
using Zup.Employees.Domain.Employees.Interfaces;

namespace Zup.Employees.Domain.Employees.Services
{
    public class EmployeeCreator : IEmployeeCreator
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEmployeeSearcher _employeeSearcher;

        public EmployeeCreator(IEmployeeRepository employeeRepository, IEmployeeSearcher employeeSearcher)
        {
            _employeeRepository = employeeRepository;
            _employeeSearcher = employeeSearcher;
        }

        public async Task<Employee> CreateAsync(EmployeeDTO createEmployeeDTO)
        {
            var employee = new Employee(
                createEmployeeDTO.Name,
                createEmployeeDTO.Surname,
                createEmployeeDTO.Email,
                createEmployeeDTO.PlateNumber,
                createEmployeeDTO.Name + createEmployeeDTO.Surname,
                createEmployeeDTO.IsLeader,
                await _employeeSearcher.GetAsync(createEmployeeDTO.LeaderId)
            );

            return await _employeeRepository.CreateAsync(employee);
        }
    }
}
