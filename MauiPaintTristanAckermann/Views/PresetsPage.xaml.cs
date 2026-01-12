using MauiPaintTristanAckermann.ViewModels;

namespace MauiPaintTristanAckermann.Views;

public partial class PresetsPage : ContentPage
{
    public PresetsPage(PresetsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}