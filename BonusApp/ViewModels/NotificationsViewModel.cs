using System.Collections.ObjectModel;
using BonusApp.Models;
using BonusApp.Services;

namespace BonusApp.ViewModels;

public class NotificationsViewModel : BaseViewModel
{
    private readonly NotificationService _notificationService;

    public ObservableCollection<NotificationItem> Notifications { get; } = new();

    public NotificationsViewModel()
    {
        _notificationService = NotificationService.Instance;
    }

    public void LoadNotifications()
    {
        Notifications.Clear();

        var notifications = _notificationService.GetNotifications();

        foreach (var notification in notifications)
        {
            Notifications.Add(notification);
        }
    }
}