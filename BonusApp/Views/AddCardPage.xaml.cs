using BonusApp.ViewModels;

namespace BonusApp.Views;

public partial class AddCardPage : ContentPage
{
    private readonly AddCardViewModel _viewModel;

    public AddCardPage()
    {
        InitializeComponent();

        _viewModel = new AddCardViewModel();
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadCafes();
    }

    private async void CreateCardButton_Clicked(object sender, EventArgs e)
    {
        if (_viewModel.SelectedCafe == null)
        {
            await DisplayAlert("Ошибка", "Сначала выберите заведение.", "OK");
            return;
        }

        if (_viewModel.HasCardForSelectedCafe())
        {
            await DisplayAlert(
                "Карта уже существует",
                $"У вас уже есть карта заведения {_viewModel.SelectedCafe.Name}.",
                "OK");

            _viewModel.ClearSelection();
            return;
        }

        bool confirm = await DisplayAlert(
            "Создание карты",
            $"Оформить бонусную карту для заведения {_viewModel.SelectedCafe.Name}?",
            "Да",
            "Нет");

        if (!confirm)
            return;

        bool created = _viewModel.CreateCardForSelectedCafe();

        if (created)
        {
            await DisplayAlert(
                "Готово",
                $"Карта для заведения {_viewModel.SelectedCafe?.Name} успешно создана.",
                "OK");

            _viewModel.ClearSelection();
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await DisplayAlert("Ошибка", "Не удалось создать карту.", "OK");
        }
    }
}