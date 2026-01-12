using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPaintTristanAckermann.Models;
using MauiPaintTristanAckermann.Services;
using System.Collections.ObjectModel;

namespace MauiPaintTristanAckermann.ViewModels;

public partial class GalleryViewModel : BaseViewModel
{
    private readonly IStorageService _storageService;

    [ObservableProperty]
    private ObservableCollection<Drawing> drawings = new();

    public GalleryViewModel(IStorageService storageService)
    {
        _storageService = storageService;
        Title = "Meine Galerie";
    }

    [RelayCommand]
    public async Task LoadGallery()
    {
        IsBusy = true;
        var list = await _storageService.GetGallery();
        Drawings = new ObservableCollection<Drawing>(list);
        IsBusy = false;
    }

    [RelayCommand]
    private async Task Delete(Drawing drawing)
    {
        if (drawing == null) return;
        
        bool confirm = await Shell.Current.DisplayAlert("Löschen", "Möchtest du dieses Bild wirklich löschen?", "Ja", "Nein");
        if (confirm)
        {
            Drawings.Remove(drawing);
            // Hier müsste im StorageService noch eine Delete-Methode aufgerufen werden
        }
    }
}