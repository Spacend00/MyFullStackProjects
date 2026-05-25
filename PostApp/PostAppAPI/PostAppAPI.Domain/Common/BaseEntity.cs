
using PostAppAPI.Domain.Interfaces;

namespace PostAppAPI.Domain.Common
{
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;
    }
}
