using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
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
            .UseMauiCommunityToolkit() // Wichtig für die UI-Tools!
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // 1. Services registrieren
        builder.Services.AddSingleton<ValidationService>();

        // 2. ViewModels registrieren
        builder.Services.AddTransient<AccountViewModel>();
        builder.Services.AddTransient<ProfileSummaryViewModel>();
        builder.Services.AddTransient<GalleryViewModel>();

        // 3. Pages registrieren
        builder.Services.AddTransient<DrawPage>();
        builder.Services.AddTransient<AccountPage>();
        builder.Services.AddTransient<ProfileSummaryPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}