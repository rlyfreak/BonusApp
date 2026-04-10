namespace BonusApp.Models
{
    public class NotificationItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public bool IsPositive { get; set; }

        public string IconSource { get; set; } = string.Empty;
        public string TimeText => Date.ToString("HH:mm");
        public string DateGroupTitle
        {
            get
            {
                var today = DateTime.Today;
                var date = Date.Date;
                if (date == today)
                    return "Сегодня";
                if (date == today.AddDays(-1))
                    return "Вчера";
                return date.ToString("dd.MM.yyyy");
            }
        }
    }
}
