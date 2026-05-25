
using MediatR;

namespace PostAppAPI.Application.Features.User.Command.Delete
{
    public class DeleteUserCommand :IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
