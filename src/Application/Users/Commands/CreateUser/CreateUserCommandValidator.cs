using Common.Domain.Constants;

namespace Security.Application.Users.Commands.CreateUser;
public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(v => v.User.Username)
            .MaximumLength(ColumnMetadata.DefaultUserNameLength)
            .NotEmpty();

        RuleFor(v => v.User.Password)
            .MinimumLength(8)
            .MinimumLength(ColumnMetadata.DefaultPasswordLength)
            .NotEmpty();

        RuleFor(v => v.User.Email)
            .MaximumLength(ColumnMetadata.DefaultEmailLength)
            .NotEmpty();
    }
}
