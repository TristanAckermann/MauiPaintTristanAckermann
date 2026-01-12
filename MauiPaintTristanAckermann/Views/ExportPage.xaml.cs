using MauiPaintTristanAckermann.ViewModels;

namespace MauiPaintTristanAckermann.Views;

public partial class ExportPage : ContentPage
{
    public ExportPage(ExportViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}