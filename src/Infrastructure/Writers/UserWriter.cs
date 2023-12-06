using Security.Domain.Interfaces;
using Security.Domain.Records;
using Security.Infrastructure.Interfaces;

namespace Security.Infrastructure.Writers;

public sealed class UserWriter(IApplicationDbContext context) : IUserWriter
{
    public async Task<UserVm> CreateUserAsync(UserDto userDto, CancellationToken cancellationToken = default)
    {
        var user = new User(
            userDto.Username,
            userDto.Password,
            userDto.Email,
            userDto.FirstName,
            userDto.SecondName,
            userDto.LastName,
            userDto.SeconSurname,
            userDto.DOB);

        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return new UserVm(
            user.UserId,
            user.Username,
            user.Password,
            user.Email,
            user.FirstName,
            user.SecondName,
            user.LastName,
            user.SeconSurname,
            user.DOB,
            Enumerable.Empty<RoleVm>(),
            Enumerable.Empty<ProfileVm>());
    }

    public async Task UpdateUserAsync(UserDto userDto, CancellationToken cancellationToken = default)
    {
        var user = await context.Users.FindAsync([userDto.UserId], cancellationToken)
            ?? throw new NotFoundException(nameof(User), $"{userDto.UserId}");

        user.Username = userDto.Username;
        user.Email = userDto.Email;
        user.FirstName = userDto.FirstName;
        user.SecondName = userDto.SecondName;
        user.LastName = userDto.LastName;
        user.SeconSurname = userDto.SeconSurname;
        user.DOB = userDto.DOB;
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdatePasswordAsync(UserPasswordDto userPasswordDto, CancellationToken cancellationToken = default)
    {
        var user = await context.Users.FindAsync([userPasswordDto.UserId], cancellationToken)
            ?? throw new NotFoundException(nameof(User), $"{userPasswordDto.UserId}");

        user.Password = userPasswordDto.Password;
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await context.Users.FindAsync([userId], cancellationToken)
            ?? throw new NotFoundException(nameof(User), $"{userId}");

        context.Users.Remove(user);
        await context.SaveChangesAsync(cancellationToken);
    }
}
