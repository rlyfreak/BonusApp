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

    private async void ContactsSectionTapped(object sender, TappedEventArgs e)
    {
        await DisplayAlertAsync(
            "Контактные данные",
            "Следующим шагом здесь откроются отдельные экраны редактирования номера телефона и электронной почты.",
            "OK");
    }

    private async void PersonalDataSectionTapped(object sender, TappedEventArgs e)
    {
        await DisplayAlertAsync(
            "Персональные данные",
            "Следующим шагом здесь откроется экран редактирования фамилии, имени, отчества и даты рождения.",
            "OK");
    }
}