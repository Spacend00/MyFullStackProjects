
using AutoMapper;
using MediatR;
using PostAppAPI.Application.DTOs.User.Query;
using PostAppAPI.Application.Interfaces;

namespace PostAppAPI.Application.Features.User.Query.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserListDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        async Task<IEnumerable<UserListDTO>> IRequestHandler<GetAllUsersQuery, IEnumerable<UserListDTO>>.Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.Users.GetAllAsync(withTracking: false);
            var mappedUsers = _mapper.Map<IEnumerable<UserListDTO>>(users);
            return mappedUsers;
        }
    }
}
