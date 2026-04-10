using BonusApp.ViewModels;

namespace BonusApp.Views;

public partial class NotificationsPage : ContentPage
{
    private readonly NotificationsViewModel _viewModel;

    public NotificationsPage()
    {
        InitializeComponent();

        _viewModel = new NotificationsViewModel();
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadNotifications();
    }
    private async void BackChevron_Tapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}