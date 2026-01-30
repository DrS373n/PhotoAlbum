using PhotoAlbum.Core.Interfaces;
using PhotoAlbum.Core.Models;

namespace PhotoAlbum.Core.Services;

/// <summary>
/// Service for layout templates and design management
/// </summary>
public class LayoutService : ILayoutService
{
    private readonly List<LayoutTemplate> _templates = new();

    public LayoutService()
    {
        InitializeDefaultTemplates();
    }

    public async Task<List<LayoutTemplate>> GetTemplatesAsync(string? category = null)
    {
        await Task.CompletedTask;
        
        if (string.IsNullOrEmpty(category))
        {
            return _templates;
        }
        
        return _templates.Where(t => t.Category == category).ToList();
    }

    public async Task<LayoutTemplate> CreateCustomTemplateAsync(string name, List<DropZone> dropZones)
    {
        var template = new LayoutTemplate
        {
            Name = name,
            Category = "Custom",
            DropZones = dropZones,
            PhotoCount = dropZones.Count
        };
        
        _templates.Add(template);
        
        await Task.CompletedTask;
        return template;
    }

    public async Task ApplyTemplateAsync(AlbumPage page, LayoutTemplate template)
    {
        page.Template = template;
        page.DropZones.Clear();
        
        foreach (var zone in template.DropZones)
        {
            page.DropZones.Add(new DropZone
            {
                X = zone.X,
                Y = zone.Y,
                Width = zone.Width,
                Height = zone.Height,
                Shape = zone.Shape,
                ZIndex = zone.ZIndex
            });
        }
        
        if (template.Background != null)
        {
            page.Background = new BackgroundElement
            {
                Color = template.Background.Color,
                ImagePath = template.Background.ImagePath,
                PatternId = template.Background.PatternId,
                Opacity = template.Background.Opacity
            };
        }
        
        await Task.CompletedTask;
    }

    public async Task<List<LayoutTemplate>> GetAutoDesignSuggestionsAsync(List<PhotoMetadata> photos)
    {
        var suggestions = new List<LayoutTemplate>();
        
        // Suggest templates based on photo count
        int photoCount = photos.Count;
        
        var matchingTemplates = _templates
            .Where(t => t.PhotoCount <= photoCount)
            .OrderByDescending(t => t.PhotoCount)
            .Take(5)
            .ToList();
        
        suggestions.AddRange(matchingTemplates);
        
        await Task.CompletedTask;
        return suggestions;
    }

    private void InitializeDefaultTemplates()
    {
        // Single photo - full bleed
        _templates.Add(new LayoutTemplate
        {
            Name = "Full Bleed",
            Category = "Single Photo",
            PhotoCount = 1,
            DropZones = new List<DropZone>
            {
                new() { X = 0, Y = 0, Width = 12, Height = 12, ZIndex = 1 }
            }
        });

        // Two photos - side by side
        _templates.Add(new LayoutTemplate
        {
            Name = "Side by Side",
            Category = "Two Photos",
            PhotoCount = 2,
            DropZones = new List<DropZone>
            {
                new() { X = 0, Y = 0, Width = 5.9, Height = 12, ZIndex = 1 },
                new() { X = 6.1, Y = 0, Width = 5.9, Height = 12, ZIndex = 1 }
            }
        });

        // Three photos - one large, two small
        _templates.Add(new LayoutTemplate
        {
            Name = "Hero with Two",
            Category = "Three Photos",
            PhotoCount = 3,
            DropZones = new List<DropZone>
            {
                new() { X = 0, Y = 0, Width = 8, Height = 12, ZIndex = 1 },
                new() { X = 8.1, Y = 0, Width = 3.9, Height = 5.9, ZIndex = 1 },
                new() { X = 8.1, Y = 6.1, Width = 3.9, Height = 5.9, ZIndex = 1 }
            }
        });

        // Four photos - grid
        _templates.Add(new LayoutTemplate
        {
            Name = "Four Grid",
            Category = "Four Photos",
            PhotoCount = 4,
            DropZones = new List<DropZone>
            {
                new() { X = 0, Y = 0, Width = 5.9, Height = 5.9, ZIndex = 1 },
                new() { X = 6.1, Y = 0, Width = 5.9, Height = 5.9, ZIndex = 1 },
                new() { X = 0, Y = 6.1, Width = 5.9, Height = 5.9, ZIndex = 1 },
                new() { X = 6.1, Y = 6.1, Width = 5.9, Height = 5.9, ZIndex = 1 }
            }
        });

        // Six photos - magazine style
        _templates.Add(new LayoutTemplate
        {
            Name = "Magazine Style",
            Category = "Multiple Photos",
            PhotoCount = 6,
            DropZones = new List<DropZone>
            {
                new() { X = 0, Y = 0, Width = 8, Height = 8, ZIndex = 1 },
                new() { X = 8.1, Y = 0, Width = 3.9, Height = 3.9, ZIndex = 1 },
                new() { X = 8.1, Y = 4.05, Width = 3.9, Height = 3.9, ZIndex = 1 },
                new() { X = 0, Y = 8.1, Width = 3.9, Height = 3.9, ZIndex = 1 },
                new() { X = 4.05, Y = 8.1, Width = 3.9, Height = 3.9, ZIndex = 1 },
                new() { X = 8.1, Y = 8.1, Width = 3.9, Height = 3.9, ZIndex = 1 }
            }
        });
    }
}
