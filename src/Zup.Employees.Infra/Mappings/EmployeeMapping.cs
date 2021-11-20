using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zup.Employees.Domain.Employees.Entities;

namespace Zup.Employees.Infra.Mappings;

public class EmployeeMapping : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Surname).IsRequired();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.PlateNumber).IsRequired();
        builder.Property(x => x.PasswordHash);
        builder.Property(x => x.IsLeader);

        builder.HasMany(x => x.Contacts)
               .WithOne(x => x.Employee)
               .HasForeignKey(x => x.EmployeeId);

        builder.HasIndex(x => x.PlateNumber).IsUnique();
    }
}
