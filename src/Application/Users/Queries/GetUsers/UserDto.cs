using Security.Domain.Entities;

namespace Security.Application.Users.Queries.GetUsers;
public sealed class UserDto
{
    public Guid UserId { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public string? SecondName { get; init; }
    public required string LastName { get; init; }
    public string? SeconSurname { get; init; }
    public DateTime? DOB { get; init; }

    public IReadOnlyCollection<RoleDto> Roles { get; init; } = Array.Empty<RoleDto>();
    public IReadOnlyCollection<ProfileDto> Profiles { get; init; } = Array.Empty<ProfileDto>();

    private class Mapping : AutoMapper.Profile
    {
        public Mapping()
        {
            CreateMap<User, UserDto>();
        }
    }
}
