using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPaintTristanAckermann.Models;

namespace MauiPaintTristanAckermann.ViewModels;

[QueryProperty(nameof(Settings), "Settings")]
public partial class ExportSummaryViewModel : BaseViewModel
{
    [ObservableProperty]
    private ExportSettings settings;

    public ExportSummaryViewModel()
    {
        Title = "Export Bestätigung";
    }

    [RelayCommand]
    private async Task ShareImage()
    {
        // Hier könnte später eine Share-Funktion implementiert werden
        await Shell.Current.DisplayAlert("Teilen", "Funktion wird in Kürze hinzugefügt!", "OK");
    }

    [RelayCommand]
    private async Task Close()
    {
        await Shell.Current.GoToAsync("//DrawPage");
    }
}