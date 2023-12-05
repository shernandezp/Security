using AutoMapper;
using Security.Application.Common.Interfaces;

namespace Security.Application.Users.Queries.GetUsers;

public record GetUsersQuery() : IRequest<UserDto>
{
    public required string Id { get; init; }
}

public class GetUsersQueryHandler(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetUsersQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await context.Users
            .Where(u => u.Id.Equals(request.Id))
            .Include(role => role.Roles)
            .Include(user => user.Profiles)
            .ProjectTo<UserDto>(mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken);
    }
}
