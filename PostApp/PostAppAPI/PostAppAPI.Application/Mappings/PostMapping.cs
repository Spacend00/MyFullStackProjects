
using AutoMapper;
using PostAppAPI.Application.DTOs.Post.Create;
using PostAppAPI.Application.DTOs.Post.Delete;
using PostAppAPI.Application.DTOs.Post.Query;
using PostAppAPI.Application.DTOs.Post.Update;
using PostAppAPI.Application.Features.Post.Command.Create;
using PostAppAPI.Application.Features.Post.Command.Delete;
using PostAppAPI.Application.Features.Post.Command.Update;
using PostAppAPI.Domain.Entities;

namespace PostAppAPI.Application.Mappings
{
    public class PostMapping : Profile
    {
        public PostMapping()
        {
            CreateMap<CreatePostDTO, CreatePostCommand>().ReverseMap();
            CreateMap<CreatePostCommand, Post>()
                .ForMember(dest => dest.UserId, opts => opts.Ignore());

            CreateMap<UpdatePostDTO, UpdatePostCommand>().ReverseMap();
            CreateMap<UpdatePostCommand, Post>()
                .ForAllMembers(opts => opts.Condition((src, member, srcMember) => srcMember != null));

            CreateMap<DeletePostDTO, DeletePostCommand>().ReverseMap();

            CreateMap<Post, PostViewDTO>();
            CreateMap<Post, PostListDTO>();
            CreateMap<Post, PostSimpleDTO>();
        }
    }
}
