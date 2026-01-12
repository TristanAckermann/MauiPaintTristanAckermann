using MauiPaintTristanAckermann.ViewModels;

namespace MauiPaintTristanAckermann.Views;

public partial class DrawPage : ContentPage
{
    public DrawPage(DrawViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}