using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace MauiPaintTristanAckermann.Services;

public interface IDrawingService
{
    ObservableCollection<ImageSource> GalleryImages { get; }
    void AddToGallery(ImageSource image);
    
    // Diese Properties braucht dein PresetsViewModel unbedingt!
    float CurrentLineWidth { get; set; }
    Color CurrentColor { get; set; }
}