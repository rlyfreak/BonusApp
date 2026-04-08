using System.ComponentModel;
using BonusApp.ViewModels;

namespace BonusApp.Views;

public partial class HistoryPage : ContentPage
{
    private readonly HistoryViewModel _viewModel;
    private bool _isAnimating;

    public HistoryPage()
    {
        InitializeComponent();

        _viewModel = new HistoryViewModel();
        BindingContext = _viewModel;
        _viewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadHistory();
    }

    private async void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(HistoryViewModel.IsTransactionSheetOpen))
            return;

        if (_isAnimating)
            return;

        _isAnimating = true;

        if (_viewModel.IsTransactionSheetOpen)
        {
            TransactionSheetOverlay.IsVisible = true;
            TransactionSheetContainer.TranslationY = 1200;
            await TransactionSheetContainer.TranslateToAsync(0, 0, 220, Easing.CubicOut);
        }
        else
        {
            await TransactionSheetContainer.TranslateToAsync(0, 1200, 180, Easing.CubicIn);
            TransactionSheetOverlay.IsVisible = false;
        }

        _isAnimating = false;
    }

    private void CloseTransactionSheetButton_Clicked(object sender, EventArgs e)
    {
        _viewModel.CloseTransactionSheetCommand.Execute(null);
    }

    private async void CopyOperationIdButton_Clicked(object sender, EventArgs e)
    {
        if (_viewModel.SelectedRecord == null)
            return;

        await Clipboard.Default.SetTextAsync(_viewModel.SelectedRecord.OperationCode);
        await DisplayAlertAsync("Скопировано", "ID операции скопирован.", "OK");
    }
}