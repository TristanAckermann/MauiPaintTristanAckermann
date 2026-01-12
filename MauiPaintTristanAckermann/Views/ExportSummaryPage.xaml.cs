using MauiPaintTristanAckermann.Models;

namespace MauiPaintTristanAckermann.Views;

[QueryProperty(nameof(Settings), "Settings")]
public partial class ExportSummaryPage : ContentPage
{
    private ExportSettings _settings;
    public ExportSettings Settings 
    { 
        get => _settings; 
        set { _settings = value; OnPropertyChanged(); } 
    }

    public ExportSummaryPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private async void OnBackClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(".."); // Geht eine Seite zurück
    }
}