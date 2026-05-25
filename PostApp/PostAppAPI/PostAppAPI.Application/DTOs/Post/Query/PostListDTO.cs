
using PostAppAPI.Application.DTOs.User.Query;

namespace PostAppAPI.Application.DTOs.Post.Query
{
    public class PostListDTO
    {
        public Guid Id { get; set; }
        public string? Content { get; set; }
        public required UserSimpleDTO User { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
