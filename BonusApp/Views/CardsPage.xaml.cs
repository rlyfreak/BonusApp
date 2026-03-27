using BonusApp.Models;
using BonusApp.Services;

namespace BonusApp.Views;

public partial class CardsPage : ContentPage
{
    private readonly CardService _cardService;

    public CardsPage()
    {
        InitializeComponent();

        _cardService = new CardService();
        CardsCollectionView.ItemsSource = _cardService.GetCards();
    }

    private async void CardsCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is LoyaltyCard selectedCard)
        {
            CardsCollectionView.SelectedItem = null;
            await Shell.Current.GoToAsync($"{nameof(CardDetailsPage)}?cardId={selectedCard.Id}");
        }
    }
}