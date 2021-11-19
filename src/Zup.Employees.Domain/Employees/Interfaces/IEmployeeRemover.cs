namespace Zup.Employees.Domain.Employees.Interfaces
{
    public interface IEmployeeRemover
    {
        Task DeleteAsync(Guid id);
    }
}
