namespace Security.Application.Users.Queries.GetUser;

public sealed class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("User Id is required.");
    }
}
