using MauiPaintTristanAckermann.Views;

namespace MauiPaintTristanAckermann;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        
        Routing.RegisterRoute(nameof(ProfileSummaryPage), typeof(ProfileSummaryPage));
        Routing.RegisterRoute(nameof(AccountPage), typeof(AccountPage));
 
    }
}