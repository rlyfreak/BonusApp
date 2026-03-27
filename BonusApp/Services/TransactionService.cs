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
            Date = DateTime.Now.AddDays(-3),
            Description = "Покупка: Капучино 0,3"
        },
        new TransactionItem
        {
            Id = 2,
            CardId = 1,
            Type = "Списание",
            BonusAmount = 20,
            Date = DateTime.Now.AddDays(-2),
            Description = "Оплата бонусами"
        },
        new TransactionItem
        {
            Id = 3,
            CardId = 2,
            Type = "Начисление",
            BonusAmount = 35,
            Date = DateTime.Now.AddDays(-4),
            Description = "Покупка: Латте 0,5"
        },
        new TransactionItem
        {
            Id = 4,
            CardId = 3,
            Type = "Начисление",
            BonusAmount = 100,
            Date = DateTime.Now.AddDays(-1),
            Description = "Покупка: Флет Уайт 0,3"
        },
        new TransactionItem
        {
            Id = 5,
            CardId = 3,
            Type = "Списание",
            BonusAmount = 40,
            Date = DateTime.Now,
            Description = "Скидка бонусами"
        }
    };

    public List<TransactionItem> GetTransactionsByCardId(int cardId)
    {
        return _transactions
            .Where(x => x.CardId == cardId)
            .OrderByDescending(x => x.Date)
            .ToList();
    }
}