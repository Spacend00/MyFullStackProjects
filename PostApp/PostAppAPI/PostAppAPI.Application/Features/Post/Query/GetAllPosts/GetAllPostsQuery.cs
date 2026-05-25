
using MediatR;
using PostAppAPI.Application.DTOs.Post.Query;

namespace PostAppAPI.Application.Features.Post.Query.GetAllPosts
{
    public class GetAllPostsQuery : IRequest<IEnumerable<PostListDTO>>
    {
    }
}
