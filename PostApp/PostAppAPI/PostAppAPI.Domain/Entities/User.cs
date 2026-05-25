
using PostAppAPI.Domain.Common;

namespace PostAppAPI.Domain.Entities
{
    public class User : BaseEntity
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string FullName => $"{Name} {Surname}";
        public required string Mail { get; set; }
        public required byte[] PasswordHash { get; set; }
        public required byte[] PasswordSalt { get; set; }
        public ICollection<Post> Posts { get;} = new List<Post>();
    }
}
