using System.Collections.ObjectModel;
using BonusApp.Models;
using BonusApp.Services;

namespace BonusApp.ViewModels;

public class TransactionHistoryViewModel : BaseViewModel
{
    private readonly TransactionService _transactionService;
    private readonly CardService _cardService;

    private string _cardInfoText = "История операций";
    public string CardInfoText
    {
        get => _cardInfoText;
        set => SetProperty(ref _cardInfoText, value);
    }

    public ObservableCollection<TransactionItem> Transactions { get; } = new();

    public TransactionHistoryViewModel()
    {
        _transactionService = new TransactionService();
        _cardService = CardService.Instance;
    }

    public void LoadTransactions(int cardId)
    {
        Transactions.Clear();

        var card = _cardService.GetCardById(cardId);
        CardInfoText = card == null
            ? $"История операций по карте ID: {cardId}"
            : $"История операций по карте {card.CafeName}";

        var transactions = _transactionService.GetTransactionsByCardId(cardId);

        foreach (var transaction in transactions)
        {
            Transactions.Add(transaction);
        }
    }
}