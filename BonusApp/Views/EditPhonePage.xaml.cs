using BonusApp.ViewModels;

namespace BonusApp.Views;

public partial class EditPhonePage : ContentPage
{
    private readonly EditPhoneViewModel _viewModel;

    public EditPhonePage()
    {
        InitializeComponent();

        _viewModel = new EditPhoneViewModel();
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadPhone();
    }

    private async void BackChevron_Tapped(object sender, TappedEventArgs e) 
    { 
        await Shell.Current.GoToAsync("..");
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        bool saved = _viewModel.SavePhone();

        if (!saved)
        {
            await DisplayAlertAsync("Ошибка", "Введите номер телефона.", "OK");
            return;
        }

        await Shell.Current.GoToAsync("..");
    }
}