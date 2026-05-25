
using PostAppAPI.Domain.Entities;

namespace PostAppAPI.Application.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
