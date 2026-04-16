using BonusApp.Models;

namespace BonusApp.Services;

public class TransactionService
{
    private static readonly TransactionService _instance = new();
    public static TransactionService Instance => _instance;

    private readonly Dictionary<int, List<TransactionItem>> _transactionsByUser = new();

    private TransactionService()
    {
    }

    public List<TransactionItem> GetAllTransactions()
    {
        return GetCurrentUserTransactions()
            .OrderByDescending(x => x.Date)
            .ToList();
    }

    public List<TransactionItem> GetTransactionsByCardId(int cardId)
    {
        return GetCurrentUserTransactions()
            .Where(x => x.CardId == cardId)
            .OrderByDescending(x => x.Date)
            .ToList();
    }

    private List<TransactionItem> GetCurrentUserTransactions()
    {
        int userId = SessionService.Instance.UserId;

        if (_transactionsByUser.TryGetValue(userId, out var transactions))
        {
            return transactions;
        }

        transactions = DemoAccountDefaults.IsCurrentSessionDemoAccount()
            ? DemoAccountDefaults.CreateTransactions()
            : [];

        _transactionsByUser[userId] = transactions;
        return transactions;
    }
}
