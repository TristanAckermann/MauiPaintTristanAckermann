using Microsoft.Maui.Controls;
using SQLite;

namespace MauiPaintTristanAckermann.Models;

public class GalleryItem
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Ignore]
    public ImageSource Image { get; set; }
    
    public byte[] ImageData { get; set; }
    public DateTime CreatedAt { get; set; }
    public string OwnerName { get; set; }
}