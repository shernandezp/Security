namespace Security.Application.Users.Queries.GetUsersByAccount;

public sealed class GetUsersByAccountQueryValidator : AbstractValidator<GetUsersByAccountQuery>
{
    public GetUsersByAccountQueryValidator()
    {
        RuleFor(x => x.AccountId)
            .NotEmpty().WithMessage("Account Id is required.");
    }
}
