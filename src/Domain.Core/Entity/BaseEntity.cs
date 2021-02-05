
namespace PapoDeDev.TDD.Domain.Core.Entity
{
    public abstract class BaseEntity { }
    public abstract class BaseEntity<T> : BaseEntity where T : struct
    {
        public T Id { get; set; }
    }
}
