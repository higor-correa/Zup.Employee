using Microsoft.EntityFrameworkCore;
using Zup.Employees.Domain.EmployeeContacts.Entities;
using Zup.Employees.Domain.EmployeeContacts.Interfaces;

namespace Zup.Employees.Infra.Repositories;

public class EmployeeContactRepository : RepositoryBase<Contact>, IEmployeeContactRepository
{
    public EmployeeContactRepository(EmployeeContext employeeContext) : base(employeeContext)
    {
    }

    public async Task<IEnumerable<Contact>> GetAllFromEmployeeAsync(Guid employeeId)
    {
        return await Set.Where(x => x.EmployeeId == employeeId).ToListAsync();
    }
}
