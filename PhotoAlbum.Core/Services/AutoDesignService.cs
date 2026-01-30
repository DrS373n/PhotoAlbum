using PhotoAlbum.Core.Interfaces;
using PhotoAlbum.Core.Models;

namespace PhotoAlbum.Core.Services;

/// <summary>
/// Auto-design service for automatic album generation
/// </summary>
public class AutoDesignService : IAutoDesignService
{
    private readonly ILayoutService _layoutService;

    public AutoDesignService(ILayoutService layoutService)
    {
        _layoutService = layoutService;
    }

    public async Task<List<AlbumSpread>> AutoGenerateAlbumAsync(List<PhotoMetadata> photos, AlbumSettings settings)
    {
        var spreads = new List<AlbumSpread>();
        var templates = await _layoutService.GetTemplatesAsync();
        var photoQueue = new Queue<PhotoMetadata>(photos);
        
        int spreadNumber = 1;
        
        while (photoQueue.Count > 0)
        {
            var spread = new AlbumSpread
            {
                SpreadNumber = spreadNumber++,
                Type = SpreadType.Regular,
                LeftPage = new AlbumPage { PageNumber = (spreadNumber - 1) * 2 },
                RightPage = new AlbumPage { PageNumber = (spreadNumber - 1) * 2 + 1 }
            };
            
            // Fill left page
            if (photoQueue.Count > 0 && spread.LeftPage != null)
            {
                await FillPageWithPhotos(spread.LeftPage, photoQueue, templates);
            }
            
            // Fill right page
            if (photoQueue.Count > 0 && spread.RightPage != null)
            {
                await FillPageWithPhotos(spread.RightPage, photoQueue, templates);
            }
            
            spreads.Add(spread);
        }
        
        return spreads;
    }

    public async Task<AlbumPage> AutoFillPageAsync(AlbumPage page, List<PhotoMetadata> availablePhotos)
    {
        if (page.DropZones.Count == 0)
        {
            // No drop zones - apply a template
            var templates = await _layoutService.GetTemplatesAsync();
            var template = templates.FirstOrDefault(t => t.PhotoCount <= availablePhotos.Count);
            
            if (template != null)
            {
                await _layoutService.ApplyTemplateAsync(page, template);
            }
        }
        
        // Fill drop zones with photos
        int photoIndex = 0;
        foreach (var zone in page.DropZones)
        {
            if (photoIndex < availablePhotos.Count && zone.Photo == null)
            {
                var photo = availablePhotos[photoIndex++];
                zone.Photo = new PhotoElement
                {
                    FilePath = photo.FilePath,
                    DetectedFaces = photo.Faces
                };
            }
        }
        
        return page;
    }

    public async Task OptimizePhotoPlacementAsync(AlbumSpread spread)
    {
        // Optimize photo placement based on faces, composition, etc.
        await Task.CompletedTask;
        
        // This would implement smart algorithms to:
        // 1. Ensure faces aren't cut off at spread gutter
        // 2. Balance visual weight across the spread
        // 3. Group related photos together
        // 4. Vary photo sizes for visual interest
    }

    private async Task FillPageWithPhotos(AlbumPage page, Queue<PhotoMetadata> photoQueue, List<LayoutTemplate> templates)
    {
        // Select appropriate template based on remaining photos
        int remainingPhotos = photoQueue.Count;
        var suitableTemplates = templates
            .Where(t => t.PhotoCount <= remainingPhotos && t.PhotoCount <= 6)
            .OrderByDescending(t => t.PhotoCount)
            .ToList();
        
        var selectedTemplate = suitableTemplates.FirstOrDefault() 
            ?? templates.First(t => t.PhotoCount == 1);
        
        await _layoutService.ApplyTemplateAsync(page, selectedTemplate);
        
        // Fill drop zones with photos
        foreach (var zone in page.DropZones)
        {
            if (photoQueue.Count > 0)
            {
                var photo = photoQueue.Dequeue();
                zone.Photo = new PhotoElement
                {
                    FilePath = photo.FilePath,
                    DetectedFaces = photo.Faces
                };
                photo.IsUsed = true;
                photo.UsageCount++;
            }
        }
    }
}
