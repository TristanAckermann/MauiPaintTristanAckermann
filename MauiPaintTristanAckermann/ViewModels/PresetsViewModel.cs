using CommunityToolkit.Mvvm.ComponentModel;
using MauiPaintTristanAckermann.Models;
using MauiPaintTristanAckermann.Services;

namespace MauiPaintTristanAckermann.ViewModels;

public partial class PresetsViewModel : BaseViewModel
{
    private readonly IDrawingService _drawingService;

    [ObservableProperty] private float size;
    [ObservableProperty] private float opacity;
    [ObservableProperty] private string selectedTip;

    public List<string> TipTypes { get; } = new() { "Round", "Square", "Calligraphy" };

    public PresetsViewModel(IDrawingService drawingService)
    {
        _drawingService = drawingService;
        Title = "Pinsel-Einstellungen";
        
        // Initialwerte vom Service laden
        Size = _drawingService.CurrentBrush.Size;
        Opacity = _drawingService.CurrentBrush.Opacity;
        SelectedTip = _drawingService.CurrentBrush.TipType;
    }

    // Wenn sich ein Wert im UI ändert, wird er hier direkt in den Service geschrieben
    partial void OnSizeChanged(float value) => _drawingService.CurrentBrush.Size = value;
    partial void OnOpacityChanged(float value) => _drawingService.CurrentBrush.Opacity = value;
    partial void OnSelectedTipChanged(string value) => _drawingService.CurrentBrush.TipType = value;
}