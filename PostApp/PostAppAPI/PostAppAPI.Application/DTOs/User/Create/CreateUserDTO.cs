
namespace PostAppAPI.Application.DTOs.User.Create
{
    public class CreateUserDTO
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Mail { get; set; }
        public required string Password { get; set; }
    }
}
