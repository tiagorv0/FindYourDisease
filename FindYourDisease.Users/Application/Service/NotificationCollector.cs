namespace FindYourDisease.Users.Application.Service;

public class NotificationCollector : INotificationCollector
{
    private readonly List<string> _notifications;

    public NotificationCollector()
    {
        _notifications = new List<string>();
    }

    public void AddNotification(string notification)
    {
        _notifications.Add(notification);
    }

    public void AddNotifications(IEnumerable<string> notifications)
    {
        _notifications.AddRange(notifications);
    }

    public IReadOnlyList<string> GetNotifications()
    {
        return _notifications.AsReadOnly();
    }

    public bool HasNotifications()
    {
        return _notifications.Count > 0;
    }
}
