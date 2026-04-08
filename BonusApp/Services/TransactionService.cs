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
            Description = "Покупка на 320 ₽",
            BalanceBefore = 290,
            BalanceAfter = 340,
            VenueAddress = "Калипсо, улица 50-летия Белгородской области, 6",
            OperationCode = "8B5712C2847A",
            Comment = "Покупка в заведении"
        },
        new TransactionItem
        {
            Id = 2,
            CardId = 1,
            Type = "Списание",
            BonusAmount = 20,
            Date = DateTime.Now.AddHours(-5),
            Description = "Списание бонусов",
            BalanceBefore = 122,
            BalanceAfter = 102,
            VenueAddress = "Калипсо, улица 50-летия Белгородской области, 6",
            OperationCode = "8B5712C2847B",
            Comment = "Покупка в заведении"
        },
        new TransactionItem
        {
            Id = 3,
            CardId = 2,
            Type = "Начисление",
            BonusAmount = 35,
            Date = DateTime.Now.AddDays(-1).AddHours(-3),
            Description = "Покупка на 780 ₽",
            BalanceBefore = 115,
            BalanceAfter = 150,
            VenueAddress = "Розмарин, улица Победы, 12",
            OperationCode = "8B5712C2847C",
            Comment = "Покупка в заведении"
        },
        new TransactionItem
        {
            Id = 4,
            CardId = 3,
            Type = "Начисление",
            BonusAmount = 100,
            Date = DateTime.Now.AddDays(-3),
            Description = "Оплата на 520 ₽",
            BalanceBefore = 0,
            BalanceAfter = 100,
            VenueAddress = "Чайка, проспект Славы, 31",
            OperationCode = "8B5712C2847D",
            Comment = "Покупка в заведении"
        },
        new TransactionItem
        {
            Id = 5,
            CardId = 3,
            Type = "Списание",
            BonusAmount = 40,
            Date = DateTime.Now.AddDays(-5),
            Description = "Списание бонусами",
            BalanceBefore = 90,
            BalanceAfter = 50,
            VenueAddress = "Чайка, проспект Славы, 31",
            OperationCode = "8B5712C2847E",
            Comment = "Покупка в заведении"
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