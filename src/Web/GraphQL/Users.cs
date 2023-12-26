using Security.Application.Users.Commands.CreateUser;
using Security.Application.Users.Commands.DeleteUser;
using Security.Application.Users.Commands.UpdateUser;
using Security.Application.Users.Queries.GetUsers;
using Security.Application.Users.Queries.GetUsersByAccount;
using Security.Domain.Models;

namespace Security.Web.GraphQL;

public class Users
{
    public async Task<UserVm> GetUser([Service] ISender sender, [AsParameters] GetUsersQuery query)
        => await sender.Send(query);

    public async Task<IReadOnlyCollection<UserVm>> GetUsers([Service] ISender sender, [AsParameters] GetUsersByAccountQuery query)
        => await sender.Send(query);

    public async Task<UserVm> GetUsers([Service] ISender sender, CreateUserCommand command)
        => await sender.Send(command);

    public async Task<IResult> UpdateUser(ISender sender, Guid id, UpdateUserCommand command)
    {
        if (id != command.User.UserId) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteUser(ISender sender, Guid id)
    {
        await sender.Send(new DeleteUserCommand(id));
        return Results.NoContent();
    }
}
