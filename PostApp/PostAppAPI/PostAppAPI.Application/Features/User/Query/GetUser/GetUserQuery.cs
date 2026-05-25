
using MediatR;
using PostAppAPI.Application.DTOs.User.Query;

namespace PostAppAPI.Application.Features.User.Query.GetUser
{
    public class GetUserQuery : IRequest<UserViewDTO>
    {
        public Guid Id { get; set; }

        public GetUserQuery(Guid id)
        {
            Id = id;
        }
    }
}
