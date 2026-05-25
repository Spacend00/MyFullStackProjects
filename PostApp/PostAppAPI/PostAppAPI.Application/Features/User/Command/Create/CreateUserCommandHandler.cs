
using AutoMapper;
using MediatR;
using PostAppAPI.Application.Interfaces;

namespace PostAppAPI.Application.Features.User.Command.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork,IPasswordService passwordService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            _passwordService.CreatePasswordHash(request.Password, out byte[] pHash, out byte[] pSalt);

            var user = _mapper.Map<Domain.Entities.User>(request);
            user.PasswordHash = pHash;
            user.PasswordSalt = pSalt;
            await _unitOfWork.Users.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return user.Id;
        }
    }
}
