using System.ComponentModel;
using BonusApp.Models;
using BonusApp.ViewModels;

namespace BonusApp.Views;

public partial class CatalogPage : ContentPage
{
    private readonly CatalogViewModel _viewModel;

    private double _headerNaturalHeight = -1;
    private bool _isHeaderHidden;
    private bool _isHeaderAnimating;
    private double _lastVerticalOffset;
    private const double ScrollDeltaThreshold = 6;
    private const double HideAfterOffset = 20;
    private bool _isSearchFocused;

    private bool _isAnimating;
    private bool _isCatalogCardTapInProgress;

    private const int MinItemsForHeaderCollapse = 4;

    public CatalogPage()
    {
        InitializeComponent();

        _viewModel = new CatalogViewModel();
        BindingContext = _viewModel;
        _viewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    private bool CanCollapseHeader()
    {
        return _viewModel.Items.Count >= MinItemsForHeaderCollapse;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadCatalog();

        Dispatcher.Dispatch(() =>
        {
            ShowHeaderImmediate();
            _lastVerticalOffset = 0;
        });

        Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(60), () =>
        {
            try
            {
                CatalogCollectionView.ScrollTo(0, position: ScrollToPosition.Start, animate: false);
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

    private async void CatalogCollectionView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
    {
        if (_headerNaturalHeight <= 0)
            return;

        if (!CanCollapseHeader())
        {
            ShowHeaderImmediate();
            _lastVerticalOffset = e.VerticalOffset;
            return;
        }

        if (_isSearchFocused || !string.IsNullOrWhiteSpace(_viewModel.SearchText))
        {
            ShowHeaderImmediate();
            _lastVerticalOffset = e.VerticalOffset;
            return;
        }

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
            await HideHeaderAsync();
        else if (delta < 0 || e.VerticalOffset <= HideAfterOffset)
            await ShowHeaderAsync();
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
                if (_viewModel.Items.Count > 0)
                    CatalogCollectionView.ScrollTo(0, position: ScrollToPosition.Start, animate: false);
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
        if (e.PropertyName != nameof(CatalogViewModel.IsAddSheetOpen))
            return;

        if (_isAnimating)
            return;

        _isAnimating = true;

        if (_viewModel.IsAddSheetOpen)
        {
            AddCardSheetOverlay.IsVisible = true;
            AddCardSheetContainer.TranslationY = 1200;
            await AddCardSheetContainer.TranslateToAsync(0, 0, 220, Easing.CubicOut);
        }
        else
        {
            await AddCardSheetContainer.TranslateToAsync(0, 1200, 180, Easing.CubicIn);
            AddCardSheetOverlay.IsVisible = false;
        }

        _isAnimating = false;
    }

    private async void CatalogCard_Tapped(object sender, TappedEventArgs e)
    {
        if (_isCatalogCardTapInProgress || _isAnimating)
            return;

        if (sender is not Border catalogCardBorder)
            return;

        if (catalogCardBorder.BindingContext is not CatalogCardItem item)
            return;

        _isCatalogCardTapInProgress = true;

        try
        {
            await Task.WhenAll(
                catalogCardBorder.ScaleToAsync(0.97, 80, Easing.CubicOut),
                catalogCardBorder.FadeToAsync(0.96, 80, Easing.CubicOut)
            );

            await Task.WhenAll(
                catalogCardBorder.ScaleToAsync(1.0, 80, Easing.CubicIn),
                catalogCardBorder.FadeToAsync(1.0, 80, Easing.CubicIn)
            );

            if (_viewModel.OpenAddCardSheetCommand.CanExecute(item))
                _viewModel.OpenAddCardSheetCommand.Execute(item);
        }
        finally
        {
            _isCatalogCardTapInProgress = false;
        }
    }

    private void CloseAddCardSheetButton_Tapped(object sender, TappedEventArgs e)
    {
        _viewModel.CloseAddCardSheetCommand.Execute(null);
    }
}