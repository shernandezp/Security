namespace Security.Domain.Events;

public class AccountCompletedEvent(Account item) : BaseEvent
{
    public Account Item { get; } = item;
}
