namespace MauiPaintTristanAckermann.Models;

public class BrushPreset
{
    public float Size { get; set; } = 5f;
    public float Opacity { get; set; } = 1.0f;
    public string TipType { get; set; } = "Round"; // Round, Square, Calligraphy
    public Color SelectedColor { get; set; } = Colors.Black;
}