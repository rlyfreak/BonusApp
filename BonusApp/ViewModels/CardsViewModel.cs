using System.Collections.ObjectModel;
using System.Windows.Input;
using BonusApp.Models;
using BonusApp.Services;
using BonusApp.Views;

namespace BonusApp.ViewModels
{
    public class CardsViewModel : BaseViewModel
    {
        private readonly CardService _cardService;
        public ObservableCollection<LoyaltyCard> Cards { get; } = new();
        public ICommand OpenAddCardPageCommand { get; }
        public CardsViewModel()
        {
            _cardService = CardService.Instance;

            OpenAddCardPageCommand = new Command(async () =>
                {
                await Shell.Current.GoToAsync(nameof(AddCardPage));
                });
        }
        public void LoadCards()
        {
            Cards.Clear();
            var cards = _cardService.GetCards();
            foreach (var card in cards) {
                Cards.Add(card);
            }
        }
        public async Task OpenCardDetailsAsync(LoyaltyCard selectedCard)
        {
            if (selectedCard == null) return;
            await Shell.Current.GoToAsync($"{nameof(CardDetailsPage)}?cardId={selectedCard.Id}");
        }
    }
}
