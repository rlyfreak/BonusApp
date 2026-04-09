using BonusApp.ViewModels;

namespace BonusApp.Views;

public partial class EditPersonalDataPage : ContentPage
{
    private readonly EditPersonalDataViewModel _viewModel;

    public EditPersonalDataPage()
    {
        InitializeComponent();

        _viewModel = new EditPersonalDataViewModel();
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadData();
    }

    private async void BackChevron_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        bool saved = _viewModel.Save();

        if (!saved)
        {
            await DisplayAlertAsync(
                "Ошибка",
                "Проверьте заполнение полей. Дата рождения должна быть в формате дд.мм.гггг.",
                "OK");
            return;
        }

        await Shell.Current.GoToAsync("..");
    }
}