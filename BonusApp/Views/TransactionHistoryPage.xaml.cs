using BonusApp.ViewModels;

namespace BonusApp.Views;

[QueryProperty(nameof(CardId), "cardId")]
public partial class TransactionHistoryPage : ContentPage
{
    private readonly TransactionHistoryViewModel _viewModel;
    private string _cardId = string.Empty;

    public string CardId
    {
        get => _cardId;
        set
        {
            _cardId = value;

            if (int.TryParse(value, out int id))
            {
                _viewModel.LoadTransactions(id);
            }
        }
    }

    public TransactionHistoryPage()
    {
        InitializeComponent();

        _viewModel = new TransactionHistoryViewModel();
        BindingContext = _viewModel;
    }
}