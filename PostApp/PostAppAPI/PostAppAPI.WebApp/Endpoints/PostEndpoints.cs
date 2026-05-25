using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using PostAppAPI.Application.DTOs.Post.Create;
using PostAppAPI.Application.DTOs.Post.Delete;
using PostAppAPI.Application.DTOs.Post.Update;
using PostAppAPI.Application.Features.Post.Command.Create;
using PostAppAPI.Application.Features.Post.Command.Delete;
using PostAppAPI.Application.Features.Post.Command.Update;
using PostAppAPI.Application.Features.Post.Query.GetAllPosts;
using PostAppAPI.Application.Features.Post.Query.GetPost;

namespace PostAppAPI.WebApp.Endpoints
{
    public static class PostEndpoints
    {
        public static void MapPostEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/post")
                       .WithTags("Post");

            group.MapGet("/GetAll/", [Authorize] async(IMediator mediator) =>
            {
                var posts = await mediator.Send(new GetAllPostsQuery());
                return Results.Ok(posts);
            });

            group.MapGet("/Get/{id:Guid}", [Authorize] async (Guid id, IMediator mediator) =>
            {
                var post = await mediator.Send(new GetPostQuery(id));
                return post is not null ? Results.Ok(post) : Results.NotFound(); 
            });

            group.MapPost("/Create/", [Authorize] async (CreatePostDTO createPost, IMediator mediator, IMapper mapper) =>
            {
                var command = mapper.Map<CreatePostCommand>(createPost);
                var newPostId = await mediator.Send(command);
                return Results.Created($"/{newPostId}", newPostId);
            });

            group.MapPut("/Update/", [Authorize] async (UpdatePostDTO updatePost, IMediator mediator, IMapper mapper) =>
            {
                var command = mapper.Map<UpdatePostCommand>(updatePost);
                await mediator.Send(command);
                return Results.NoContent();
            });

            group.MapDelete("/Delete/{id:Guid}", [Authorize] async (Guid id, IMediator mediator, IMapper mapper) =>
            {
                var dto = new DeletePostDTO { Id = id };
                var command = mapper.Map<DeletePostCommand>(dto);
                await mediator.Send(command);
                Results.NoContent();
            });
        }
    }
}
