using BonusApp.Services;

namespace BonusApp.Views;

[QueryProperty(nameof(CardId), "cardId")]
public partial class TransactionHistoryPage : ContentPage
{
    private readonly TransactionService _transactionService;

    private string _cardId = string.Empty;
    public string CardId
    {
        get => _cardId;
        set
        {
            _cardId = value;
            LoadTransactions();
        }
    }

    public TransactionHistoryPage()
    {
        InitializeComponent();
        _transactionService = new TransactionService();
    }

    private void LoadTransactions()
    {
        if (string.IsNullOrWhiteSpace(_cardId))
            return;

        if (!int.TryParse(_cardId, out int id))
            return;

        var transactions = _transactionService.GetTransactionsByCardId(id);
        TransactionsCollectionView.ItemsSource = transactions;
        CardInfoLabel.Text = $"История операций по карте ID: {id}";
    }
}