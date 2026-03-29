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
         CardNumber = "CH-100001",
         BonusBalance = 250,
         QrCodeValue = "QR-KO-100001"
     },
     new LoyaltyCard
     {
         Id = 2,
         CafeName = "Розмарин",
         CardNumber = "CH-100002",
         BonusBalance = 150,
         QrCodeValue = "QR-RN-100002"
     },
     new LoyaltyCard
     {
         Id = 3,
         CafeName = "Чайка",
         CardNumber = "CH-100003",
         BonusBalance = 50,
         QrCodeValue = "QR-CA-100003"
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
            "Удаление карты",
            $"Бонусная карта заведения {cafename} была успешно удалена.");
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
        string qrCode = $"QR-{cardNumber}";

        var newCard = new LoyaltyCard
        {
            Id = _nextId,
            CafeName = cafename,
            CardNumber = cardNumber,
            BonusBalance = 0,
            QrCodeValue = qrCode
        };
        _cards.Add(newCard);
        _notificationService.AddNotification(
            "Создание карт",
            $"Бонусная карта для заведения {cafename} была успешно создана.");
        _nextId++;
        return newCard;
    }
    private string GetPrefix(string cafeName)
    {
        return cafeName switch
        {
            "Калипсо" => "KO",
            "Розмарин" => "RN",
            "Чайка" => "CA",
            "Мечтатели" => "MI",
            "Сахара не надо" => "SO",
            "Ательер" => "AR",
            "Комод" => "KD",
            _ => "XX"
        };
    }
}