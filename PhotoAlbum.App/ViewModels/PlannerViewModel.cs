using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PhotoAlbum.Core.Models;
using System.Collections.ObjectModel;

namespace PhotoAlbum.App.ViewModels;

public partial class PlannerViewModel : ObservableObject
{
    [ObservableProperty]
    private AlbumProject? _project;
    
    [ObservableProperty]
    private AlbumSpread? _selectedSpread;

    public ObservableCollection<AlbumSpread> Spreads { get; } = new();

    public void LoadProject(AlbumProject project)
    {
        Project = project;
        Spreads.Clear();
        
        foreach (var spread in project.Spreads)
        {
            Spreads.Add(spread);
        }
        
        if (Spreads.Count > 0)
        {
            SelectedSpread = Spreads[0];
        }
    }

    [RelayCommand]
    private void SelectSpread(AlbumSpread spread)
    {
        SelectedSpread = spread;
    }

    [RelayCommand]
    private void MoveSpreadUp(AlbumSpread spread)
    {
        if (Project == null) return;
        
        var index = Project.Spreads.IndexOf(spread);
        if (index > 0)
        {
            Project.Spreads.RemoveAt(index);
            Project.Spreads.Insert(index - 1, spread);
            LoadProject(Project);
        }
    }

    [RelayCommand]
    private void MoveSpreadDown(AlbumSpread spread)
    {
        if (Project == null) return;
        
        var index = Project.Spreads.IndexOf(spread);
        if (index < Project.Spreads.Count - 1)
        {
            Project.Spreads.RemoveAt(index);
            Project.Spreads.Insert(index + 1, spread);
            LoadProject(Project);
        }
    }

    [RelayCommand]
    private void DeleteSpread(AlbumSpread spread)
    {
        if (Project == null) return;
        
        Project.Spreads.Remove(spread);
        LoadProject(Project);
    }
}
