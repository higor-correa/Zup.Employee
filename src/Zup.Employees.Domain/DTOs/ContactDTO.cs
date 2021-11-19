namespace Zup.Employees.Domain.DTOs
{
    public class ContactDTO
    {
        public Guid Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public Guid EmployeeId { get; set; }
    }
}
