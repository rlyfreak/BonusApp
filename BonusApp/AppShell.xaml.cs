using BonusApp.Views;

namespace BonusApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(CatalogPage), typeof(CatalogPage));
        Routing.RegisterRoute(nameof(NotificationsPage), typeof(NotificationsPage));
        Routing.RegisterRoute(nameof(TransactionHistoryPage), typeof(TransactionHistoryPage));
        Routing.RegisterRoute(nameof(EditPhonePage), typeof(EditPhonePage));
        Routing.RegisterRoute(nameof(EditEmailPage), typeof(EditEmailPage));
        Routing.RegisterRoute(nameof(EditPersonalDataPage), typeof(EditPersonalDataPage));
    }
}