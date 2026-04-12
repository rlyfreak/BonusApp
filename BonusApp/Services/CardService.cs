using BonusApp.Models;
namespace BonusApp.Services;

public class CardService
{
    private static readonly CardService _instance = new CardService();
    public static CardService Instance => _instance;

    private readonly List<LoyaltyCard> _cards = new()
    {
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
    };
    private int _nextId = 4;
    private readonly NotificationService _notificationService;
    private CardService()
    {
        _notificationService = NotificationService.Instance;
    }
    public List<LoyaltyCard> GetCards()
    {
        return _cards.ToList();
    }

    public LoyaltyCard? GetCardById(int id)
    {
        return _cards.FirstOrDefault(x => x.Id == id);
    }

    public bool DeleteCard(int id)
    {
        var card = _cards.FirstOrDefault(x => x.Id == id);
        if (card == null)
            return false;
        string cafename = card.CafeName;
        _cards.Remove(card);
        _notificationService.AddNotification(
            "Карта удалена",
            $"{cafename} удалена из приложения.");
        return true;
    }
    public bool HasCardForCafe(string cafeName)
    {
        return _cards.Any(x => x.CafeName == cafeName);
    }
    public LoyaltyCard? AddCard(string cafename)
    {
        if (HasCardForCafe(cafename))
            return null;
        string prefix = GetPrefix(cafename);
        string cardNumber = $"{prefix}-{_nextId:000000}";

        var newCard = new LoyaltyCard
        {
            Id = _nextId,
            CafeName = cafename,
            CardNumber = cardNumber,
            BonusBalance = 0,
            QrCodeSource = GetQrCodeSource(cafename)
        };
        _cards.Add(newCard);
        _notificationService.AddNotification(
            "Карта добавлена",
            $"{cafename} добавлена в приложение.");
        _nextId++;
        return newCard;
    }

    private string GetQrCodeSource(string cafename)
    {
        return cafename switch
        {
            "Калипсо" => "qr_code_kalipso.svg",
            "Розмарин" => "qr_code_rozmarin.svg",
            "KoMod" => "qr_code_komod.svg",
            "Винил" => "qr_code_vinyl.svg",
            "Знак" => "qr_code_znak.svg",
            "Редакция" => "qr_code_redac.svg",
            _ => string.Empty
        };
    }

    private string GetPrefix(string cafeName)
    {
        return cafeName switch
        {
            "Калипсо" => "KO",
            "Розмарин" => "RN",
            "Редакция" => "RA",
            "Знак" => "ZK",
            "Винил" => "VL",
            "Комод" => "KD",
            _ => "XX"
        };
    }
}