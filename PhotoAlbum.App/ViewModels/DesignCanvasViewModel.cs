using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PhotoAlbum.Core.Models;
using PhotoAlbum.Core.Interfaces;
using System.Collections.ObjectModel;

namespace PhotoAlbum.App.ViewModels;

public partial class DesignCanvasViewModel : ObservableObject
{
    private readonly ILayoutService _layoutService;
    
    [ObservableProperty]
    private AlbumProject? _project;
    
    [ObservableProperty]
    private AlbumSpread? _currentSpread;
    
    [ObservableProperty]
    private AlbumPage? _currentPage;
    
    [ObservableProperty]
    private double _zoomLevel = 1.0;
    
    [ObservableProperty]
    private bool _showCutLines = true;
    
    [ObservableProperty]
    private bool _showSafeZones = true;
    
    [ObservableProperty]
    private DropZone? _selectedDropZone;

    public ObservableCollection<LayoutTemplate> AvailableTemplates { get; } = new();

    public DesignCanvasViewModel(ILayoutService layoutService)
    {
        _layoutService = layoutService;
    }

    public void LoadProject(AlbumProject project)
    {
        Project = project;
        
        if (project.Spreads.Count > 0)
        {
            CurrentSpread = project.Spreads[0];
            CurrentPage = CurrentSpread.RightPage;
        }
        
        ShowCutLines = project.Settings.ShowCutLines;
        ShowSafeZones = project.Settings.ShowSafeZones;
        
        LoadTemplates();
    }

    private async void LoadTemplates()
    {
        var templates = await _layoutService.GetTemplatesAsync();
        AvailableTemplates.Clear();
        
        foreach (var template in templates)
        {
            AvailableTemplates.Add(template);
        }
    }

    [RelayCommand]
    private void ZoomIn()
    {
        ZoomLevel = Math.Min(ZoomLevel + 0.1, 3.0);
    }

    [RelayCommand]
    private void ZoomOut()
    {
        ZoomLevel = Math.Max(ZoomLevel - 0.1, 0.1);
    }

    [RelayCommand]
    private void ZoomFit()
    {
        ZoomLevel = 1.0;
    }

    [RelayCommand]
    private async Task ApplyTemplate(LayoutTemplate template)
    {
        if (CurrentPage != null)
        {
            await _layoutService.ApplyTemplateAsync(CurrentPage, template);
            OnPropertyChanged(nameof(CurrentPage));
        }
    }

    [RelayCommand]
    private void AddTextElement()
    {
        if (CurrentPage != null)
        {
            var textElement = new TextElement
            {
                Text = "Double click to edit",
                X = 1,
                Y = 1,
                Width = 3,
                Height = 0.5,
                FontSize = 24
            };
            
            CurrentPage.TextElements.Add(textElement);
            OnPropertyChanged(nameof(CurrentPage));
        }
    }

    [RelayCommand]
    private void NavigateToSpread(AlbumSpread spread)
    {
        CurrentSpread = spread;
        CurrentPage = spread.RightPage ?? spread.LeftPage;
    }

    [RelayCommand]
    private void SelectDropZone(DropZone zone)
    {
        SelectedDropZone = zone;
    }
}
