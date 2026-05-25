
using PostAppAPI.Domain.Common;

namespace PostAppAPI.Domain.Entities
{
    public class Post : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public required string Content { get; set; }
    }
}
