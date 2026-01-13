using Microsoft.Maui.Controls;

namespace MauiPaintTristanAckermann.Models;

public class GalleryItem
{
    public ImageSource Image { get; set; }
    public byte[] ImageData { get; set; }
    public DateTime CreatedAt { get; set; }
    public string OwnerName { get; set; }
}