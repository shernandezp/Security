﻿using Security.Application.Users.Events;
using Security.Domain.Interfaces;
using Security.Domain.Models;
using Security.Domain.Records;

namespace Security.Application.Users.Commands.CreateUser;

public record CreateUserCommand : IRequest<UserVm>
{
    public required UserDto User { get; init; }
}

public class CreateUserCommandHandler(IUserWriter writer, IPublisher publisher) : IRequestHandler<CreateUserCommand, UserVm>
{
    public async Task<UserVm> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await writer.CreateUserAsync(request.User, cancellationToken);
        
        await publisher.Publish(new UserCreated.Notification(user.UserId), cancellationToken);

        return user;
    }
}
