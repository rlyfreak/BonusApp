using System.Collections.ObjectModel;
using System.Windows.Input;
using BonusApp.Models;
using BonusApp.Services;

namespace BonusApp.ViewModels;

public class HistoryViewModel : BaseViewModel
{
    private readonly TransactionService _transactionService;

    private List<HistoryRecord> _allHistory = new();

    private string _searchText = string.Empty;
    public string SearchText
    {
        get => _searchText;
        set
        {
            if (SetProperty(ref _searchText, value))
            {
                ApplyFilter();
            }
        }
    }

    private string _selectedFilter = "All";
    public string SelectedFilter
    {
        get => _selectedFilter;
        set
        {
            if (SetProperty(ref _selectedFilter, value))
            {
                UpdateFilterStyles();
                ApplyFilter();
            }
        }
    }

    private bool _hasHistory;
    public bool HasHistory
    {
        get => _hasHistory;
        set => SetProperty(ref _hasHistory, value);
    }

    private bool _isHistoryEmpty;
    public bool IsHistoryEmpty
    {
        get => _isHistoryEmpty;
        set => SetProperty(ref _isHistoryEmpty, value);
    }

    private bool _isTransactionSheetOpen;
    public bool IsTransactionSheetOpen
    {
        get => _isTransactionSheetOpen;
        set => SetProperty(ref _isTransactionSheetOpen, value);
    }

    private HistoryRecord? _selectedRecord;
    public HistoryRecord? SelectedRecord
    {
        get => _selectedRecord;
        set => SetProperty(ref _selectedRecord, value);
    }

    public ObservableCollection<HistoryGroup> Groups { get; } = new();

    public ICommand ShowAllCommand { get; }
    public ICommand ShowAccrualCommand { get; }
    public ICommand ShowWriteOffCommand { get; }
    public ICommand OpenTransactionDetailsCommand { get; }
    public ICommand CloseTransactionSheetCommand { get; }

    private Color _allBackground = Color.FromArgb("#EDEFF5");
    public Color AllBackground
    {
        get => _allBackground;
        set => SetProperty(ref _allBackground, value);
    }

    private Color _allTextColor = Color.FromArgb("#2F3648");
    public Color AllTextColor
    {
        get => _allTextColor;
        set => SetProperty(ref _allTextColor, value);
    }

    private Color _accrualBackground = Color.FromArgb("#434C62");
    public Color AccrualBackground
    {
        get => _accrualBackground;
        set => SetProperty(ref _accrualBackground, value);
    }

    private Color _accrualTextColor = Color.FromArgb("#EDEFF5");
    public Color AccrualTextColor
    {
        get => _accrualTextColor;
        set => SetProperty(ref _accrualTextColor, value);
    }

    private Color _writeOffBackground = Color.FromArgb("#434C62");
    public Color WriteOffBackground
    {
        get => _writeOffBackground;
        set => SetProperty(ref _writeOffBackground, value);
    }

    private Color _writeOffTextColor = Color.FromArgb("#EDEFF5");
    public Color WriteOffTextColor
    {
        get => _writeOffTextColor;
        set => SetProperty(ref _writeOffTextColor, value);
    }

    public HistoryViewModel()
    {
        _transactionService = TransactionService.Instance;

        ShowAllCommand = new Command(() => SelectedFilter = "All");
        ShowAccrualCommand = new Command(() => SelectedFilter = "Accrual");
        ShowWriteOffCommand = new Command(() => SelectedFilter = "WriteOff");

        OpenTransactionDetailsCommand = new Command<HistoryRecord>(record =>
        {
            if (record == null)
                return;

            SelectedRecord = record;
            IsTransactionSheetOpen = true;
        });

        CloseTransactionSheetCommand = new Command(() =>
        {
            IsTransactionSheetOpen = false;
        });
    }

    public void LoadHistory()
    {
        _allHistory = _transactionService
            .GetAllTransactions()
            .Select(t => new HistoryRecord
            {
                Id = t.Id,
                CardId = t.CardId,
                CafeName = t.CafeName,
                Type = t.Type,
                BonusAmount = t.BonusAmount,
                Date = t.Date,
                Description = t.Description,
                BalanceBefore = t.BalanceBefore,
                BalanceAfter = t.BalanceAfter,
                VenueAddress = t.VenueAddress,
                OperationCode = t.OperationCode,
                Comment = t.Comment
            })
            .OrderByDescending(x => x.Date)
            .ToList();

        ApplyFilter();
    }

    private void ApplyFilter()
    {
        IEnumerable<HistoryRecord> filtered = _allHistory;

        if (SelectedFilter == "Accrual")
            filtered = filtered.Where(x => x.Type == "Начисление");
        else if (SelectedFilter == "WriteOff")
            filtered = filtered.Where(x => x.Type == "Списание");

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            string query = SearchText.Trim();

            filtered = filtered.Where(x =>
                x.CafeName.Contains(query, StringComparison.CurrentCultureIgnoreCase) ||
                x.Description.Contains(query, StringComparison.CurrentCultureIgnoreCase));
        }

        var grouped = filtered
            .GroupBy(x => x.DateGroupTitle)
            .Select(g => new HistoryGroup(g.Key, g.OrderByDescending(x => x.Date)))
            .ToList();

        Groups.Clear();

        foreach (var group in grouped)
        {
            Groups.Add(group);
        }

        HasHistory = Groups.Count > 0;
        IsHistoryEmpty = Groups.Count == 0;
    }

    private void UpdateFilterStyles()
    {
        AllBackground = SelectedFilter == "All" ? Color.FromArgb("#EDEFF5") : Color.FromArgb("#434C62");
        AllTextColor = SelectedFilter == "All" ? Color.FromArgb("#2F3648") : Color.FromArgb("#EDEFF5");

        AccrualBackground = SelectedFilter == "Accrual" ? Color.FromArgb("#EDEFF5") : Color.FromArgb("#434C62");
        AccrualTextColor = SelectedFilter == "Accrual" ? Color.FromArgb("#2F3648") : Color.FromArgb("#EDEFF5");

        WriteOffBackground = SelectedFilter == "WriteOff" ? Color.FromArgb("#EDEFF5") : Color.FromArgb("#434C62");
        WriteOffTextColor = SelectedFilter == "WriteOff" ? Color.FromArgb("#2F3648") : Color.FromArgb("#EDEFF5");
    }
}
