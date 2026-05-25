
using AutoMapper;
using MediatR;
using PostAppAPI.Application.Interfaces;

namespace PostAppAPI.Application.Features.Post.Command.Update
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdatePostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var existingPost = await _unitOfWork.Posts.GetAsync(p => p.Id == request.Id);
            if (existingPost == null) throw new Exception("Post bulunamadı!");
            _mapper.Map(request, existingPost);
            _unitOfWork.Posts.Update(existingPost);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
