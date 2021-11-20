using Zup.Employees.Domain.Employees.Interfaces;

namespace Zup.Employees.Domain.Employees.Services;

public class EmployeeRemover : IEmployeeRemover
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeRemover(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task DeleteAsync(Guid id)
    {
        var employee = await _employeeRepository.GetAsync(id);

        if (employee != null)
        {
            await _employeeRepository.DeleteAsync(employee);
        }
    }
}
