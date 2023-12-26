namespace Security.Application.Users.Events;
public sealed class UserCreated
{
    public class Notification(Guid id) : INotification 
    {
        public Guid Id { get; } = id;

        /*public class EventHandler(ISender sender) : INotificationHandler<Notification> 
        {
            public async Task Handle(Notification notification, CancellationToken cancellationToken) 
            { 
                await sender.Send(new ....)
            }
        }*/
    }
}
