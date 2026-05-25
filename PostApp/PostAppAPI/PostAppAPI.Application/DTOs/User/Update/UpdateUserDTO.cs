
namespace PostAppAPI.Application.DTOs.User.Update
{
    public class UpdateUserDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Mail { get; set; }
        public string? Password { get; set; }
    }
}
