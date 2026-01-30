using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PhotoAlbum.Core.Interfaces;
using PhotoAlbum.Core.Models;
using System.Collections.ObjectModel;

namespace PhotoAlbum.App.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IProjectService _projectService;
    private readonly IPhotoService _photoService;
    
    [ObservableProperty]
    private AlbumProject? _currentProject;
    
    [ObservableProperty]
    private string _statusMessage = "Ready";
    
    [ObservableProperty]
    private bool _isProjectOpen;

    public DesignCanvasViewModel DesignCanvas { get; }
    public ImageWellViewModel ImageWell { get; }
    public PlannerViewModel Planner { get; }

    public MainViewModel(
        IProjectService projectService, 
        IPhotoService photoService,
        DesignCanvasViewModel designCanvas,
        ImageWellViewModel imageWell,
        PlannerViewModel planner)
    {
        _projectService = projectService;
        _photoService = photoService;
        DesignCanvas = designCanvas;
        ImageWell = imageWell;
        Planner = planner;
    }

    [RelayCommand]
    private async Task CreateNewProjectAsync()
    {
        CurrentProject = await _projectService.CreateProjectAsync(
            "New Wedding Album",
            "Client Name",
            DateTime.Today
        );
        
        IsProjectOpen = true;
        StatusMessage = $"Created new project: {CurrentProject.Name}";
        
        // Initialize sub-ViewModels
        DesignCanvas.LoadProject(CurrentProject);
        Planner.LoadProject(CurrentProject);
    }

    [RelayCommand]
    private async Task SaveProjectAsync()
    {
        if (CurrentProject == null) return;
        
        if (string.IsNullOrEmpty(CurrentProject.ProjectPath))
        {
            // Should open save dialog
            CurrentProject.ProjectPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "PhotoAlbum Projects",
                $"{CurrentProject.Name}.pap"
            );
        }
        
        await _projectService.SaveProjectAsync(CurrentProject);
        StatusMessage = "Project saved successfully";
    }

    [RelayCommand]
    private async Task ImportPhotosAsync()
    {
        if (CurrentProject == null) return;
        
        // In real app, would open file picker
        // For now, simulate with sample paths
        var photos = await _photoService.ImportPhotosAsync(Array.Empty<string>());
        
        await ImageWell.AddPhotosAsync(photos);
        StatusMessage = $"Imported {photos.Count} photos";
    }

    [RelayCommand]
    private async Task AddSpreadAsync()
    {
        if (CurrentProject == null) return;
        
        var newSpread = new AlbumSpread
        {
            SpreadNumber = CurrentProject.Spreads.Count,
            Type = SpreadType.Regular,
            LeftPage = new AlbumPage { PageNumber = CurrentProject.Spreads.Count * 2 },
            RightPage = new AlbumPage { PageNumber = CurrentProject.Spreads.Count * 2 + 1 }
        };
        
        CurrentProject.Spreads.Add(newSpread);
        Planner.LoadProject(CurrentProject);
        
        await Task.CompletedTask;
    }
}
