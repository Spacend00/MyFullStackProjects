using MediatR;

namespace PostAppAPI.Application.Features.Post.Command.Update
{
    public class UpdatePostCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string? Content { get; set; }
    }
}
