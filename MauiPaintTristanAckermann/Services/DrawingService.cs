using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using MauiPaintTristanAckermann.Models;
using SQLite;

namespace MauiPaintTristanAckermann.Services;

public class DrawingService : IDrawingService
{
    private SQLiteAsyncConnection _database;

    public event EventHandler UserChanged;
    public event EventHandler<float> LineWidthChanged;

    public ObservableCollection<GalleryItem> GalleryImages { get; } = new ObservableCollection<GalleryItem>();
    public GalleryItem SelectedImage { get; set; }
    
    public string CurrentUser { get; private set; } = "Gast"; 

    private float _currentLineWidth = 5f;
    public float CurrentLineWidth
    {
        get => _currentLineWidth;
        set
        {
            if (_currentLineWidth != value)
            {
                _currentLineWidth = value;
                LineWidthChanged?.Invoke(this, value);
            }
        }
    }
    
    public Color CurrentColor { get; set; } = Colors.Black;

    public DrawingService()
    {
        // Lazy initialization is safer.
    }

    private async Task InitDatabase()
    {
        if (_database != null) return;

        // New DB name for optimized schema
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "mauipaint_optimized.db3");
        _database = new SQLiteAsyncConnection(dbPath);
        await _database.CreateTableAsync<GalleryItem>();
        
        // Initial load if user is set
        if (!string.IsNullOrEmpty(CurrentUser))
        {
             await RefreshGallery();
        }
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
        if (_database == null) await InitDatabase();

        var items = await _database.Table<GalleryItem>().Where(i => i.OwnerName == CurrentUser).ToListAsync();
        
        MainThread.BeginInvokeOnMainThread(() =>
        {
            GalleryImages.Clear();
            foreach (var item in items)
            {
                if (!string.IsNullOrEmpty(item.FileName))
                {
                    var path = Path.Combine(FileSystem.AppDataDirectory, item.FileName);
                    if (File.Exists(path))
                    {
                        item.Image = ImageSource.FromFile(path);
                    }
                }
                GalleryImages.Add(item);
            }
        });
    }

    public async Task AddToGallery(GalleryItem item)
    {
        if (item != null)
        {
            if (_database == null) await InitDatabase();

            item.OwnerName = CurrentUser;
            
            // Save to file instead of DB Blob
            if (item.ImageData != null)
            {
                var fileName = $"{Guid.NewGuid()}.png";
                var path = Path.Combine(FileSystem.AppDataDirectory, fileName);
                
                await File.WriteAllBytesAsync(path, item.ImageData);
                item.FileName = fileName;
                
                // Clear the heavy byte array from memory as soon as it's on disk
                var data = item.ImageData; // Keep temp ref if needed for logic, but clear model
                item.ImageData = null; 
                
                item.Image = ImageSource.FromFile(path);
            }

            await _database.InsertAsync(item);
            
            MainThread.BeginInvokeOnMainThread(() =>
            {
                GalleryImages.Add(item);
            });
        }
    }

    public void Logout()
    {
        Preferences.Remove("mauipaint_user");
        CurrentUser = "Gast";
        GalleryImages.Clear();
        UserChanged?.Invoke(this, EventArgs.Empty);
    }
}