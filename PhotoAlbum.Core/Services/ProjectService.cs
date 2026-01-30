using PhotoAlbum.Core.Interfaces;
using PhotoAlbum.Core.Models;
using Newtonsoft.Json;

namespace PhotoAlbum.Core.Services;

/// <summary>
/// Service for managing album projects
/// </summary>
public class ProjectService : IProjectService
{
    public async Task<AlbumProject> CreateProjectAsync(string name, string clientName, DateTime weddingDate)
    {
        var project = new AlbumProject
        {
            Name = name,
            ClientName = clientName,
            WeddingDate = weddingDate,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
        
        // Create initial cover spread
        var coverSpread = new AlbumSpread
        {
            SpreadNumber = 0,
            Type = SpreadType.Cover,
            RightPage = new AlbumPage
            {
                PageNumber = 1
            }
        };
        
        project.Spreads.Add(coverSpread);
        
        await Task.CompletedTask;
        return project;
    }

    public async Task<AlbumProject> OpenProjectAsync(string projectPath)
    {
        if (!File.Exists(projectPath))
        {
            throw new FileNotFoundException($"Project file not found: {projectPath}");
        }
        
        var json = await File.ReadAllTextAsync(projectPath);
        var project = JsonConvert.DeserializeObject<AlbumProject>(json);
        
        if (project == null)
        {
            throw new InvalidOperationException("Failed to deserialize project");
        }
        
        project.ProjectPath = projectPath;
        return project;
    }

    public async Task SaveProjectAsync(AlbumProject project)
    {
        project.ModifiedDate = DateTime.Now;
        
        var json = JsonConvert.SerializeObject(project, Formatting.Indented);
        
        if (string.IsNullOrEmpty(project.ProjectPath))
        {
            throw new InvalidOperationException("Project path not set");
        }
        
        var directory = Path.GetDirectoryName(project.ProjectPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        
        await File.WriteAllTextAsync(project.ProjectPath, json);
    }

    public async Task<bool> ExportProjectAsync(AlbumProject project, string outputPath, ExportFormat format)
    {
        // In production, this would render the album to the specified format
        await Task.Delay(100);
        
        // Placeholder - would implement PDF generation, image export, etc.
        return true;
    }
}
