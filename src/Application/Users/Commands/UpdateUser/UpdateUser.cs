﻿using Security.Domain.Interfaces;
using Security.Domain.Records;

namespace Security.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand : IRequest
{
    public UserDto User { get; init; }
}

public class UpdateUserCommandHandler(IUserWriter writer) : IRequestHandler<UpdateUserCommand>
{

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await writer.UpdateUserAsync(request.User, cancellationToken);

        //notify
    }
}