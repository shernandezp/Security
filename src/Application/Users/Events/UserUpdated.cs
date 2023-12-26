namespace Security.Application.Users.Events;
public sealed class UserUpdated
{
    public class Notification(Guid id) : INotification 
    {
        public Guid Id { get; } = id;
    }
}
