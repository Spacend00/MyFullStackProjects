
using MediatR;

namespace PostAppAPI.Application.Features.Post.Command.Create
{
    public class CreatePostCommand : IRequest<bool>
    {
        public required string Content { get; set; }
    }
}
