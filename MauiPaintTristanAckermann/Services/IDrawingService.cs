using MauiPaintTristanAckermann.Models;

namespace MauiPaintTristanAckermann.Services;

public interface IDrawingService
{
    BrushPreset CurrentBrush { get; set; }
    void ClearCanvas();
}

public class DrawingService : IDrawingService
{
    public BrushPreset CurrentBrush { get; set; } = new BrushPreset { Size = 5, Opacity = 1, TipType = "Round" };
    public void ClearCanvas() { /* Logik zum Zurücksetzen */ }
}