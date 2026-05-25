
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PostAppAPI.Application.DTOs.User.Query;
using PostAppAPI.Application.Interfaces;

namespace PostAppAPI.Application.Features.User.Query.GetUser
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserViewDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        async Task<UserViewDTO> IRequestHandler<GetUserQuery, UserViewDTO>.Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetAsync(
                predicate: x => x.Id == request.Id,
                include: q => q.Include(u => u.Posts),
                withTracking: false);

            if (user == null) throw new KeyNotFoundException("Kullanıcı bulunamadı!");

            return _mapper.Map<UserViewDTO>(user);
        }
    }
}
