
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PostAppAPI.Application.DTOs.Post.Query;
using PostAppAPI.Application.Interfaces;
using PostAppAPI.Domain.Entities;

namespace PostAppAPI.Application.Features.Post.Query.GetPost
{
    public class GetPostQueryHandler : IRequestHandler<GetPostQuery, PostViewDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPostQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PostViewDTO> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.Posts.GetAsync(
                predicate: x => x.Id == request.Id,
                include: q => q.Include(p => p.User),
                withTracking: false);

            if (post == null) throw new Exception("Paylaşım bulunamadı!");

            return _mapper.Map<PostViewDTO>(post);
        }
    }
}
