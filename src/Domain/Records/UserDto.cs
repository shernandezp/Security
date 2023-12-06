namespace Security.Domain.Records;

public record struct UserDto (
    Guid UserId,
    string Username,
    string Password,
    string Email,
    string FirstName,
    string? SecondName,
    string LastName,
    string? SeconSurname,
    DateTime? DOB);
