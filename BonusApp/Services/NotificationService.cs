using BonusApp.Models;

namespace BonusApp.Services
{
    public class NotificationService
    {
        private static readonly NotificationService _instance = new NotificationService();
        public static NotificationService Instance => _instance;

        private readonly List<NotificationItem> _notifications = new();
        private int _nextId = 1;

        private NotificationService()
        {
            SeedPlaceholderNotifications();
        }

        public List<NotificationItem> GetNotifications()
        {
            return _notifications
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
            var notification = new NotificationItem
            {
                Id = _nextId++,
                Title = title,
                Message = message,
                Date = date,
                IconSource = iconSource
            };

            _notifications.Add(notification);
        }

        private string ResolveIconSource(string title, string message)
        {
            string text = $"{title} {message}".ToLower();

            if (text.Contains("добав") || text.Contains("создан") || text.Contains("создание"))
                return "notification_add.svg";

            if (text.Contains("удал"))
                return "notification_remove.svg";

            return "notification_add.svg";
        }

        private void SeedPlaceholderNotifications()
        {
            AddNotification(
                "Карта удалена",
                "Знак удалена из приложения",
                "notification_remove.svg",
                DateTime.Today.AddDays(-1).AddHours(19).AddMinutes(22));

            AddNotification(
                "Карта добавлена",
                "Розмарин добавлена в приложение",
                "notification_add.svg",
                DateTime.Today.AddDays(-7).AddHours(14).AddMinutes(10));
        }
    }
}