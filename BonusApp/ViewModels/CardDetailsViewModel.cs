using System.Windows.Input;
using BonusApp.Models;
using BonusApp.Services;
using BonusApp.Views;

namespace BonusApp.ViewModels;

public class CardDetailsViewModel : BaseViewModel
{
    private readonly CardService _cardService;

    private LoyaltyCard? _currentCard;
    public LoyaltyCard? CurrentCard
    {
        get => _currentCard;
        set => SetProperty(ref _currentCard, value);
    }

    private string _cafeName = "Название заведения";
    public string CafeName
    {
        get => _cafeName;
        set => SetProperty(ref _cafeName, value);
    }

    private string _bonusValue = "0";
    public string BonusValue
    {
        get => _bonusValue;
        set => SetProperty(ref _bonusValue, value);
    }

    private string _cardNumberValue = "-";
    public string CardNumberValue
    {
        get => _cardNumberValue;
        set => SetProperty(ref _cardNumberValue, value);
    }

    private string _qrCodeValue = "-";
    public string QrCodeValue
    {
        get => _qrCodeValue;
        set => SetProperty(ref _qrCodeValue, value);
    }

    public ICommand OpenHistoryCommand { get; }

    public CardDetailsViewModel()
    {
        _cardService = CardService.Instance;

        OpenHistoryCommand = new Command(async () =>
        {
            if (CurrentCard == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(TransactionHistoryPage)}?cardId={CurrentCard.Id}");
        });
    }

    public void LoadCard(int cardId)
    {
        CurrentCard = _cardService.GetCardById(cardId);

        if (CurrentCard == null)
            return;

        CafeName = CurrentCard.CafeName;
        BonusValue = CurrentCard.BonusBalance.ToString("0");
        CardNumberValue = CurrentCard.CardNumber;
        QrCodeValue = CurrentCard.QrCodeValue;
    }

    public bool DeleteCurrentCard()
    {
        if (CurrentCard == null)
            return false;

        bool deleted = _cardService.DeleteCard(CurrentCard.Id);

        if (deleted)
        {
            CurrentCard = null;
        }

        return deleted;
    }
}