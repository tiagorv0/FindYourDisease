namespace FindYourDisease.Comments.Application.Service;

public interface INotificationCollector
{
    void AddNotification(string notification);
    void AddNotifications(IEnumerable<string> notifications);
    IReadOnlyList<string> GetNotifications();
    bool HasNotifications();
}
