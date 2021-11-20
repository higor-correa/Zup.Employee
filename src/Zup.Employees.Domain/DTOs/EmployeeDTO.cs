namespace Zup.Employees.Domain.DTOs
{
    public class EmployeeDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int PlateNumber { get; set; }
        public bool IsLeader { get; set; }
        public Guid LeaderId { get; set; }
        public string? LeaderName { get; set; }
    }
}
