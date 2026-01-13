namespace MauiPaintTristanAckermann.Models;

public class UserProfile
{
    public string Name { get; set; } = string.Empty;
    public bool AutoSave { get; set; }
    public string PreferredPalette { get; set; } = "Standard";
}