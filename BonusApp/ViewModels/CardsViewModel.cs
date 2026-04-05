using System.Collections.ObjectModel;
using System.Windows.Input;
using BonusApp.Models;
using BonusApp.Services;
using BonusApp.Views;

namespace BonusApp.ViewModels;

public class CardsViewModel : BaseViewModel
{
    private readonly CardService _cardService;
    private List<LoyaltyCard> _allCards = new();

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

    private bool _isCardsEmpty;
    public bool IsCardsEmpty
    {
        get => _isCardsEmpty;
        set => SetProperty(ref _isCardsEmpty, value);
    }

    private bool _hasCards;
    public bool HasCards
    {
        get => _hasCards;
        set => SetProperty(ref _hasCards, value);
    }

    private bool _isCardSheetOpen;
    public bool IsCardSheetOpen
    {
        get => _isCardSheetOpen;
        set => SetProperty(ref _isCardSheetOpen, value);
    }

    private LoyaltyCard? _selectedCard;
    public LoyaltyCard? SelectedCard
    {
        get => _selectedCard;
        set => SetProperty(ref _selectedCard, value);
    }

    public ObservableCollection<LoyaltyCard> Cards { get; } = new();

    public ICommand OpenAddCardPageCommand { get; }
    public ICommand OpenNotificationsPageCommand { get; }
    public ICommand OpenCardDetailsCommand { get; }
    public ICommand CloseCardSheetCommand { get; }

    public CardsViewModel()
    {
        _cardService = CardService.Instance;

        OpenAddCardPageCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(AddCardPage));
        });

        OpenNotificationsPageCommand = new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(NotificationsPage));
        });

        OpenCardDetailsCommand = new Command<LoyaltyCard>(selectedCard =>
        {
            if (selectedCard == null)
                return;

            SelectedCard = selectedCard;
            IsCardSheetOpen = true;
        });

        CloseCardSheetCommand = new Command(() =>
        {
            IsCardSheetOpen = false;
        });
    }

    public void LoadCards()
    {
        _allCards = _cardService.GetCards();
        ApplyFilter();
    }

    private void ApplyFilter()
    {
        IEnumerable<LoyaltyCard> filteredCards = _allCards;

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            string query = SearchText.Trim().ToLower();

            filteredCards = filteredCards.Where(card =>
                card.CafeName.ToLower().Contains(query) ||
                card.CardNumber.ToLower().Contains(query));
        }

        Cards.Clear();

        foreach (var card in filteredCards)
        {
            Cards.Add(card);
        }

        HasCards = Cards.Count > 0;
        IsCardsEmpty = Cards.Count == 0;
    }
}