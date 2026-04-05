using BonusApp.Views;

namespace BonusApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(AddCardPage), typeof(AddCardPage));
        Routing.RegisterRoute(nameof(NotificationsPage), typeof(NotificationsPage));
        Routing.RegisterRoute(nameof(TransactionHistoryPage), typeof(TransactionHistoryPage));
    }
}