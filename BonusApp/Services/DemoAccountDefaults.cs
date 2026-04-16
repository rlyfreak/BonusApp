using BonusApp.Models;

namespace BonusApp.Services;

internal static class DemoAccountDefaults
{
    private const string TestEmail = "demo@bonusapp.local";
    private const string TestPhone = "+79990000000";

    public static bool IsCurrentSessionDemoAccount()
    {
        var session = SessionService.Instance;

        if (!session.IsAuthenticated)
        {
            return false;
        }

        return string.Equals(session.Email, TestEmail, StringComparison.OrdinalIgnoreCase)
            || string.Equals(session.PhoneNumber, TestPhone, StringComparison.Ordinal);
    }

    public static List<LoyaltyCard> CreateCards()
    {
        return
        [
            new LoyaltyCard
            {
                Id = 1,
                CafeName = "Калипсо",
                CardNumber = "KO-100001",
                BonusBalance = 250,
                QrCodeSource = "qr_code_kalipso.svg"
            },
            new LoyaltyCard
            {
                Id = 2,
                CafeName = "Розмарин",
                CardNumber = "RN-100002",
                BonusBalance = 150,
                QrCodeSource = "qr_code_rozmarin.svg"
            },
            new LoyaltyCard
            {
                Id = 3,
                CafeName = "Комод",
                CardNumber = "KD-100003",
                BonusBalance = 50,
                QrCodeSource = "qr_code_komod.svg"
            }
        ];
    }

    public static List<NotificationItem> CreateNotifications()
    {
        return
        [
            new NotificationItem
            {
                Id = 1,
                Title = "Карта удалена",
                Message = "Знак удалена из приложения",
                Date = DateTime.Today.AddDays(-1).AddHours(19).AddMinutes(22),
                IconSource = "notification_remove.svg"
            },
            new NotificationItem
            {
                Id = 2,
                Title = "Карта добавлена",
                Message = "Розмарин добавлена в приложение",
                Date = DateTime.Today.AddDays(-7).AddHours(14).AddMinutes(10),
                IconSource = "notification_add.svg"
            }
        ];
    }

    public static List<TransactionItem> CreateTransactions()
    {
        return
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
    }

    public static UserProfile CreateProfile()
    {
        return new UserProfile
        {
            LastName = "Бондаренко",
            FirstName = "Дмитрий",
            MiddleName = "Сергеевич",
            BirthDate = new DateTime(2007, 9, 10),
            PhoneNumber = SessionService.Instance.PhoneNumber,
            Email = SessionService.Instance.Email
        };
    }

    public static UserProfile CreateEmptyProfile()
    {
        return new UserProfile
        {
            PhoneNumber = SessionService.Instance.PhoneNumber,
            Email = SessionService.Instance.Email
        };
    }

    private static DateTime CreateDemoDate(int dayOffset, int hour, int minute)
    {
        return DateTime.Today.AddDays(dayOffset).AddHours(hour).AddMinutes(minute);
    }
}
