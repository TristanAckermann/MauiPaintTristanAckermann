using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiPaintTristanAckermann.ViewModels;

public partial class DrawViewModel : BaseViewModel
{
    [ObservableProperty] private float brushSize = 5;
    [ObservableProperty] private Color selectedColor = Colors.Black;
    [ObservableProperty] private bool isEraserMode;

    [RelayCommand]
    private void Clear() { /* Event für View triggern */ }

    [RelayCommand]
    private async Task Save() 
    {
        
        await Shell.Current.DisplayAlert("Gespeichert", "Bild in Galerie abgelegt", "OK");
    }
}