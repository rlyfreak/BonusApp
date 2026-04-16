using System.Collections.ObjectModel;
using System.Windows.Input;
using BonusApp.Models;
using BonusApp.Services;

namespace BonusApp.ViewModels;

public class CatalogViewModel : BaseViewModel
{
    private readonly CafeService _cafeService;
    private readonly CardService _cardService;

    private List<CatalogCardItem> _allItems = new();
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

    private CatalogCardItem? _selectedItem;
    public CatalogCardItem? SelectedItem
    {
        get => _selectedItem;
        set => SetProperty(ref _selectedItem, value);
    }

    private bool _isAddSheetOpen;
    public bool IsAddSheetOpen
    {
        get => _isAddSheetOpen;
        set => SetProperty(ref _isAddSheetOpen, value);
    }

    private bool _isAgreementAccepted;
    public bool IsAgreementAccepted
    {
        get => _isAgreementAccepted;
        set
        {
            if (SetProperty(ref _isAgreementAccepted, value) && value)
            {
                ShowAgreementError = false;
            }
        }
    }

    private bool _showAgreementError;
    public bool ShowAgreementError
    {
        get => _showAgreementError;
        set => SetProperty(ref _showAgreementError, value);
    }

    public ObservableCollection<CatalogCardItem> Items { get; } = new();

    public ICommand OpenAddCardSheetCommand { get; }
    public ICommand CloseAddCardSheetCommand { get; }
    public ICommand AddCardCommand { get; }

    public CatalogViewModel()
    {
        _cafeService = CafeService.Instance;
        _cardService = CardService.Instance;

        OpenAddCardSheetCommand = new Command<CatalogCardItem>(item =>
        {
            if (item == null)
                return;

            SelectedItem = item;
            IsAgreementAccepted = false;
            ShowAgreementError = false;
            IsAddSheetOpen = true;
        });

        CloseAddCardSheetCommand = new Command(() =>
        {
            IsAddSheetOpen = false;
            IsAgreementAccepted = false;
            ShowAgreementError = false;
            SelectedItem = null;
        });

        AddCardCommand = new Command(() =>
        {
            if (SelectedItem == null)
                return;

            if (!IsAgreementAccepted)
            {
                ShowAgreementError = true;
                return;
            }

            var created = _cardService.AddCard(SelectedItem.Name);

            if (created != null)
            {
                LoadCatalog();
                IsAddSheetOpen = false;
                IsAgreementAccepted = false;
                ShowAgreementError = false;
                SelectedItem = null;
            }
        });
    }

    public void LoadCatalog()
    {
        _allItems = _cafeService.GetCafes()
            .Where(cafe => !_cardService.HasCardForCafe(cafe.Name))
            .Select(cafe => new CatalogCardItem
            {
                Id = cafe.ID,
                Name = cafe.Name
            })
            .ToList();

        ApplyFilter();
    }

    private void ApplyFilter()
    {
        IEnumerable<CatalogCardItem> filtered = _allItems;

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            string query = SearchText.Trim();
            filtered = filtered.Where(x => x.Name.Contains(query, StringComparison.CurrentCultureIgnoreCase));
        }

        Items.Clear();

        foreach (var item in filtered)
        {
            Items.Add(item);
        }

        HasItems = Items.Count > 0;
        IsEmpty = Items.Count == 0;
    }
}
