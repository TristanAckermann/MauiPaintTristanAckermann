using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace MauiPaintTristanAckermann.ViewModels;

public partial class FaqItem : ObservableObject
{
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    [ObservableProperty] private bool isExpanded;
}

public partial class FaqViewModel : BaseViewModel
{
    public ObservableCollection<FaqItem> Faqs { get; } = new()
    {
        new FaqItem { Question = "Wie speichere ich?", Answer = "Nutze den Export-Tab." },
        new FaqItem { Question = "Gibt es einen Radierer?", Answer = "Ja, in den Pinsel-Einstellungen." },
        new FaqItem { Question = "Offline nutzbar?", Answer = "Ja, alle Daten liegen lokal." },
        new FaqItem { Question = "Dateiformate?", Answer = "Wir unterstützen PNG und JPG." },
        new FaqItem { Question = "Kosten?", Answer = "Die App ist komplett kostenlos." },
        new FaqItem { Question = "Desktop Support?", Answer = "Ja, Windows wird voll unterstützt." }
    };

    [RelayCommand]
    private void Toggle(FaqItem item) => item.IsExpanded = !item.IsExpanded;
}