namespace Security.Application.Users.Events;
public sealed class UserDeleted
{
    public class Notification(Guid id) : INotification 
    {
        public Guid Id { get; } = id;
    }
}
