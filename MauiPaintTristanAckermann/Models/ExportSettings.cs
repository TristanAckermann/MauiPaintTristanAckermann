namespace MauiPaintTristanAckermann.Models;

public class ExportSettings
{
    public string FileName { get; set; } = string.Empty;
    public string Format { get; set; } = "PNG";
    public bool UseTransparency { get; set; }
    public DateTime ExportTime { get; set; }
}