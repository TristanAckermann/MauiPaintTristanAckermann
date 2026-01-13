using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using MauiPaintTristanAckermann.Views;
using MauiPaintTristanAckermann.ViewModels;
using MauiPaintTristanAckermann.Services;

namespace MauiPaintTristanAckermann;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // SERVICES (Wichtig: Singleton damit Daten erhalten bleiben)
        builder.Services.AddSingleton<IDrawingService, DrawingService>();
        builder.Services.AddSingleton<ValidationService>();

        // VIEWMODELS
        builder.Services.AddTransient<AccountViewModel>();
        builder.Services.AddTransient<ProfileSummaryViewModel>();
        builder.Services.AddTransient<GalleryViewModel>();
        builder.Services.AddTransient<PresetsViewModel>(); 

        // PAGES
        builder.Services.AddTransient<DrawPage>();
        builder.Services.AddTransient<AccountPage>();
        builder.Services.AddTransient<ProfileSummaryPage>();
        builder.Services.AddTransient<GalleryPage>();
        builder.Services.AddTransient<PresetsPage>();
        builder.Services.AddTransient<LoginPage>(); // New Login Page

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}