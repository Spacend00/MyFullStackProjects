
using MediatR;
using PostAppAPI.Application.DTOs.Post.Query;

namespace PostAppAPI.Application.Features.Post.Query.GetPost
{
    public class GetPostQuery : IRequest<PostViewDTO>
    {
        public Guid Id { get; set; }

        public GetPostQuery(Guid id)
        {
            Id = id;
        }
    }
}
