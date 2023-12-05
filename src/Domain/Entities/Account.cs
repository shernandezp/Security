namespace Security.Domain.Entities;

public class Account : BaseAuditableEntity
{
    public Guid AccountId { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public AccountType Type { get; set; }

    public bool Active { get; set; }

    public IEnumerable<User>? Users { get; set; }


    private bool _done;
    public bool Done
    {
        get => _done;
        set
        {
            if (value && !_done)
            {
                AddDomainEvent(new AccountCompletedEvent(this));
            }

            _done = value;
        }
    }
}
