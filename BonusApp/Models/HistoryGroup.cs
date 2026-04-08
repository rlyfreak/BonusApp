using System.Collections.ObjectModel;

namespace BonusApp.Models
{
    public class HistoryGroup : ObservableCollection<HistoryRecord>
    {
        public string Title { get; }
        public HistoryGroup(string title, IEnumerable<HistoryRecord> items) 
            : base(items)
        {
            Title = title;
        }
    }
}
