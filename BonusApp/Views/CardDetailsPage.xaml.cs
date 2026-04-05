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

    private async void BackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private async void ConditionsButton_Clicked(object sender, EventArgs e)
    {
        await DisplayAlertAsync(
            "Условия",
            "В прототипе этот раздел показывает условия использования бонусной карты. Полная детализация будет вынесена в дальнейшее развитие проекта.",
            "OK");
    }

    private async void ContactsButton_Clicked(object sender, EventArgs e)
    {
        await DisplayAlertAsync(
            "Контакты",
            "В прототипе этот раздел показывает контактные данные заведения. Реальные данные и интеграции будут добавлены на следующем этапе.",
            "OK");
    }

    private async void DeleteCardButton_Clicked(object sender, EventArgs e)
    {
        if (_viewModel.CurrentCard == null)
            return;

        bool confirm = await DisplayAlertAsync(
            "Удаление карты",
            $"Удалить карту заведения {_viewModel.CurrentCard.CafeName}?",
            "Да",
            "Нет");

        if (!confirm)
            return;

        bool deleted = _viewModel.DeleteCurrentCard();

        if (deleted)
        {
            await DisplayAlertAsync("Готово", "Карта удалена.", "OK");
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            await DisplayAlertAsync("Ошибка", "Не удалось удалить карту.", "OK");
        }
    }
}