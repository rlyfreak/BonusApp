using BonusApp.Models;

namespace BonusApp.Services;

public class NotificationService
{
    private static readonly NotificationService _instance = new();
    public static NotificationService Instance => _instance;

    private readonly Dictionary<int, List<NotificationItem>> _notificationsByUser = new();
    private readonly Dictionary<int, int> _nextIdsByUser = new();

    private NotificationService()
    {
    }

    public List<NotificationItem> GetNotifications()
    {
        return GetCurrentUserNotifications()
            .OrderByDescending(x => x.Date)
            .ToList();
    }

    public void AddNotification(string title, string message)
    {
        string iconSource = ResolveIconSource(title, message);
        AddNotification(title, message, iconSource, DateTime.Now);
    }

    public void AddNotification(string title, string message, string iconSource)
    {
        AddNotification(title, message, iconSource, DateTime.Now);
    }

    private void AddNotification(string title, string message, string iconSource, DateTime date)
    {
        int userId = GetCurrentUserId();
        var notifications = GetCurrentUserNotifications();

        var notification = new NotificationItem
        {
            Id = _nextIdsByUser[userId]++,
            Title = title,
            Message = message,
            Date = date,
            IconSource = iconSource
        };

        notifications.Add(notification);
    }

    private string ResolveIconSource(string title, string message)
    {
        string text = $"{title} {message}".ToLower();

        if (text.Contains("добав") || text.Contains("создан") || text.Contains("создание"))
        {
            return "notification_add.svg";
        }

        if (text.Contains("удал"))
        {
            return "notification_remove.svg";
        }

        return "notification_add.svg";
    }

    private List<NotificationItem> GetCurrentUserNotifications()
    {
        int userId = GetCurrentUserId();

        if (_notificationsByUser.TryGetValue(userId, out var notifications))
        {
            return notifications;
        }

        notifications = DemoAccountDefaults.IsCurrentSessionDemoAccount()
            ? DemoAccountDefaults.CreateNotifications()
            : [];

        _notificationsByUser[userId] = notifications;
        _nextIdsByUser[userId] = notifications.Count == 0 ? 1 : notifications.Max(x => x.Id) + 1;
        return notifications;
    }

    private static int GetCurrentUserId()
    {
        return SessionService.Instance.UserId;
    }
}
