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

    public async Task<UserProfile> GetProfile() => 
        File.Exists(ProfilePath) ? JsonConvert.DeserializeObject<UserProfile>(await File.ReadAllTextAsync(ProfilePath)) : new UserProfile();

    public async Task<List<Drawing>> GetGallery() => 
        File.Exists(GalleryPath) ? JsonConvert.DeserializeObject<List<Drawing>>(await File.ReadAllTextAsync(GalleryPath)) : new List<Drawing>();

    public async Task AddToGallery(Drawing drawing)
    {
        var gallery = await GetGallery();
        gallery.Add(drawing);
        await File.WriteAllTextAsync(GalleryPath, JsonConvert.SerializeObject(gallery));
    }
}