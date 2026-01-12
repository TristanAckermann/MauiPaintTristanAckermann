using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPaintTristanAckermann.Services;
using MauiPaintTristanAckermann.Models;
using MauiPaintTristanAckermann.Views;

namespace MauiPaintTristanAckermann.ViewModels;

public partial class AccountViewModel : BaseViewModel
{
    private readonly ValidationService _val;

    public AccountViewModel(ValidationService val)
    {
        _val = val;
        Title = "Konto erstellen";
        
        
        email = string.Empty;
        password = string.Empty;
        confirmPassword = string.Empty;
        selectedRegion = string.Empty;
    }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    [NotifyPropertyChangedFor(nameof(IsEmailValid))]
    [NotifyPropertyChangedFor(nameof(EmailError))]
    private string email;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    private string password;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    private string confirmPassword;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    private string selectedRegion;

    public List<string> Regions => new() { "Deutschland", "Schweiz", "Österreich" };

    public bool IsEmailValid => _val.ValidateEmail(Email);
    
    public string EmailError => IsEmailValid ? "" : "Ungültige E-Mail Adresse";

    private bool CanRegister => IsEmailValid && 
                                !string.IsNullOrEmpty(Password) && 
                                Password == ConfirmPassword && 
                                !string.IsNullOrEmpty(SelectedRegion);

    [RelayCommand(CanExecute = nameof(CanRegister))]
    private async Task Register()
    {
        var user = new UserProfile { Email = Email, Region = SelectedRegion };
        var navParam = new Dictionary<string, object> { { "User", user } };
        await Shell.Current.GoToAsync(nameof(ProfileSummaryPage), navParam);
    }
}