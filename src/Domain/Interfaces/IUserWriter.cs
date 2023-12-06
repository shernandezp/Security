using Security.Domain.Models;
using Security.Domain.Records;

namespace Security.Domain.Interfaces;
public interface IUserWriter
{
    Task<UserVm> CreateUserAsync(UserDto userDto, CancellationToken cancellationToken = default);
    Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task UpdatePasswordAsync(UserPasswordDto userPasswordDto, CancellationToken cancellationToken = default);
    Task UpdateUserAsync(UserDto userDto, CancellationToken cancellationToken = default);
}
