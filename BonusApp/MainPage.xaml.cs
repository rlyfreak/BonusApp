using BonusApp.Views;

namespace BonusApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OpenCardsButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(CardsPage));
    }
}