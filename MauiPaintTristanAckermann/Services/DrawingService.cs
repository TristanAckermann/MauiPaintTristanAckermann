using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using MauiPaintTristanAckermann.Models;

namespace MauiPaintTristanAckermann.Services;

public class DrawingService : IDrawingService
{
    private readonly List<GalleryItem> _allImages = new();

    public event EventHandler UserChanged;

    public ObservableCollection<GalleryItem> GalleryImages { get; } = new ObservableCollection<GalleryItem>();
    public GalleryItem SelectedImage { get; set; }
    
    public string CurrentUser { get; private set; } = "Gast"; // Default User

    // Standardwerte setzen
    public float CurrentLineWidth { get; set; } = 5f;
    public Color CurrentColor { get; set; } = Colors.Black;

    public DrawingService()
    {
    }

    public void SetUser(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName)) return;
        
        CurrentUser = userName;
        RefreshGallery();
        UserChanged?.Invoke(this, EventArgs.Empty);
    }

    private void RefreshGallery()
    {
        GalleryImages.Clear();
        foreach (var item in _allImages)
        {
            if (item.OwnerName == CurrentUser)
            {
                GalleryImages.Add(item);
            }
        }
    }

    public void AddToGallery(GalleryItem item)
    {
        if (item != null)
        {
            item.OwnerName = CurrentUser;
            _allImages.Add(item);
            GalleryImages.Add(item);
        }
    }
}