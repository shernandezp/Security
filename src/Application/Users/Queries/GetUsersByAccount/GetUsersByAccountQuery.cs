using Security.Domain.Interfaces;
using Security.Domain.Models;

namespace Security.Application.Users.Queries.GetUsersByAccount;

public record GetUsersByAccountQuery() : IRequest<IReadOnlyCollection<UserVm>>
{
    public required Guid AccountId { get; init; }
}

public class GetUsersByAccountQueryHandler(IUserReader reader) : IRequestHandler<GetUsersByAccountQuery, IReadOnlyCollection<UserVm>>
{
    public async Task<IReadOnlyCollection<UserVm>> Handle(GetUsersByAccountQuery request, CancellationToken cancellationToken)
    {
        return await reader.GetUserByAccountAsync(request.AccountId, cancellationToken);
    }
}
