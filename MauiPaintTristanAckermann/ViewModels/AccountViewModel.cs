using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPaintTristanAckermann.Services;
using MauiPaintTristanAckermann.Models;
using MauiPaintTristanAckermann.Views;
using System.Text.RegularExpressions;

namespace MauiPaintTristanAckermann.ViewModels;

public partial class AccountViewModel : BaseViewModel
{
    private readonly IDrawingService _drawingService;

    public AccountViewModel(IDrawingService drawingService)
    {
        _drawingService = drawingService;
        Title = "Benutzerprofil";
        userName = _drawingService.CurrentUser; // Default to current
    }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    [NotifyPropertyChangedFor(nameof(UserNameError))]
    private string userName;

    public string UserNameError 
    {
        get
        {
            if (string.IsNullOrWhiteSpace(UserName)) return "";
            if (!Regex.IsMatch(UserName, @"^[a-zA-Z\säöüÄÖÜß]+$"))
                return "Nur Buchstaben erlaubt!";
            return "";
        }
    }

    private bool CanLogin => !string.IsNullOrWhiteSpace(UserName) && string.IsNullOrEmpty(UserNameError);

    [RelayCommand(CanExecute = nameof(CanLogin))]
    private async Task Login()
    {
        await _drawingService.SetUser(UserName);
        
        // If we are currently on the Login Page (not already in Shell), switch to Shell
        if (Application.Current.MainPage is not AppShell)
        {
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            // Just switching user inside the app
            await Shell.Current.DisplayAlert("Willkommen", $"Du bist jetzt als {UserName} angemeldet.", "OK");
            await Shell.Current.GoToAsync("///GalleryPage");
        }
    }
}