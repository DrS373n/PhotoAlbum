using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PhotoAlbum.Core.Models;
using PhotoAlbum.Core.Interfaces;
using System.Collections.ObjectModel;

namespace PhotoAlbum.App.ViewModels;

public partial class ImageWellViewModel : ObservableObject
{
    private readonly IFaceDetectionService _faceDetectionService;
    
    [ObservableProperty]
    private string _searchText = string.Empty;
    
    [ObservableProperty]
    private string _selectedGroupFilter = "All Photos";
    
    [ObservableProperty]
    private PhotoMetadata? _selectedPhoto;

    public ObservableCollection<PhotoMetadata> AllPhotos { get; } = new();
    public ObservableCollection<PhotoMetadata> FilteredPhotos { get; } = new();
    public ObservableCollection<string> GroupFilters { get; } = new() 
    { 
        "All Photos", 
        "Used", 
        "Unused", 
        "People" 
    };

    public ImageWellViewModel(IFaceDetectionService faceDetectionService)
    {
        _faceDetectionService = faceDetectionService;
    }

    public async Task AddPhotosAsync(List<PhotoMetadata> photos)
    {
        foreach (var photo in photos)
        {
            AllPhotos.Add(photo);
        }
        
        await ApplyFilter();
    }

    [RelayCommand]
    private async Task ApplyFilter()
    {
        FilteredPhotos.Clear();
        
        IEnumerable<PhotoMetadata> filtered = AllPhotos;
        
        // Apply group filter
        filtered = SelectedGroupFilter switch
        {
            "Used" => filtered.Where(p => p.IsUsed),
            "Unused" => filtered.Where(p => !p.IsUsed),
            "People" => filtered.Where(p => p.Faces.Count > 0),
            _ => filtered
        };
        
        // Apply search filter
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            filtered = filtered.Where(p => 
                p.FileName.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                p.Tags.Any(t => t.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
            );
        }
        
        foreach (var photo in filtered)
        {
            FilteredPhotos.Add(photo);
        }
        
        await Task.CompletedTask;
    }

    [RelayCommand]
    private async Task GroupByFaces()
    {
        var groups = await _faceDetectionService.GroupPhotosByFacesAsync(AllPhotos.ToList());
        
        // Would update UI to show grouped view
        await Task.CompletedTask;
    }

    partial void OnSearchTextChanged(string value)
    {
        _ = ApplyFilter();
    }

    partial void OnSelectedGroupFilterChanged(string value)
    {
        _ = ApplyFilter();
    }
}
