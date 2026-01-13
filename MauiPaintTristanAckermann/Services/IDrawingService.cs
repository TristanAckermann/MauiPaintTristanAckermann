using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using MauiPaintTristanAckermann.Models;

namespace MauiPaintTristanAckermann.Services;

public interface IDrawingService
{
    ObservableCollection<GalleryItem> GalleryImages { get; }
    GalleryItem SelectedImage { get; set; }
    
    event EventHandler UserChanged;
    event EventHandler<float> LineWidthChanged;
    
    string CurrentUser { get; }
    
    float CurrentLineWidth { get; set; }
    Color CurrentColor { get; set; }

    Task SetUser(string userName);
    Task AddToGallery(GalleryItem item);
}