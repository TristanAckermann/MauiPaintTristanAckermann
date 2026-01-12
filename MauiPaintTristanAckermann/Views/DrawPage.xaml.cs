using System;
using System.IO;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using CommunityToolkit.Maui.Views;
using MauiPaintTristanAckermann.Services;

namespace MauiPaintTristanAckermann.Views;

public partial class DrawPage : ContentPage
{
    private DrawingView _activeLayer;
    private readonly IDrawingService _drawingService;

    public DrawPage(IDrawingService drawingService)
    {
        InitializeComponent();
        _drawingService = drawingService;
        _activeLayer = Layer1; // Standard-Layer setzen
        
        // UI auf gespeicherte Werte vom Service setzen
        SizeSlider.Value = _drawingService.CurrentLineWidth;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        try 
        {
            // FIX: Microsoft.Maui.Graphics.Size verwenden
            var exportSize = new Size(800, 600);
            var stream = await _activeLayer.GetImageStream(exportSize, Colors.White);
            
            if (stream != null)
            {
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                
                var imageSource = ImageSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));
                
                // Korrekter Zugriff auf den Klassen-Service
                _drawingService.AddToGallery(imageSource);
                
                await DisplayAlert("Erfolg", "Bild in Galerie gespeichert!", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Fehler", ex.Message, "OK");
        }
    }

    private void OnColorSelected(object sender, EventArgs e) 
    { 
        if (sender is Button btn && _activeLayer != null) 
        {
            _activeLayer.LineColor = btn.BackgroundColor;
            _drawingService.CurrentColor = btn.BackgroundColor; // Im Service merken
        }
    }

    private void OnSliderValueChanged(object sender, ValueChangedEventArgs e) 
    {
        if (_activeLayer != null)
        {
            float val = (float)e.NewValue;
            _activeLayer.LineWidth = val;
            _drawingService.CurrentLineWidth = val; // Im Service merken
        }
    }

    private void OnClearClicked(object sender, EventArgs e) => _activeLayer?.Lines.Clear();

    private void OnAddLayerClicked(object sender, EventArgs e)
    {
        var newLayer = new DrawingView 
        { 
            LineColor = _activeLayer.LineColor, 
            LineWidth = _activeLayer.LineWidth, 
            IsMultiLineModeEnabled = true, 
            InputTransparent = true, 
            BackgroundColor = Colors.Transparent 
        };
        
        LayerContainer.Children.Add(newLayer);
        
        var btn = new ImageButton { Source = "layers.png", WidthRequest = 40, HeightRequest = 40, CommandParameter = newLayer };
        btn.Clicked += (s, ev) => SwitchToLayer((DrawingView)((ImageButton)s).CommandParameter, (ImageButton)s);
        LayerButtonList.Children.Add(btn);
        
        SwitchToLayer(newLayer, btn);
    }

    private void SwitchToLayer(DrawingView target, ImageButton activeBtn)
    {
        foreach (var child in LayerContainer.Children) if (child is DrawingView lv) lv.InputTransparent = true;
        _activeLayer = target; 
        _activeLayer.InputTransparent = false;
    }
}