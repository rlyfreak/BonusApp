using BonusApp.Models;

namespace BonusApp.Services;

public class TransactionService
{
    private static readonly TransactionService _instance = new();
    public static TransactionService Instance => _instance;

    private readonly List<TransactionItem> _transactions =
    [
        new TransactionItem
        {
            Id = 1,
            CardId = 1,
            CafeName = "Калипсо",
            Type = "Начисление",
            BonusAmount = 50,
            Date = CreateDemoDate(0, 10, 30),
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
            CafeName = "Калипсо",
            Type = "Списание",
            BonusAmount = 20,
            Date = CreateDemoDate(0, 7, 15),
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
            CafeName = "Розмарин",
            Type = "Начисление",
            BonusAmount = 35,
            Date = CreateDemoDate(-1, 18, 40),
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
            CafeName = "Комод",
            Type = "Начисление",
            BonusAmount = 100,
            Date = CreateDemoDate(-3, 14, 5),
            Description = "Оплата на 520 ₽",
            BalanceBefore = 0,
            BalanceAfter = 100,
            VenueAddress = "Комод, Гражданский проспект, 59А",
            OperationCode = "8B5712C2847D",
            Comment = "Покупка в заведении"
        },
        new TransactionItem
        {
            Id = 5,
            CardId = 3,
            CafeName = "Комод",
            Type = "Списание",
            BonusAmount = 40,
            Date = CreateDemoDate(-5, 12, 20),
            Description = "Списание бонусами",
            BalanceBefore = 90,
            BalanceAfter = 50,
            VenueAddress = "Комод, Гражданский проспект, 59А",
            OperationCode = "8B5712C2847E",
            Comment = "Покупка в заведении"
        }
    ];

    private TransactionService()
    {
    }

    private static DateTime CreateDemoDate(int dayOffset, int hour, int minute)
    {
        return DateTime.Today.AddDays(dayOffset).AddHours(hour).AddMinutes(minute);
    }

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
