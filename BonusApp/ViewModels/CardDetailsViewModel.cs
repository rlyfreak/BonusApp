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

    private string _cardNumberText = "Номер карты:";
    public string CardNumberText
    {
        get => _cardNumberText;
        set => SetProperty(ref _cardNumberText, value);
    }

    private string _bonusBalanceText = "Баланс бонусов:";
    public string BonusBalanceText
    {
        get => _bonusBalanceText;
        set => SetProperty(ref _bonusBalanceText, value);
    }

    private string _qrCodeValueText = "QR-код:";
    public string QrCodeValueText
    {
        get => _qrCodeValueText;
        set => SetProperty(ref _qrCodeValueText, value);
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
        CardNumberText = $"Номер карты: {CurrentCard.CardNumber}";
        BonusBalanceText = $"Баланс бонусов: {CurrentCard.BonusBalance}";
        QrCodeValueText = $"QR-код: {CurrentCard.QrCodeValue}";
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