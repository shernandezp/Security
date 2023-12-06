using Security.Domain.Interfaces;
using Security.Infrastructure.Interfaces;

namespace Security.Infrastructure.Readers;
public sealed class UserReader(IApplicationDbContext context) : IUserReader
{
    public async Task<UserVm> GetUserAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users
            .AsNoTracking()
            .Where(u => u.UserId.Equals(id))
            .Include(role => role.Roles)
            .Include(user => user.Profiles)
            .Select(u => new UserVm(
                u.UserId,
                u.Username,
                u.Password,
                u.Email,
                u.FirstName,
                u.SecondName,
                u.LastName,
                u.SeconSurname,
                u.DOB,
                u.Roles.Select(r => new RoleVm(r.Name)).ToList(),
                u.Profiles.Select(p => new ProfileVm(p.Name)).ToList()))
            .FirstAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<UserVm>> GetUserByAccountAsync(Guid accountId, CancellationToken cancellationToken)
    {
        return await context.Users
            .AsNoTracking()
            .Where(u => u.AccountId.Equals(accountId))
            .Select(u => new UserVm(
                u.UserId,
                u.Username,
                string.Empty,
                u.Email,
                u.FirstName,
                u.SecondName,
                u.LastName,
                u.SeconSurname,
                u.DOB,
                Enumerable.Empty<RoleVm>(),
                Enumerable.Empty<ProfileVm>()))
            .ToListAsync(cancellationToken);
    }
}
