using MauiPaintTristanAckermann.ViewModels;

namespace MauiPaintTristanAckermann.Views;

public partial class AboutPage : ContentPage
{
    public AboutPage(AboutViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private async void OnLinkClicked(object sender, EventArgs e)
    {
      
        await Launcher.Default.OpenAsync("https://github.com/TristanAckermann");
    }
}