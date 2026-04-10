using System.Collections.ObjectModel;

namespace BonusApp.Models
{
    public class NotificationGroup : ObservableCollection<NotificationItem>
    {
        public string Title { get; }
        public NotificationGroup(string title, IEnumerable<NotificationItem> items) : base(items)
        {
            Title = title;
        }
    }
}
