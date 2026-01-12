using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace MauiPaintTristanAckermann.Services;

public class DrawingService : IDrawingService
{
    public ObservableCollection<ImageSource> GalleryImages { get; } = new ObservableCollection<ImageSource>();
    
    // Standardwerte setzen
    public float CurrentLineWidth { get; set; } = 5f;
    public Color CurrentColor { get; set; } = Colors.Black;

    public void AddToGallery(ImageSource image)
    {
        if (image != null) GalleryImages.Add(image);
    }
}