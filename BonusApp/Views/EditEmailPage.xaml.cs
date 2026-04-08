using BonusApp.ViewModels;

namespace BonusApp.Views;

public partial class EditEmailPage : ContentPage
{
    private readonly EditEmailViewModel _viewModel;

    public EditEmailPage()
    {
        InitializeComponent();

        _viewModel = new EditEmailViewModel();
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadEmail();
    }

    private async void BackChevron_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        bool saved = _viewModel.SaveEmail();

        if (!saved)
        {
            await DisplayAlertAsync("Ошибка", "Введите электронную почту.", "OK");
            return;
        }

        await Shell.Current.GoToAsync("..");
    }
}