
namespace PostAppAPI.Application.DTOs.User.Query
{
    public class UserListDTO
    {
        public Guid Id { get; set; }
        public required string FullName { get; set; }
    }
}
