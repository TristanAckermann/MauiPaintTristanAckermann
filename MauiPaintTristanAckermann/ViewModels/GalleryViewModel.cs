using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using MauiPaintTristanAckermann.Services;
using Microsoft.Maui.Controls;

namespace MauiPaintTristanAckermann.ViewModels;

public partial class GalleryViewModel : ObservableObject
{
    private readonly IDrawingService _drawingService;

    // Die View bindet an diese Eigenschaft
    public ObservableCollection<ImageSource> GalleryImages => _drawingService.GalleryImages;

    public GalleryViewModel(IDrawingService drawingService)
    {
        _drawingService = drawingService;
    }
}