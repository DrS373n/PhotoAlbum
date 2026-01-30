using PhotoAlbum.Core.Models;

namespace PhotoAlbum.Core.Interfaces;

/// <summary>
/// Service for AI-powered face detection
/// </summary>
public interface IFaceDetectionService
{
    Task<List<DetectedFace>> DetectFacesAsync(string imagePath);
    Task<Dictionary<string, List<PhotoMetadata>>> GroupPhotosByFacesAsync(List<PhotoMetadata> photos);
    Task<string?> IdentifyPersonAsync(DetectedFace face, string imagePath);
}

/// <summary>
/// Service for project management
/// </summary>
public interface IProjectService
{
    Task<AlbumProject> CreateProjectAsync(string name, string clientName, DateTime weddingDate);
    Task<AlbumProject> OpenProjectAsync(string projectPath);
    Task SaveProjectAsync(AlbumProject project);
    Task<bool> ExportProjectAsync(AlbumProject project, string outputPath, ExportFormat format);
}

public enum ExportFormat
{
    PDF,
    JPG,
    PNG,
    TIFF
}

/// <summary>
/// Service for layout and template management
/// </summary>
public interface ILayoutService
{
    Task<List<LayoutTemplate>> GetTemplatesAsync(string? category = null);
    Task<LayoutTemplate> CreateCustomTemplateAsync(string name, List<DropZone> dropZones);
    Task ApplyTemplateAsync(AlbumPage page, LayoutTemplate template);
    Task<List<LayoutTemplate>> GetAutoDesignSuggestionsAsync(List<PhotoMetadata> photos);
}

/// <summary>
/// Service for photo management and processing
/// </summary>
public interface IPhotoService
{
    Task<List<PhotoMetadata>> ImportPhotosAsync(string[] paths);
    Task<string> GenerateThumbnailAsync(string imagePath, int maxWidth, int maxHeight);
    Task<byte[]> ApplyFilterAsync(string imagePath, PhotoFilter filter);
    Task<PhotoMetadata> AnalyzePhotoAsync(string imagePath);
}

/// <summary>
/// Service for auto-design features
/// </summary>
public interface IAutoDesignService
{
    Task<List<AlbumSpread>> AutoGenerateAlbumAsync(List<PhotoMetadata> photos, AlbumSettings settings);
    Task<AlbumPage> AutoFillPageAsync(AlbumPage page, List<PhotoMetadata> availablePhotos);
    Task OptimizePhotoPlacementAsync(AlbumSpread spread);
}
