
using MediatR;
using PostAppAPI.Application.Interfaces;

namespace PostAppAPI.Application.Features.Post.Command.Delete
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePostCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _unitOfWork.Posts.GetAsync(p => p.Id == request.Id);
            if (post == null) throw new Exception("Silinecek post bulunamadı!");
            _unitOfWork.Posts.Delete(post);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
