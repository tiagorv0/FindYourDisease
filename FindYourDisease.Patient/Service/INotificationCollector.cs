namespace FindYourDisease.Patient.Service;

public interface INotificationCollector
{
    void AddNotification(string notification);
    void AddNotifications(IEnumerable<string> notifications);
    IReadOnlyList<string> GetNotifications();
    bool HasNotifications();
}
