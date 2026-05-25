
using AutoMapper;
using MediatR;
using PostAppAPI.Application.Interfaces;

namespace PostAppAPI.Application.Features.User.Command.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.Users.GetAsync(u => u.Id == request.Id);
            if (existingUser == null) throw new Exception("Güncellenecek kullanıcı bulunamadı!");
            _mapper.Map(request, existingUser);
            _unitOfWork.Users.Update(existingUser);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
