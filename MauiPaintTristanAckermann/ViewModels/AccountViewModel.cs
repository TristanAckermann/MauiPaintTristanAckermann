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
    private readonly IServiceProvider _serviceProvider;

    public AccountViewModel(IDrawingService drawingService, IServiceProvider serviceProvider)
    {
        _drawingService = drawingService;
        _serviceProvider = serviceProvider;
        Title = "Benutzerprofil";
        userName = _drawingService.CurrentUser; 
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
            if (!Regex.IsMatch(UserName, @"^[a-zA-Z\säöüÄÖÜ]+$"))
                return "Nur Buchstaben erlaubt!";
            return "";
        }
    }

    private bool CanLogin => !string.IsNullOrWhiteSpace(UserName) && string.IsNullOrEmpty(UserNameError);

    [RelayCommand(CanExecute = nameof(CanLogin))]
    private async Task Login()
    {
        await _drawingService.SetUser(UserName);
        
        
        if (Application.Current.MainPage is not AppShell)
        {
            Application.Current.MainPage = new AppShell();
        }
        else
        {
            
            await Shell.Current.DisplayAlert("Willkommen", $"Du bist jetzt als {UserName} angemeldet.", "OK");
            await Shell.Current.GoToAsync("///GalleryPage");
        }
    }

    [RelayCommand]
    private void Logout()
    {
        _drawingService.Logout();
        
        
        Application.Current.MainPage = _serviceProvider.GetRequiredService<LoginPage>();
    }
}