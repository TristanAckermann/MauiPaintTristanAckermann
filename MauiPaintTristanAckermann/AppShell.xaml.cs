using MauiPaintTristanAckermann.Views;

namespace MauiPaintTristanAckermann;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Hier müssen alle Seiten registriert werden, die über "GoToAsync" aufgerufen werden
        Routing.RegisterRoute(nameof(ProfileSummaryPage), typeof(ProfileSummaryPage));
        Routing.RegisterRoute(nameof(AccountPage), typeof(AccountPage));
 
    }
}