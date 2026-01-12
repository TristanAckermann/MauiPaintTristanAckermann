namespace MauiPaintTristanAckermann.Models;

public class Drawing
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = "Unbenannt";
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string ImagePath { get; set; } = string.Empty;
    public string ThumbnailPath { get; set; } = string.Empty;
}