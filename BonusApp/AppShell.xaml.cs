using BonusApp.Views;
namespace BonusApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CardsPage), typeof(CardsPage));
            Routing.RegisterRoute(nameof(CardDetailsPage), typeof(CardDetailsPage));
            Routing.RegisterRoute(nameof(TransactionHistoryPage), typeof(TransactionHistoryPage));
        }
    }
}
