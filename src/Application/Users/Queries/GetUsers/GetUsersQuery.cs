using Security.Domain.Interfaces;
using Security.Domain.Models;

namespace Security.Application.Users.Queries.GetUsers;

public record GetUsersQuery() : IRequest<UserVm>
{
    public required Guid Id { get; init; }
}

public class GetUsersQueryHandler(IUserReader reader) : IRequestHandler<GetUsersQuery, UserVm>
{
    public async Task<UserVm> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await reader.GetUserAsync(request.Id, cancellationToken);
    }
}
