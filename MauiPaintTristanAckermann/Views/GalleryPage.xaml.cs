using MauiPaintTristanAckermann.ViewModels;

namespace MauiPaintTristanAckermann.Views;

public partial class GalleryPage : ContentPage
{
    public GalleryPage(GalleryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}