using System;
using System.Collections.Generic;
using System.Text;
using BonusApp.Models;

namespace BonusApp.Services
{
    public class NotificationService
    {
        private static readonly NotificationService _instance = new NotificationService();
        public static NotificationService Instance => _instance;
        private readonly List<NotificationItem> _notifications = new();
        private int _nextId = 1;
        private NotificationService() { }
        public List<NotificationItem> GetNotifications()
        {
            return _notifications
                .OrderByDescending(x => x.CreatedAt)
                .ToList();
        }
        public void AddNotification(string title, string message)
        {
            var notification = new NotificationItem
            {
                Id = _nextId++,
                Title = title,
                Message = message,
                CreatedAt = DateTime.Now
            };
            _notifications.Add(notification);
            _nextId++;
        }
    }
}
