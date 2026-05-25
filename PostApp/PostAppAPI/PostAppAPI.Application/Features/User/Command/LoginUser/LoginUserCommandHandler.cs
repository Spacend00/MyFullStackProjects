
using MediatR;
using PostAppAPI.Application.Interfaces;

namespace PostAppAPI.Application.Features.User.Command.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        public LoginUserCommandHandler(IUnitOfWork unitOfWork, IPasswordService passwordService, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }
        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetAsync(u => u.Mail == request.Mail);
            if (user == null) throw new Exception("E-posta veya şifre hatalı!");
            var isVerified = _passwordService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt);
            if (!isVerified) throw new Exception("E-posta veya şifre hatalı!");
            return _tokenService.CreateToken(user);
        }
    }
}
