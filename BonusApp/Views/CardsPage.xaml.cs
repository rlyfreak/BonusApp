using BonusApp.Models;
using BonusApp.ViewModels;

namespace BonusApp.Views;

public partial class CardsPage : ContentPage
{
    private readonly CardsViewModel _viewModel;

    public CardsPage()
    {
        InitializeComponent();
        _viewModel = new CardsViewModel();
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadCards();
    }

    private async void CardsCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is LoyaltyCard selectedCard)
        {
            CardsCollectionView.SelectedItem = null;
            await _viewModel.OpenCardDetailsAsync(selectedCard);
        }
    }
}