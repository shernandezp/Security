namespace Security.Domain.Models;

public record struct UserVm (
    Guid UserId,
    string Username,
    string Password,
    string Email,
    string FirstName,
    string? SecondName,
    string LastName,
    string? SeconSurname,
    DateTime? DOB,
    IEnumerable<RoleVm> Roles,
    IEnumerable<ProfileVm> Profiles);
