using Newtonsoft.Json;
using MauiPaintTristanAckermann.Models;

namespace MauiPaintTristanAckermann.Services;

public interface IStorageService
{
    Task SaveProfile(UserProfile profile);
    Task<UserProfile> GetProfile();
    Task<List<Drawing>> GetGallery();
    Task AddToGallery(Drawing drawing);
}

public class StorageService : IStorageService
{
    private string ProfilePath => Path.Combine(FileSystem.AppDataDirectory, "profile.json");
    private string GalleryPath => Path.Combine(FileSystem.AppDataDirectory, "gallery.json");

    public async Task SaveProfile(UserProfile profile) => 
        await File.WriteAllTextAsync(ProfilePath, JsonConvert.SerializeObject(profile));

    public async Task<UserProfile> GetProfile()
    {
        if (!File.Exists(ProfilePath)) return new UserProfile();
        var json = await File.ReadAllTextAsync(ProfilePath);
        return JsonConvert.DeserializeObject<UserProfile>(json) ?? new UserProfile();
    }

    public async Task<List<Drawing>> GetGallery()
    {
        if (!File.Exists(GalleryPath)) return new List<Drawing>();
        var json = await File.ReadAllTextAsync(GalleryPath);
        return JsonConvert.DeserializeObject<List<Drawing>>(json) ?? new List<Drawing>();
    }

    public async Task AddToGallery(Drawing drawing)
    {
        var gallery = await GetGallery();
        gallery.Add(drawing);
        await File.WriteAllTextAsync(GalleryPath, JsonConvert.SerializeObject(gallery));
    }
}