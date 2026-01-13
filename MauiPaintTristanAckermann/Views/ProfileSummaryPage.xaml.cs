using MauiPaintTristanAckermann.ViewModels;
using MauiPaintTristanAckermann.Models;

namespace MauiPaintTristanAckermann.Views;

[QueryProperty(nameof(User), "User")]
public partial class ProfileSummaryPage : ContentPage
{
    private UserProfile _user;
    public UserProfile User 
    { 
        get => _user; 
        set { _user = value; OnPropertyChanged(); } 
    }

    public ProfileSummaryPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private async void OnGoToDrawClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//DrawPage"); 
    }
}