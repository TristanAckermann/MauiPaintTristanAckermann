using CommunityToolkit.Mvvm.ComponentModel;
using MauiPaintTristanAckermann.Models;

namespace MauiPaintTristanAckermann.ViewModels;

[QueryProperty(nameof(User), "User")]
public partial class ProfileSummaryViewModel : BaseViewModel
{
    [ObservableProperty]
    private UserProfile user = new(); 

    public ProfileSummaryViewModel()
    {
        Title = "Zusammenfassung";
    }
}