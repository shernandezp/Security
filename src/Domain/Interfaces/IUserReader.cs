namespace Security.Domain.Interfaces;
public interface IUserReader
{
    Task<UserVm> GetUserAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<UserVm>> GetUserByAccountAsync(Guid accountId, CancellationToken cancellationToken);
}
