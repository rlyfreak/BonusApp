using BonusApp.ViewModels;

namespace BonusApp.Views;

[QueryProperty(nameof(CardId), "cardId")]
public partial class CardDetailsPage : ContentPage
{
    private readonly CardDetailsViewModel _viewModel;
    private string _cardId = string.Empty;

    public string CardId
    {
        get => _cardId;
        set
        {
            _cardId = value;

            if (int.TryParse(value, out int id))
            {
                _viewModel.LoadCard(id);
            }
        }
    }

    public CardDetailsPage()
    {
        InitializeComponent();

        _viewModel = new CardDetailsViewModel();
        BindingContext = _viewModel;
    }

    private async void DeleteCardButton_Clicked(object sender, EventArgs e)
    {
        if (_viewModel.CurrentCard == null)
            return;

        bool confirm = await DisplayAlert(
            "Удаление карты",
            $"Удалить карту заведения {_viewModel.CurrentCard.CafeName}?",
            "Да",
            "Нет");

        if (!confirm)
            return;

        bool deleted = _viewModel.DeleteCurrentCard();

        if (deleted)
        {
            await DisplayAlert("Готово", "Карта удалена.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await DisplayAlert("Ошибка", "Не удалось удалить карту.", "OK");
        }
    }
}