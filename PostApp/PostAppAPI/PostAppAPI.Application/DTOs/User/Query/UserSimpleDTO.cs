
namespace PostAppAPI.Application.DTOs.User.Query
{
    public class UserSimpleDTO
    {
        public Guid Id { get; set; }
        public required string FullName { get; set; }
    }
}
