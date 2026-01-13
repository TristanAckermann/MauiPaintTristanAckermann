using MauiPaintTristanAckermann.ViewModels;

namespace MauiPaintTristanAckermann.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(AccountViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}