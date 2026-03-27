using BonusApp.Models;

namespace BonusApp.Services;

public class CardService
{
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

    public List<LoyaltyCard> GetCards()
    {
        return _cards;
    }

    public LoyaltyCard? GetCardById(int id)
    {
        return _cards.FirstOrDefault(x => x.Id == id);
    }
}