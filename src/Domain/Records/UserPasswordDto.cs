namespace Security.Domain.Records;

public record struct UserPasswordDto(
    Guid UserId,
    string Password,
    string Key);
