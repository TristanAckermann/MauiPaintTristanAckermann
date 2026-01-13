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
            
            _ = drawingService.SetUser(storedUser);
            MainPage = new AppShell();
        }
        else
        {
      
            MainPage = serviceProvider.GetService<LoginPage>();
        }
    }
}