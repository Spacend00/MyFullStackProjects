
using AutoMapper;
using MediatR;
using PostAppAPI.Application.Interfaces;

namespace PostAppAPI.Application.Features.Post.Command.Create
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public CreatePostCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId)) throw new UnauthorizedAccessException();
            var post = _mapper.Map<Domain.Entities.Post>(request);
            post.UserId = Guid.Parse(userId);
            post.Id = Guid.NewGuid();
            await _unitOfWork.Posts.CreateAsync(post);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
