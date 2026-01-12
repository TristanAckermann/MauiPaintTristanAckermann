using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPaintTristanAckermann.Models;

namespace MauiPaintTristanAckermann.ViewModels;

public partial class ExportViewModel : BaseViewModel
{
    [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(ExportCommand))] private string fileName = "";
    [ObservableProperty] private string selectedFormat = "PNG";
    [ObservableProperty] private bool useTransparency;
    [ObservableProperty] private DateTime reminder = DateTime.Now;

    public List<string> Formats => new() { "PNG", "JPG" };
    public bool IsPng => SelectedFormat == "PNG";

    private bool CanExport => !string.IsNullOrWhiteSpace(FileName) && FileName.Length > 2;

    [RelayCommand(CanExecute = nameof(CanExport))]
    private async Task Export()
    {
        var settings = new ExportSettings { FileName = FileName, Format = SelectedFormat, UseTransparency = UseTransparency };
        await Shell.Current.GoToAsync(nameof(Views.ExportSummaryPage), new Dictionary<string, object> { { "Settings", settings } });
    }
}