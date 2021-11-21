using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography;
using System.Text;
using Zup.Employees.Domain.Employees.Entities;

namespace Zup.Employees.Infra.Mappings;

public class EmployeeMapping : IEntityTypeConfiguration<Employee>
{
    private const string AdminId = "661b8028-6ce0-4544-950d-18837c2bcd7e";
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
        builder.HasData(new Employee("Admin", string.Empty, "admin@admin.com", 0, GetPassword(), true, null)
        {
            Id = Guid.Parse(AdminId)
        });
    }

    private static string GetPassword()
    {
        return SHA256.Create()
                     .ComputeHash(Encoding.UTF8.GetBytes("admin"))
                     .Select(x => string.Format("{0:x2}", x))
                     .Aggregate((@byte, hash) => hash + @byte);
    }
}
