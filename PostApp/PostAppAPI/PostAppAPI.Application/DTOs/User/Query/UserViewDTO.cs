
using PostAppAPI.Application.DTOs.Post.Query;

namespace PostAppAPI.Application.DTOs.User.Query
{
    public class UserViewDTO
    {
        public Guid Id { get; set; }
        public required string FullName { get; set; }
        public required string Mail { get; set; }
        public List<PostSimpleDTO>? Posts { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
