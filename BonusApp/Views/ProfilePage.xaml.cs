using BonusApp.ViewModels;

namespace BonusApp.Views;

public partial class ProfilePage : ContentPage
{
    private readonly ProfileViewModel _viewModel;

    public ProfilePage()
    {
        InitializeComponent();

        _viewModel = new ProfileViewModel();
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadProfile();
    }

    private async void PhoneCardTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(EditPhonePage));
    }

    private async void EmailCardTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(EditEmailPage));
    }

    private async void EditProfileButton_Clicked(object sender, EventArgs e)
    {
        await DisplayAlertAsync(
            "Редактирование",
            "Следующим шагом здесь откроется EditPersonalDataPage.",
            "OK");
    }

    private async void LogoutButton_Clicked(object sender, EventArgs e)
    {
        await DisplayAlertAsync(
            "Выход",
            "В прототипе это действие пока показано как кнопка без реальной деавторизации.",
            "OK");
    }
}