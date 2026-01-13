using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using MauiPaintTristanAckermann.Models;

namespace MauiPaintTristanAckermann.Services;

public interface IDrawingService
{
    ObservableCollection<GalleryItem> GalleryImages { get; }
    GalleryItem SelectedImage { get; set; }
    
    event EventHandler UserChanged;
    
    string CurrentUser { get; }
    
    float CurrentLineWidth { get; set; }
    Color CurrentColor { get; set; }

    void SetUser(string userName);
    void AddToGallery(GalleryItem item);
}