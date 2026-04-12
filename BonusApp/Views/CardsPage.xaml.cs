using System.ComponentModel;
using BonusApp.Models;
using BonusApp.Services;
using BonusApp.ViewModels;

namespace BonusApp.Views;

public partial class CardsPage : ContentPage
{
    private readonly CardsViewModel _viewModel;
    private bool _isAnimating;
    private bool _isCardTapInProgress;

    private double _headerNaturalHeight = -1;
    private bool _isHeaderHidden;
    private bool _isHeaderAnimating;
    private double _lastVerticalOffset;
    private const double ScrollDeltaThreshold = 6;
    private const double HideAfterOffset = 20;
    private bool _isSearchFocused;

    public CardsPage()
    {
        InitializeComponent();

        _viewModel = new CardsViewModel();
        BindingContext = _viewModel;
        _viewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    private const int MinCardsForHeaderCollapse = 4;

    private bool CanCollapseHeader()
    {
        return _viewModel.Cards != null && _viewModel.Cards.Count >= MinCardsForHeaderCollapse;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadCards();

        Dispatcher.Dispatch(() =>
        {
            ShowHeaderImmediate();
            _lastVerticalOffset = 0;
        });

        Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(60), () =>
        {
            try
            {
                CardsCollectionView.ScrollTo(0, position: ScrollToPosition.Start, animate: false);
            }
            catch
            {
            }

            ShowHeaderImmediate();
            _lastVerticalOffset = 0;
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        ShowHeaderImmediate();
        _lastVerticalOffset = 0;
    }

    private void HeaderContainer_SizeChanged(object? sender, EventArgs e)
    {
        if (_headerNaturalHeight > 0)
            return;

        if (HeaderContainer.Height <= 0)
            return;

        _headerNaturalHeight = HeaderContainer.Height;
        HeaderSlot.HeightRequest = _headerNaturalHeight;
        ShowHeaderImmediate();
    }

    private void ShowHeaderImmediate()
    {
        if (_headerNaturalHeight <= 0)
            return;

        HeaderContainer.CancelAnimations();
        HeaderContainer.IsVisible = true;
        HeaderSlot.HeightRequest = _headerNaturalHeight;
        HeaderContainer.Opacity = 1;
        HeaderContainer.TranslationY = 0;

        _isHeaderHidden = false;
        _isHeaderAnimating = false;
    }

    private async Task HideHeaderAsync()
    {
        if (_headerNaturalHeight <= 0 || _isHeaderHidden || _isHeaderAnimating)
            return;

        _isHeaderAnimating = true;

        try
        {
            HeaderContainer.CancelAnimations();

            await Task.WhenAll(
                HeaderContainer.TranslateToAsync(0, -18, 140, Easing.CubicOut),
                HeaderContainer.FadeToAsync(0, 120, Easing.CubicOut)
            );

            HeaderContainer.IsVisible = false;
            HeaderSlot.HeightRequest = 0;
            HeaderContainer.Opacity = 1;
            HeaderContainer.TranslationY = 0;

            _isHeaderHidden = true;
        }
        finally
        {
            _isHeaderAnimating = false;
        }
    }

    private async Task ShowHeaderAsync()
    {
        if (_headerNaturalHeight <= 0 || !_isHeaderHidden || _isHeaderAnimating)
            return;

        _isHeaderAnimating = true;

        try
        {
            HeaderContainer.CancelAnimations();

            HeaderSlot.HeightRequest = _headerNaturalHeight;
            HeaderContainer.IsVisible = true;
            HeaderContainer.Opacity = 0;
            HeaderContainer.TranslationY = -18;

            await Task.WhenAll(
                HeaderContainer.TranslateToAsync(0, 0, 160, Easing.CubicOut),
                HeaderContainer.FadeToAsync(1, 160, Easing.CubicOut)
            );

            _isHeaderHidden = false;
        }
        finally
        {
            _isHeaderAnimating = false;
        }
    }

    private async void CardsCollectionView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        if (_headerNaturalHeight <= 0)
            return;

        // Для короткого списка шапка всегда должна быть видна
        if (!CanCollapseHeader())
        {
            ShowHeaderImmediate();
            _lastVerticalOffset = e.VerticalOffset;
            return;
        }

        // Во время поиска шапка всегда видна
        if (_isSearchFocused || !string.IsNullOrWhiteSpace(_viewModel.SearchText))
        {
            ShowHeaderImmediate();
            _lastVerticalOffset = e.VerticalOffset;
            return;
        }

        // В верхней точке шапка всегда видна
        if (e.VerticalOffset <= 0)
        {
            ShowHeaderImmediate();
            _lastVerticalOffset = 0;
            return;
        }

        double delta = e.VerticalOffset - _lastVerticalOffset;
        _lastVerticalOffset = e.VerticalOffset;

        if (_isHeaderAnimating)
            return;

        if (Math.Abs(delta) < ScrollDeltaThreshold)
            return;

        if (delta > 0 && e.VerticalOffset > HideAfterOffset)
        {
            await HideHeaderAsync();
        }
        else if (delta < 0 || e.VerticalOffset <= HideAfterOffset)
        {
            await ShowHeaderAsync();
        }
    }

    private void SearchEntry_Focused(object sender, FocusEventArgs e)
    {
        _isSearchFocused = true;
        ShowHeaderImmediate();
        _lastVerticalOffset = 0;
    }

    private void SearchEntry_Unfocused(object sender, FocusEventArgs e)
    {
        _isSearchFocused = false;
        ShowHeaderImmediate();
        _lastVerticalOffset = 0;
    }

    private void SearchEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        ShowHeaderImmediate();
        _lastVerticalOffset = 0;

        Dispatcher.Dispatch(() =>
        {
            try
            {
                if (_viewModel.Cards != null && _viewModel.Cards.Count > 0)
                    CardsCollectionView.ScrollTo(0, position: ScrollToPosition.Start, animate: false);
            }
            catch
            {
            }

            ShowHeaderImmediate();
            _lastVerticalOffset = 0;
        });
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

    private async void Card_Tapped(object sender, TappedEventArgs e)
    {
        if (_isCardTapInProgress || _isAnimating)
            return;

        if (sender is not Grid cardGrid)
            return;

        if (cardGrid.BindingContext is not LoyaltyCard card)
            return;

        _isCardTapInProgress = true;

        try
        {
            await Task.WhenAll(
                cardGrid.ScaleToAsync(0.97, 80, Easing.CubicOut),
                cardGrid.FadeToAsync(0.96, 80, Easing.CubicOut)
            );

            await Task.WhenAll(
                cardGrid.ScaleToAsync(1.0, 80, Easing.CubicIn),
                cardGrid.FadeToAsync(1.0, 80, Easing.CubicIn)
            );

            if (_viewModel.OpenCardDetailsCommand.CanExecute(card))
                _viewModel.OpenCardDetailsCommand.Execute(card);
        }
        finally
        {
            _isCardTapInProgress = false;
        }
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
            ShowHeaderImmediate();
            _lastVerticalOffset = 0;
        }
        else
        {
            await DisplayAlertAsync("Ошибка", "Не удалось удалить карту.", "OK");
        }
    }
    private void ConditionsRow_Tapped(object sender, TappedEventArgs e)
    {
        ConditionsButton_Clicked(sender, EventArgs.Empty);
    }

    private void ContactsRow_Tapped(object sender, TappedEventArgs e)
    {
        ContactsButton_Clicked(sender, EventArgs.Empty);
    }
}