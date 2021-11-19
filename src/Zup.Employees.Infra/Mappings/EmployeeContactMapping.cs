using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zup.Employees.Domain.EmployeeContacts.Entities;

namespace Zup.Employees.Infra.Mappings;

public class EmployeeContactMapping : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.Property(x => x.Number).IsRequired();
        builder.Property(x => x.EmployeeId);

        builder.HasOne(x => x.Employee)
               .WithMany(x => x.Contacts)
               .HasForeignKey(x => x.EmployeeId);
    }
}
