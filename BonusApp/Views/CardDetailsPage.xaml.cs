using System;
using System.Collections.Generic;
using System.Text;
using BonusApp.Models;
using BonusApp.Services;
using BonusApp.Views;

namespace BonusApp.Views;

[QueryProperty(nameof(CardId), "cardId")]
public partial class CardDetailsPage : ContentPage
{
    private readonly CardService _cardService;
    private LoyaltyCard? _currentCard;

    private string _cardId = string.Empty;
    public string CardId
    {
        get => _cardId;
        set
        {
            _cardId = value;
            LoadCard();
        }
    }

    public CardDetailsPage()
    {
        InitializeComponent();
        _cardService = new CardService();
    }

    private void LoadCard()
    {
        if (string.IsNullOrWhiteSpace(_cardId))
            return;

        if (!int.TryParse(_cardId, out int id))
            return;

        _currentCard = _cardService.GetCardById(id);
        if (_currentCard == null)
            return;

        CafeNameLabel.Text = _currentCard.CafeName;
        CardNumberLabel.Text = $"Номер карты: {_currentCard.CardNumber}";
        BonusBalanceLabel.Text = $"Баланс бонусов: {_currentCard.BonusBalance}";
        QrCodeValueLabel.Text = $"QR-код: {_currentCard.QrCodeValue}";
    }

    private async void OpenHistoryButton_Clicked(object sender, EventArgs e)
    {
        if(_currentCard == null)
            return;
        await Shell.Current.GoToAsync($"{nameof(TransactionHistoryPage)}?cardId={_currentCard.Id}");
    }

    private async void DeleteCardButton_Clicked(object sender, EventArgs e)
    {
        if (_currentCard == null)
            return;

        bool result = await DisplayAlert(
            "Удаление карты",
            $"Удалить карту заведения {_currentCard.CafeName}?",
            "Да",
            "Нет");

        if (result)
        {
            await DisplayAlert("В разработке", "сделаю потом.", "OK");
        }
    }
}