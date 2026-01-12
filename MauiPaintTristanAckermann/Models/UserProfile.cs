namespace MauiPaintTristanAckermann.Models;

public class UserProfile
{
    public string Email { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public bool AutoSave { get; set; }
    public string PreferredPalette { get; set; } = "Standard";
}