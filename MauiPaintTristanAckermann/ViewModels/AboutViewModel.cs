using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiPaintTristanAckermann.ViewModels;

public partial class AboutViewModel : BaseViewModel
{
    public string AppName => "Maui Paint Studio";
    public string Version => "1.0.0 (Build 2026)";
    public string Developer => "Tristan Ackermann";
    public string Description => "Eine leistungsstarke, cross-plattform Zeichen-App, entwickelt mit .NET MAUI und MVVM.";

    public AboutViewModel()
    {
        Title = "Über diese App";
    }

    [RelayCommand]
    private async Task OpenGitHub()
    {
        await Launcher.Default.OpenAsync("https://github.com/TristanAckermann");
    }
}