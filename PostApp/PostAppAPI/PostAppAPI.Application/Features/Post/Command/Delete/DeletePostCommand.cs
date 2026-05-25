
using MediatR;

namespace PostAppAPI.Application.Features.Post.Command.Delete
{
    public class DeletePostCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
