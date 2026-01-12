namespace MauiPaintTristanAckermann;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Lädt die Shell, damit die TabBar unten erscheint
        MainPage = new AppShell(); 
    }
}