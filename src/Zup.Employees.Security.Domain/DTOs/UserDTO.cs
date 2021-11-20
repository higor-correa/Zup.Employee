namespace Zup.Employees.Security.Domain.DTOs;

public class UserDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public Guid LeaderId { get; set; }
    public string LeaderName { get; set; }
}
