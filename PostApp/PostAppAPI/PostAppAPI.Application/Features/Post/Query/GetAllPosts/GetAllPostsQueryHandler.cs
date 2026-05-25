
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PostAppAPI.Application.DTOs.Post.Query;
using PostAppAPI.Application.Interfaces;

namespace PostAppAPI.Application.Features.Post.Query.GetAllPosts
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, IEnumerable<PostListDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllPostsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<PostListDTO>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _unitOfWork.Posts.GetAllAsync(
                include: x => x.Include(p => p.User),
                withTracking: false);
            var mappedPosts = _mapper.Map<IEnumerable<PostListDTO>>(posts);
            return mappedPosts;
        }
    }
}
