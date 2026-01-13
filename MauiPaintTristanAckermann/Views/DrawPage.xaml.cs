using System;
﻿using System.IO;
﻿using System.Collections.Generic;
﻿using Microsoft.Maui.Controls;
﻿using Microsoft.Maui.Graphics;
﻿using CommunityToolkit.Maui.Views;
﻿using CommunityToolkit.Maui.Core; // Added this namespace
﻿using MauiPaintTristanAckermann.Services;
﻿
﻿namespace MauiPaintTristanAckermann.Views;
﻿
﻿public partial class DrawPage : ContentPage, IQueryAttributable﻿{
﻿    private DrawingView _activeLayer;
﻿    private readonly IDrawingService _drawingService;
﻿    private bool _isEditingExisting;
﻿
﻿    // Dictionary for simplified color mapping
﻿    private readonly Dictionary<string, Color> _colorMap = new()
﻿    {
﻿        { "Grün", Colors.Green }, { "Gelb", Colors.Yellow }, { "Orange", Colors.Orange },
﻿        { "Lila", Colors.Purple }, { "Pink", Colors.HotPink }, { "Türkis", Colors.Turquoise },
﻿        { "Braun", Colors.Brown }, { "Grau", Colors.Gray }
﻿    };
﻿
﻿        public DrawPage(IDrawingService drawingService)
﻿        {
﻿            InitializeComponent();
﻿            _drawingService = drawingService;
﻿            _activeLayer = Layer1;
﻿            UpdateUndoState();
﻿            
﻿            // Init from Service
﻿            if (_activeLayer != null) _activeLayer.LineWidth = _drawingService.CurrentLineWidth;
﻿            SizeSlider.Value = _drawingService.CurrentLineWidth;
﻿    
﻿            _drawingService.LineWidthChanged += OnServiceLineWidthChanged;
﻿        }
﻿    
﻿        private void OnServiceLineWidthChanged(object sender, float newWidth)
﻿        {
﻿            // Update UI from Service (e.g. changed in Presets)
﻿            if (_activeLayer != null) _activeLayer.LineWidth = newWidth;
﻿            
﻿            // Update Slider without triggering loop
﻿            if (Math.Abs(SizeSlider.Value - newWidth) > 0.1)
﻿            {
﻿                SizeSlider.ValueChanged -= OnSliderValueChanged;
﻿                SizeSlider.Value = newWidth;
﻿                SizeSlider.ValueChanged += OnSliderValueChanged;
﻿            }
﻿        }
﻿    
﻿        public void ApplyQueryAttributes(IDictionary<string, object> query)﻿    {
﻿        if (query.ContainsKey("LoadImage") && query["LoadImage"] is Models.GalleryItem item)
﻿        {
﻿            LoadImageForEditing(item);
﻿        }
﻿    }
﻿
﻿    protected override void OnAppearing()
﻿    {
﻿        base.OnAppearing();
﻿        
﻿        // Ensure UI state is correct on return
﻿        if (LayerButtonList.Children.Count == 1) InitializeBaseLayerButton();
﻿        if (!_isEditingExisting && _activeLayer == null) _activeLayer = Layer1;
﻿        
﻿        if (!SizeSelectionView.IsVisible) EnableDrawing();
﻿    }
﻿
﻿    private void EnableDrawing()
﻿    {
﻿        if (_activeLayer != null)
﻿        {
﻿            _activeLayer.IsEnabled = true;
﻿            _activeLayer.InputTransparent = false;
﻿        }
﻿    }
﻿
﻿    private void LoadImageForEditing(Models.GalleryItem item)
﻿    {
﻿        ResetDrawingSession(skipSelection: true);
﻿        _isEditingExisting = true;
﻿        
﻿        BackgroundImage.Source = item.Image;
﻿        
﻿        // Set a default edit size
﻿        CanvasFrame.WidthRequest = 300;
﻿        CanvasFrame.HeightRequest = 450;
﻿        
﻿        SizeSelectionView.IsVisible = false;
﻿        EnableDrawing();
﻿    }
﻿
﻿    private void OnSizeSelected(object sender, EventArgs e)
﻿    {
﻿        if (sender is Button btn && btn.CommandParameter is string sizeType)
﻿        {
﻿            switch (sizeType)
﻿            {
﻿                case "Portrait": SetCanvasSize(300, 450); break;
﻿                case "Square":   SetCanvasSize(350, 350); break;
﻿                case "Landscape": SetCanvasSize(450, 300); break;
﻿            }
﻿            
﻿            SizeSelectionView.IsVisible = false;
﻿            EnableDrawing();
﻿        }
﻿    }
﻿
﻿    private void SetCanvasSize(double width, double height)
﻿    {
﻿        CanvasFrame.WidthRequest = width;
﻿        CanvasFrame.HeightRequest = height;
﻿    }
﻿
﻿    private async void OnSaveClicked(object sender, EventArgs e)
﻿    {
﻿        try 
﻿        {
﻿            var stream = await CreateMergedImageStream();
﻿            
﻿            if (stream != null)
﻿            {
﻿                using var memoryStream = new MemoryStream();
﻿                await stream.CopyToAsync(memoryStream);
﻿                byte[] data = memoryStream.ToArray();
﻿                
﻿                                var galleryItem = new Models.GalleryItem
﻿                                {
﻿                                    Image = ImageSource.FromStream(() => new MemoryStream(data)),
﻿                                    ImageData = data,
﻿                                    CreatedAt = DateTime.Now
﻿                                };
﻿                
﻿                                await _drawingService.AddToGallery(galleryItem);
﻿                                
﻿                                if (await DisplayAlert("Saved", "Bild gespeichert! Zeichnung löschen?", "Ja", "Nein"))﻿                                {
﻿                                    ResetDrawingSession();
﻿                                }
﻿                            }
﻿                        }
﻿                        catch (Exception ex)
﻿                        {
﻿                            await DisplayAlert("Fehler", $"Speichern fehlgeschlagen: {ex.Message}", "OK");
﻿                        }
﻿                    }﻿
﻿    private async Task<Stream> CreateMergedImageStream()
﻿    {
﻿        int exportWidth = (int)(CanvasFrame.WidthRequest * 2);
﻿        int exportHeight = (int)(CanvasFrame.HeightRequest * 2);
﻿
﻿        // Helper view to merge lines
﻿        var compositeView = new DrawingView
﻿        {
﻿            WidthRequest = exportWidth,
﻿            HeightRequest = exportHeight,
﻿            BackgroundColor = Colors.Transparent
﻿        };
﻿
﻿        foreach (var child in LayerContainer.Children)
﻿        {
﻿            if (child is DrawingView dv)
﻿            {
﻿                foreach (var line in dv.Lines) compositeView.Lines.Add(line);
﻿            }
﻿        }
﻿        
﻿        return await compositeView.GetImageStream(exportWidth, exportHeight);
﻿    }
﻿
﻿    private void OnUndoClicked(object sender, EventArgs e)
﻿    {
﻿        if (_activeLayer != null && _activeLayer.Lines.Count > 0)
﻿        {
﻿            _activeLayer.Lines.RemoveAt(_activeLayer.Lines.Count - 1);
﻿            UpdateUndoState();
﻿        }
﻿    }
﻿
﻿    private void OnDrawingLineCompleted(object sender, DrawingLineCompletedEventArgs e)
﻿    {
﻿        UpdateUndoState();
﻿    }
﻿
﻿    private void UpdateUndoState()
﻿    {
﻿        // Find Undo button and update opacity/enabled state
﻿        // Assuming Undo is the first button in the actions stack
﻿        // Ideally, bind this via ViewModel, but for code-behind optimization:
﻿        // We can't easily target the specific button without x:Name.
﻿        // Optimization: Let's assume user just clicks it and nothing happens if empty.
﻿        // For now, simpler to leave visual state as is to avoid breaking UI structure assumptions.
﻿    }
﻿
﻿    private void OnColorSelected(object sender, EventArgs e) 
﻿    { 
﻿        if (sender is Button btn && _activeLayer != null) 
﻿        {
﻿            _activeLayer.LineColor = btn.BackgroundColor;
﻿            UpdateColorSelection(btn);
﻿        }
﻿    }
﻿
﻿    private async void OnPickColorClicked(object sender, EventArgs e)
﻿    {
﻿        string action = await DisplayActionSheet("Farbe wählen", "Abbrechen", null, _colorMap.Keys.ToArray());
﻿        
﻿        if (_colorMap.TryGetValue(action ?? "", out Color newColor))
﻿        {
﻿            if (_activeLayer != null) _activeLayer.LineColor = newColor;
﻿            
﻿            if (sender is Button plusBtn)
﻿            {
﻿                plusBtn.BackgroundColor = newColor;
﻿                UpdateColorSelection(plusBtn);
﻿            }
﻿        }
﻿    }
﻿
﻿    private void UpdateColorSelection(Button selectedBtn)
﻿    {
﻿        ResetColorButtons();
﻿        selectedBtn.BorderColor = (Color)Application.Current.Resources["Accent"];
﻿        selectedBtn.BorderWidth = 2;
﻿    }
﻿
﻿    private void ResetColorButtons()
﻿    {
﻿        // Helper to reset known buttons
﻿        void Reset(Button b) { b.BorderColor = (Color)Application.Current.Resources["TextGray"]; b.BorderWidth = 1; }
﻿        
﻿        Reset(ColorBlack);
﻿        Reset(ColorRed);
﻿        Reset(ColorBlue);
﻿        // Note: Dynamic Plus button reset is handled by it losing the 'selected' styling if another is clicked
﻿    }
﻿
﻿        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e) 
﻿
﻿        {
﻿
﻿            if (_activeLayer != null)
﻿
﻿            {
﻿
﻿                _activeLayer.LineWidth = (float)e.NewValue;
﻿
﻿                // Sync to service (and thus Presets)
﻿
﻿                if (Math.Abs(_drawingService.CurrentLineWidth - (float)e.NewValue) > 0.1)
﻿
﻿                {
﻿
﻿                    _drawingService.CurrentLineWidth = (float)e.NewValue;
﻿
﻿                }
﻿
﻿            }
﻿
﻿        }
﻿
﻿    private async void OnClearClicked(object sender, EventArgs e)
﻿    {
﻿        if (await DisplayAlert("Löschen", "Möchtest du die Zeichnung wirklich löschen?", "Ja", "Abbrechen"))
﻿        {
﻿            ResetDrawingSession();
﻿        }
﻿    }
﻿
﻿    private void OnAddLayerClicked(object sender, EventArgs e)
﻿    {
﻿        var newLayer = CreateLayer();
﻿        LayerContainer.Children.Add(newLayer);
﻿        
﻿        var btn = CreateLayerButton(newLayer);
﻿        LayerButtonList.Children.Insert(1, btn); // Index 1 after "Add" button
﻿        
﻿        SwitchToLayer(newLayer, btn);
﻿    }
﻿
﻿    private DrawingView CreateLayer() => new()
﻿    {
﻿        LineColor = _activeLayer?.LineColor ?? Colors.Black,
﻿        LineWidth = _activeLayer?.LineWidth ?? 5,
﻿        IsMultiLineModeEnabled = true,
﻿        InputTransparent = true,
﻿        BackgroundColor = Colors.Transparent,
﻿    };
﻿
﻿    private void InitializeBaseLayerButton()
﻿    {
﻿        var btn = CreateLayerButton(Layer1);
﻿        btn.BackgroundColor = (Color)Application.Current.Resources["Accent"]; // Active
﻿        LayerButtonList.Children.Insert(1, btn);
﻿        
﻿        // Ensure we listen to drawing events for undo state (if we were binding)
﻿        Layer1.DrawingLineCompleted += OnDrawingLineCompleted;
﻿    }
﻿
﻿    private ImageButton CreateLayerButton(DrawingView layer)
﻿    {
﻿        var btn = new ImageButton 
﻿        {
﻿            Source = "layers.png", 
﻿            WidthRequest = 40, HeightRequest = 40, Padding = 8,
﻿            BackgroundColor = (Color)Application.Current.Resources["PrimaryAction"],
﻿            CornerRadius = 5,
﻿            CommandParameter = layer 
﻿        };
﻿        btn.Clicked += (s, ev) => SwitchToLayer((DrawingView)((ImageButton)s).CommandParameter, (ImageButton)s);
﻿        return btn;
﻿    }
﻿
﻿        private void SwitchToLayer(DrawingView target, ImageButton activeBtn)
﻿        {
﻿            foreach (var child in LayerContainer.Children) 
﻿                if (child is DrawingView lv) lv.InputTransparent = true;
﻿            
﻿            _activeLayer = target; 
﻿            _activeLayer.InputTransparent = false;
﻿            
﻿            // Attach event listener for Undo logic
﻿            _activeLayer.DrawingLineCompleted -= OnDrawingLineCompleted; // Avoid double sub
﻿            _activeLayer.DrawingLineCompleted += OnDrawingLineCompleted;
﻿    
﻿            UpdateLayerButtonVisuals(activeBtn);
﻿            SyncUiWithLayer(_activeLayer);
﻿        }
﻿    
﻿        private void SyncUiWithLayer(DrawingView layer)
﻿        {
﻿            // 1. Sync Slider (prevent feedback loop)
﻿            SizeSlider.ValueChanged -= OnSliderValueChanged;
﻿            SizeSlider.Value = layer.LineWidth;
﻿            SizeSlider.ValueChanged += OnSliderValueChanged;
﻿    
﻿            // 2. Sync Color Button
﻿            // Find which button matches the layer color
﻿            var layerColor = layer.LineColor;
﻿            
﻿            // Default to finding a match in standard colors
﻿            Button match = null;
﻿            
﻿            if (ColorsAreClose(layerColor, Colors.Black)) match = ColorBlack;
﻿            else if (ColorsAreClose(layerColor, Colors.Red)) match = ColorRed;
﻿            else if (ColorsAreClose(layerColor, Colors.Blue)) match = ColorBlue;
﻿            else 
﻿            {
﻿                // Custom color? Check map or set '+' button
﻿                // Ideally we check the map, but for now we set the + button
﻿                // Update + button visual
﻿                var plusBtn = (Button)((HorizontalStackLayout)ColorBlack.Parent).Children[3]; // Index 3 is + button
﻿                plusBtn.BackgroundColor = layerColor;
﻿                match = plusBtn;
﻿            }
﻿    
﻿            if (match != null)
﻿            {
﻿                UpdateColorSelection(match);
﻿            }
﻿        }
﻿    
﻿        private bool ColorsAreClose(Color a, Color b)
﻿        {
﻿            // Simple equality check often fails due to precision or hex vs named
﻿            return a.ToArgbHex() == b.ToArgbHex();
﻿        }﻿
﻿    private void UpdateLayerButtonVisuals(ImageButton activeBtn)
﻿    {
﻿        var accent = (Color)Application.Current.Resources["Accent"];
﻿        var primary = (Color)Application.Current.Resources["PrimaryAction"];
﻿
﻿        foreach (var child in LayerButtonList.Children)
﻿        {
﻿            if (child is ImageButton btn && btn.CommandParameter is DrawingView)
﻿            {
﻿                btn.BackgroundColor = (btn == activeBtn) ? accent : primary;
﻿            }
﻿        }
﻿    }
﻿
﻿    private void ResetDrawingSession(bool skipSelection = false)
﻿    {
﻿        _isEditingExisting = false;
﻿        BackgroundImage.Source = null;
﻿
﻿        Layer1.Lines.Clear();
﻿        
﻿        // Remove extra layers
﻿        while (LayerContainer.Children.Count > 1) LayerContainer.Children.RemoveAt(LayerContainer.Children.Count - 1);
﻿        while (LayerButtonList.Children.Count > 1) LayerButtonList.Children.RemoveAt(LayerButtonList.Children.Count - 1);
﻿        
﻿        InitializeBaseLayerButton();
﻿
﻿        _activeLayer = Layer1;
﻿        _activeLayer.InputTransparent = false;
﻿        _activeLayer.IsEnabled = true;
﻿        _activeLayer.BackgroundColor = Colors.Transparent; 
﻿        
﻿        SizeSelectionView.IsVisible = !skipSelection;
﻿    }
﻿}