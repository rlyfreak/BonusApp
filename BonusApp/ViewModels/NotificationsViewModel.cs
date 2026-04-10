using System.Collections.ObjectModel;
using BonusApp.Models;
using BonusApp.Services;

namespace BonusApp.ViewModels;

public class NotificationsViewModel : BaseViewModel
{
    private readonly NotificationService _notificationService;

    private bool _hasItems;
    public bool HasItems
    {
        get => _hasItems;
        set => SetProperty(ref _hasItems, value);
    }

    private bool _isEmpty;
    public bool IsEmpty
    {
        get => _isEmpty;
        set => SetProperty(ref _isEmpty, value);
    }

    public ObservableCollection<NotificationGroup> Groups { get; } = new();

    public NotificationsViewModel()
    {
        _notificationService = NotificationService.Instance;
    }

    public void LoadNotifications()
    {
        var notifications = _notificationService.GetNotifications();

        var grouped = notifications
            .OrderByDescending(x => x.Date)
            .GroupBy(x => x.DateGroupTitle)
            .Select(g => new NotificationGroup(g.Key, g.OrderByDescending(x => x.Date)))
            .ToList();

        Groups.Clear();

        foreach (var group in grouped)
        {
            Groups.Add(group);
        }

        HasItems = Groups.Count > 0;
        IsEmpty = Groups.Count == 0;
    }
}