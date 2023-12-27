using Security.Application.Users.Events;
using Security.Domain.Interfaces;

namespace Security.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id) : IRequest;

public class DeleteUserCommandHandler(IUserWriter writer, IPublisher publisher) : IRequestHandler<DeleteUserCommand>
{

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await writer.DeleteUserAsync(request.Id, cancellationToken);

        await publisher.Publish(new UserDeleted.Notification(request.Id), cancellationToken);
    }

}
