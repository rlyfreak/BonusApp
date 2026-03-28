using BonusApp.Models;
using BonusApp.Services;

namespace BonusApp.Views;

public partial class CardsPage : ContentPage
{
    private readonly CardService _cardService;

    public CardsPage()
    {
        InitializeComponent();
        _cardService = CardService.Instance;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CardsCollectionView.ItemsSource = _cardService.GetCards();
    }

    private async void AddCardButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(AddCardPage));
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