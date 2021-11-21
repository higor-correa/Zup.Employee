using System.Collections.ObjectModel;
using Zup.Employees.Domain.EmployeeContacts.Entities;

namespace Zup.Employees.Domain.Employees.Entities
{
    public class Employee : EntityBase
    {
        public Employee() { }
        public Employee(string name, string surname, string email, int plateNumber, string passwordHash, bool isLeader, Employee? leader)
        {
            Name = name;
            Surname = surname;
            Email = email;
            PlateNumber = plateNumber;
            PasswordHash = passwordHash ?? string.Empty;
            IsLeader = isLeader;
            Leader = leader;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int PlateNumber { get; set; }
        public string PasswordHash { get; set; }
        public bool IsLeader { get; set; }
        public Employee? Leader { get; set; }
        public ICollection<Contact> Contacts { get; set; } = new Collection<Contact>();

        public void UpdatePassword(string passwordHashed)
        {
            PasswordHash = passwordHashed;
        }
    }
}
