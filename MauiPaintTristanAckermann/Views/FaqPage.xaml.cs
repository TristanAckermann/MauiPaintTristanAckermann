using MauiPaintTristanAckermann.ViewModels;

namespace MauiPaintTristanAckermann.Views;

public partial class FaqPage : ContentPage
{
    public FaqPage(FaqViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}