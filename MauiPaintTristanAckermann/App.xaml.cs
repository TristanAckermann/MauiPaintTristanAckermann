using MauiPaintTristanAckermann.Services;
using MauiPaintTristanAckermann.Views;

namespace MauiPaintTristanAckermann;

public partial class App : Application
{
    public App(IDrawingService drawingService, IServiceProvider serviceProvider)
    {
        InitializeComponent();

        var storedUser = Preferences.Get("mauipaint_user", string.Empty);

        if (!string.IsNullOrEmpty(storedUser))
        {
            // User exists -> Auto Login
            drawingService.SetUser(storedUser);
            MainPage = new AppShell();
        }
        else
        {
            // No User -> Force Login Page
            MainPage = serviceProvider.GetService<LoginPage>();
        }
    }
}