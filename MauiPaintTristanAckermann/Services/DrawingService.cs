using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using MauiPaintTristanAckermann.Models;
using SQLite;

namespace MauiPaintTristanAckermann.Services;

public class DrawingService : IDrawingService
{
    private SQLiteAsyncConnection _database;

    public event EventHandler UserChanged;

    public ObservableCollection<GalleryItem> GalleryImages { get; } = new ObservableCollection<GalleryItem>();
    public GalleryItem SelectedImage { get; set; }
    
    public string CurrentUser { get; private set; } = "Gast"; 

    public float CurrentLineWidth { get; set; } = 5f;
    public Color CurrentColor { get; set; } = Colors.Black;

    public DrawingService()
    {
        InitDatabase();
    }

    private async void InitDatabase()
    {
        if (_database != null) return;

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "mauipaint.db3");
        _database = new SQLiteAsyncConnection(dbPath);
        await _database.CreateTableAsync<GalleryItem>();
    }

    public async Task SetUser(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName)) return;
        
        CurrentUser = userName;
        Preferences.Set("mauipaint_user", userName);
        
        await RefreshGallery();
        UserChanged?.Invoke(this, EventArgs.Empty);
    }

    private async Task RefreshGallery()
    {
        if (_database == null) InitDatabase();

        GalleryImages.Clear();
        
        var items = await _database.Table<GalleryItem>().Where(i => i.OwnerName == CurrentUser).ToListAsync();
        
        foreach (var item in items)
        {
            if (item.ImageData != null)
            {
                item.Image = ImageSource.FromStream(() => new MemoryStream(item.ImageData));
            }
            GalleryImages.Add(item);
        }
    }

    public async Task AddToGallery(GalleryItem item)
    {
        if (item != null)
        {
            if (_database == null) InitDatabase();

            item.OwnerName = CurrentUser;
            await _database.InsertAsync(item);
            
            // UI Update (Main Thread)
            if (item.ImageData != null)
            {
                item.Image = ImageSource.FromStream(() => new MemoryStream(item.ImageData));
            }
            GalleryImages.Add(item);
        }
    }
}