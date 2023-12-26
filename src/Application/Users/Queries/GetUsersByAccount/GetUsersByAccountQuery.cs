using Security.Domain.Interfaces;
using Security.Domain.Models;

namespace Security.Application.Users.Queries.GetUsersByAccount;

public record GetUsersByAccountQuery(Guid AccountId) : IRequest<IReadOnlyCollection<UserVm>>;

public class GetUsersByAccountQueryHandler(IUserReader reader) : IRequestHandler<GetUsersByAccountQuery, IReadOnlyCollection<UserVm>>
{
    public async Task<IReadOnlyCollection<UserVm>> Handle(GetUsersByAccountQuery request, CancellationToken cancellationToken)
    {
        return await reader.GetUserByAccountAsync(request.AccountId, cancellationToken);
    }
}
