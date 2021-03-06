namespace Zup.Employees.Domain.EmployeeContacts.Interfaces
{
    public interface IEmployeeContactRemover
    {
        Task DeleteAsync(Guid id);
        Task DeleteAllFromEmployee(Guid employeeId);
    }
}
