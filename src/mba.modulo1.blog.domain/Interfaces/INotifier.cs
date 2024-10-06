using MBA.Modulo1.Blog.Domain.Notifications;

namespace MBA.Modulo1.Blog.Domain.Interfaces;

public interface INotifier
{
    bool HasNotification();

    List<Notification> GetNotifications();

    void Handle(Notification notification);
}