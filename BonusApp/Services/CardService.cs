using BonusApp.Models;

namespace BonusApp.Services;

public class CardService
{
    private static readonly CardService _instance = new();
    public static CardService Instance => _instance;

    private readonly Dictionary<int, List<LoyaltyCard>> _cardsByUser = new();
    private readonly Dictionary<int, int> _nextIdsByUser = new();
    private readonly NotificationService _notificationService;

    private CardService()
    {
        _notificationService = NotificationService.Instance;
    }

    public List<LoyaltyCard> GetCards()
    {
        return GetCurrentUserCards().ToList();
    }

    public LoyaltyCard? GetCardById(int id)
    {
        return GetCurrentUserCards().FirstOrDefault(x => x.Id == id);
    }

    public bool DeleteCard(int id)
    {
        var cards = GetCurrentUserCards();
        var card = cards.FirstOrDefault(x => x.Id == id);
        if (card == null)
        {
            return false;
        }

        cards.Remove(card);
        _notificationService.AddNotification(
            "Карта удалена",
            $"{card.CafeName} удалена из приложения.");

        return true;
    }

    public bool HasCardForCafe(string cafeName)
    {
        return GetCurrentUserCards().Any(x => string.Equals(x.CafeName, cafeName, StringComparison.Ordinal));
    }

    public LoyaltyCard? AddCard(string cafeName)
    {
        if (HasCardForCafe(cafeName))
        {
            return null;
        }

        var cafeInfo = CafeCatalog.GetByName(cafeName);
        if (cafeInfo == null)
        {
            return null;
        }

        int userId = GetCurrentUserId();
        int nextId = _nextIdsByUser[userId];
        string cardNumber = $"{cafeInfo.CardPrefix}-{nextId:000000}";

        var newCard = new LoyaltyCard
        {
            Id = nextId,
            CafeName = cafeName,
            CardNumber = cardNumber,
            BonusBalance = 0,
            QrCodeSource = cafeInfo.QrCodeSource
        };

        GetCurrentUserCards().Add(newCard);
        _notificationService.AddNotification(
            "Карта добавлена",
            $"{cafeName} добавлена в приложение.");

        _nextIdsByUser[userId] = nextId + 1;
        return newCard;
    }

    private List<LoyaltyCard> GetCurrentUserCards()
    {
        int userId = GetCurrentUserId();

        if (_cardsByUser.TryGetValue(userId, out var cards))
        {
            return cards;
        }

        cards = DemoAccountDefaults.IsCurrentSessionDemoAccount()
            ? DemoAccountDefaults.CreateCards()
            : [];

        _cardsByUser[userId] = cards;
        _nextIdsByUser[userId] = cards.Count == 0 ? 1 : cards.Max(x => x.Id) + 1;
        return cards;
    }

    private static int GetCurrentUserId()
    {
        return SessionService.Instance.UserId;
    }
}
