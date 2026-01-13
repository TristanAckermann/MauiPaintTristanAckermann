using CommunityToolkit.Mvvm.ComponentModel;
using MauiPaintTristanAckermann.Services;

namespace MauiPaintTristanAckermann.ViewModels;

public partial class PresetsViewModel : ObservableObject
{
    private readonly IDrawingService _drawingService;

    [ObservableProperty] private float size;
    [ObservableProperty] private float opacity;

    public PresetsViewModel(IDrawingService drawingService)
    {
        _drawingService = drawingService;
        
        Size = 5;
        Opacity = 1.0f;
    }

    partial void OnSizeChanged(float value) 
    {
        _drawingService.CurrentLineWidth = value;
    }
}