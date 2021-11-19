using Zup.Employees.Domain.Employees.Entities;

namespace Zup.Employees.Domain.EmployeeContacts.Entities
{
    public class Contact : EntityBase
    {
        public Contact() { }

        public Contact(string number, Guid employeeId)
        {
            Number = number;
            EmployeeId = employeeId;
        }

        public string Number { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
