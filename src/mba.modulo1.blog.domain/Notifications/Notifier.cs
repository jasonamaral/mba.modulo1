using MBA.Modulo1.Blog.Domain.Interfaces;

namespace MBA.Modulo1.Blog.Domain.Notifications;

public class Notifier : INotifier
{
    private List<Notification> _notifications;

    public Notifier()
    {
        _notifications = [];
    }

    public void Handle(Notification notification) => _notifications.Add(notification);

    public List<Notification> GetNotifications() => _notifications;

    public bool HasNotification() => _notifications.Any();
}