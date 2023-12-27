using Security.Domain.Interfaces;
using Security.Domain.Models;

namespace Security.Application.Users.Queries.GetUser;

public record GetUserQuery() : IRequest<UserVm>
{
    public required Guid Id { get; init; }
}

public class GetUsersQueryHandler(IUserReader reader) : IRequestHandler<GetUserQuery, UserVm>
{
    public async Task<UserVm> Handle(GetUserQuery request, CancellationToken cancellationToken)
        => await reader.GetUserAsync(request.Id, cancellationToken);

}
