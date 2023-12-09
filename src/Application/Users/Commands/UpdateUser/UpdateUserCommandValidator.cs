using Common.Domain.Constants;

namespace Security.Application.Users.Commands.UpdateUser;
public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(v => v.User.Username)
            .MaximumLength(ColumnMetadata.DefaultUserNameLength)
            .NotEmpty();

        RuleFor(v => v.User.Email)
            .MaximumLength(ColumnMetadata.DefaultEmailLength)
            .NotEmpty();
    }
}
