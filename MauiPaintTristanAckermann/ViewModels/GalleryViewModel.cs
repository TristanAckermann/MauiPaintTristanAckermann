using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPaintTristanAckermann.Services;
using MauiPaintTristanAckermann.Models;
using Microsoft.Maui.Controls;
using MauiPaintTristanAckermann.Views;

namespace MauiPaintTristanAckermann.ViewModels;

public partial class GalleryViewModel : ObservableObject
{
    private readonly IDrawingService _drawingService;

    
    public ObservableCollection<GalleryItem> GalleryImages => _drawingService.GalleryImages;

    public string CurrentUserText => $"Galerie von: {_drawingService.CurrentUser}";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsImageSelected))]
    [NotifyCanExecuteChangedFor(nameof(EditCommand))]
    private GalleryItem selectedImage;

    public bool IsImageSelected => SelectedImage != null;

    public GalleryViewModel(IDrawingService drawingService)
    {
        _drawingService = drawingService;
        
        if (_drawingService is DrawingService ds)
        {
             ds.UserChanged += (s, e) => OnPropertyChanged(nameof(CurrentUserText));
        }
    }

    partial void OnSelectedImageChanged(GalleryItem value)
    {
        _drawingService.SelectedImage = value;
    }

    [RelayCommand(CanExecute = nameof(IsImageSelected))]
    private async Task Edit()
    {
        if (SelectedImage == null) return;

        var navParams = new Dictionary<string, object>
        {
            { "LoadImage", SelectedImage }
        };

        await Shell.Current.GoToAsync($"///{nameof(DrawPage)}", navParams);
    }
}