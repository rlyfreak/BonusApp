using System.ComponentModel;
using BonusApp.Services;
using BonusApp.ViewModels;

namespace BonusApp.Views;

public partial class CardsPage : ContentPage
{
    private readonly CardsViewModel _viewModel;
    private bool _isAnimating;

    public CardsPage()
    {
        InitializeComponent();

        _viewModel = new CardsViewModel();
        BindingContext = _viewModel;
        _viewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadCards();
    }

    private async void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(CardsViewModel.IsCardSheetOpen))
            return;

        if (_isAnimating)
            return;

        _isAnimating = true;

        if (_viewModel.IsCardSheetOpen)
        {
            CardSheetOverlay.IsVisible = true;
            SheetContainer.TranslationY = 1200;
            await SheetContainer.TranslateToAsync(0, 0, 220, Easing.CubicOut);
        }
        else
        {
            await SheetContainer.TranslateToAsync(0, 1200, 180, Easing.CubicIn);
            CardSheetOverlay.IsVisible = false;
        }

        _isAnimating = false;
    }

    private void CloseSheetButton_Clicked(object sender, EventArgs e)
    {
        _viewModel.CloseCardSheetCommand.Execute(null);
    }

    private async void ConditionsButton_Clicked(object sender, EventArgs e)
    {
        await DisplayAlertAsync(
            "Условия",
            "В прототипе этот раздел показывает условия использования бонусной карты.",
            "OK");
    }

    private async void ContactsButton_Clicked(object sender, EventArgs e)
    {
        await DisplayAlertAsync(
            "Контакты",
            "В прототипе этот раздел показывает контактные данные заведения.",
            "OK");
    }

    private async void DeleteCardButton_Clicked(object sender, EventArgs e)
    {
        if (_viewModel.SelectedCard == null)
            return;

        bool confirm = await DisplayAlertAsync(
            "Удаление карты",
            $"Удалить карту заведения {_viewModel.SelectedCard.CafeName}?",
            "Да",
            "Нет");

        if (!confirm)
            return;

        bool deleted = CardService.Instance.DeleteCard(_viewModel.SelectedCard.Id);

        if (deleted)
        {
            await DisplayAlertAsync("Готово", "Карта удалена.", "OK");
            _viewModel.CloseCardSheetCommand.Execute(null);
            _viewModel.LoadCards();
        }
        else
        {
            await DisplayAlertAsync("Ошибка", "Не удалось удалить карту.", "OK");
        }
    }
}