using BonusApp.ViewModels;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;

namespace BonusApp.Views.Popups;

public partial class CardDetailsPopup : Popup
{
    private readonly CardDetailsViewModel _viewModel;
    private bool _openedAnimationPlayed;

    public CardDetailsPopup(int cardId)
    {
        InitializeComponent();

        _viewModel = new CardDetailsViewModel();
        BindingContext = _viewModel;
        _viewModel.LoadCard(cardId);

        Opened += CardDetailsPopup_Opened;
    }

    private async void CardDetailsPopup_Opened(object? sender, EventArgs e)
    {
        if (_openedAnimationPlayed)
            return;

        _openedAnimationPlayed = true;
        SheetContainer.TranslationY = 900;
        await SheetContainer.TranslateToAsync(0, 0, 220, Easing.CubicOut);
    }

    private async Task CloseSheetAsync()
    {
        await SheetContainer.TranslateToAsync(0, 900, 180, Easing.CubicIn);
        await Shell.Current.ClosePopupAsync();
    }

    private async void CloseButton_Clicked(object sender, EventArgs e)
    {
        await CloseSheetAsync();
    }

    private async void HistoryButton_Clicked(object sender, EventArgs e)
    {
        if (_viewModel.CurrentCard == null)
            return;

        await CloseSheetAsync();
        await Shell.Current.GoToAsync($"{nameof(TransactionHistoryPage)}?cardId={_viewModel.CurrentCard.Id}");
    }

    private async void ConditionsButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.DisplayAlert(
            "Условия",
            "В прототипе этот раздел показывает условия использования бонусной карты.",
            "OK");
    }

    private async void ContactsButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.DisplayAlert(
            "Контакты",
            "В прототипе этот раздел показывает контактные данные заведения.",
            "OK");
    }

    private async void DeleteCardButton_Clicked(object sender, EventArgs e)
    {
        if (_viewModel.CurrentCard == null)
            return;

        bool confirm = await Shell.Current.DisplayAlert(
            "Удаление карты",
            $"Удалить карту заведения {_viewModel.CurrentCard.CafeName}?",
            "Да",
            "Нет");

        if (!confirm)
            return;

        bool deleted = _viewModel.DeleteCurrentCard();

        if (deleted)
        {
            await Shell.Current.DisplayAlert("Готово", "Карта удалена.", "OK");
            await CloseSheetAsync();
        }
        else
        {
            await Shell.Current.DisplayAlert("Ошибка", "Не удалось удалить карту.", "OK");
        }
    }
}