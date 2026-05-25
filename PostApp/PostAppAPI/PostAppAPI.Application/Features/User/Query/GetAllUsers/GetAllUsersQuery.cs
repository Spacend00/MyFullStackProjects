
using MediatR;
using PostAppAPI.Application.DTOs.User.Query;

namespace PostAppAPI.Application.Features.User.Query.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<UserListDTO>>
    {
    }
}
