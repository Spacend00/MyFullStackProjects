
using MediatR;

namespace PostAppAPI.Application.Features.User.Command.LoginUser
{
    public class LoginUserCommand : IRequest<string>
    {
        public string Mail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}
