using Zup.Employees.Domain.Interfaces;

namespace Zup.Employees.Domain
{
    public abstract class EntityBase : IEntity
    {
        public EntityBase() { }

        public Guid Id { get; set; }
    }
}
