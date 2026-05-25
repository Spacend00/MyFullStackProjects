using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using PostAppAPI.Application.DTOs.User.Create;
using PostAppAPI.Application.DTOs.User.Delete;
using PostAppAPI.Application.DTOs.User.Login;
using PostAppAPI.Application.DTOs.User.Update;
using PostAppAPI.Application.Features.User.Command.Create;
using PostAppAPI.Application.Features.User.Command.Delete;
using PostAppAPI.Application.Features.User.Command.LoginUser;
using PostAppAPI.Application.Features.User.Command.Update;
using PostAppAPI.Application.Features.User.Query.GetAllUsers;
using PostAppAPI.Application.Features.User.Query.GetUser;

namespace PostAppAPI.WebApp.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/user")
                           .WithTags("User");

            group.MapGet("/GetAll/", async(IMediator mediator) =>
            {
                var users = await mediator.Send(new GetAllUsersQuery());
                return Results.Ok(users);
            });

            group.MapGet("/Get/{id:Guid}", async(Guid id, IMediator mediator) =>
            {
                var user = await mediator.Send(new GetUserQuery(id));
                return user is not null ? Results.Ok(user) : Results.NotFound();
            });

            group.MapPost("/Create/", async (CreateUserDTO createUser, IMediator mediator, IMapper mapper) =>
            {
                var command = mapper.Map<CreateUserCommand>(createUser);
                var newUserId = await mediator.Send(command);
                return Results.Created($"/{newUserId}", newUserId);
            });

            group.MapPut("/Update/", [Authorize] async(UpdateUserDTO updateUser, IMediator mediator, IMapper mapper) =>
            {
                var command = mapper.Map<UpdateUserCommand>(updateUser);
                await mediator.Send(command);
                return Results.NoContent;
            });

            group.MapDelete("/Delete/{id:Guid}", [Authorize] async (Guid id, IMediator mediator, IMapper mapper) =>
            {
                var dto = new DeleteUserDTO { Id = id };
                var command = mapper.Map<DeleteUserCommand>(dto);
                await mediator.Send(command);
                return Results.NoContent;
            });

            group.MapPost("/Login/", async (LoginUserDTO loginUser, IMediator mediator, IMapper mapper) =>
            {
                var command = mapper.Map<LoginUserCommand>(loginUser);
                var result = await mediator.Send(command);
                return Results.Ok(result);
            });
        }
    }
}
