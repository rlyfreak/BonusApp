using BonusApp.Models;

namespace BonusApp.Services;

public class TransactionService
{
    private readonly List<TransactionItem> _transactions = new()
    {
        new TransactionItem
        {
            Id = 1,
            CardId = 1,
            Type = "Начисление",
            BonusAmount = 50,
            Date = DateTime.Now.AddHours(-2),
        },
        new TransactionItem
        {
            Id = 2,
            CardId = 1,
            Type = "Списание",
            BonusAmount = 20,
            Date = DateTime.Now.AddHours(-5),
        },
        new TransactionItem
        {
            Id = 3,
            CardId = 2,
            Type = "Начисление",
            BonusAmount = 35,
            Date = DateTime.Now.AddDays(-1).AddHours(-3),
        },
        new TransactionItem
        {
            Id = 4,
            CardId = 3,
            Type = "Начисление",
            BonusAmount = 100,
            Date = DateTime.Now.AddDays(-3),
        },
        new TransactionItem
        {
            Id = 5,
            CardId = 3,
            Type = "Списание",
            BonusAmount = 40,
            Date = DateTime.Now.AddDays(-5),
        }
    };

    public List<TransactionItem> GetAllTransactions()
    {
        return _transactions
            .OrderByDescending(x => x.Date)
            .ToList();
    }

    public List<TransactionItem> GetTransactionsByCardId(int cardId)
    {
        return _transactions
            .Where(x => x.CardId == cardId)
            .OrderByDescending(x => x.Date)
            .ToList();
    }
}