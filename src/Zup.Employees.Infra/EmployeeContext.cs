using Microsoft.EntityFrameworkCore;
using Zup.Employees.Infra.Mappings;

namespace Zup.Employees.Infra;

public class EmployeeContext : DbContext
{
    protected EmployeeContext() { }

    public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EmployeeMapping());
        modelBuilder.ApplyConfiguration(new EmployeeContactMapping());

        base.OnModelCreating(modelBuilder);
    }
}
