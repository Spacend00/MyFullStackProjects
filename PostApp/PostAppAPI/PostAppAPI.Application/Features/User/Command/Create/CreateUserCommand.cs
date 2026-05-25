using MediatR;

namespace PostAppAPI.Application.Features.User.Command.Create
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Mail { get; set; }
        public required string Password { get; set; }
    }
}
