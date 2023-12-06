using Common.Domain.Enums;
using Common.Infrastructure;

namespace Security.Infrastructure.Entities;

public class Account : BaseAuditableEntity
{
    public Guid AccountId { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public AccountType Type { get; set; }

    public bool Active { get; set; }

    public IEnumerable<User>? Users { get; set; }

}
