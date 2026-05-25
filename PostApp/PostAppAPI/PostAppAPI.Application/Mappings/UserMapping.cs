
using AutoMapper;
using PostAppAPI.Application.DTOs.User.Create;
using PostAppAPI.Application.DTOs.User.Delete;
using PostAppAPI.Application.DTOs.User.Login;
using PostAppAPI.Application.DTOs.User.Query;
using PostAppAPI.Application.DTOs.User.Update;
using PostAppAPI.Application.Features.User.Command.Create;
using PostAppAPI.Application.Features.User.Command.Delete;
using PostAppAPI.Application.Features.User.Command.LoginUser;
using PostAppAPI.Application.Features.User.Command.Update;
using PostAppAPI.Domain.Entities;

namespace PostAppAPI.Application.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<CreateUserDTO, CreateUserCommand>().ReverseMap();
            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.PasswordHash, opts => opts.Ignore())
                .ForMember(dest => dest.PasswordSalt, opts => opts.Ignore());

            CreateMap<UpdateUserDTO, UpdateUserCommand>().ReverseMap();
            CreateMap<UpdateUserCommand, User>()
                .ForAllMembers(opts => opts.Condition((src, member, srcMember) => srcMember != null));

            CreateMap<DeleteUserDTO, DeleteUserCommand>().ReverseMap();

            CreateMap<LoginUserDTO, LoginUserCommand>();

            CreateMap<User, UserViewDTO>();
            CreateMap<User, UserListDTO>();
            CreateMap<User, UserSimpleDTO>();
        }
    }
}
