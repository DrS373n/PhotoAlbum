namespace PhotoAlbum.Core.Models;

/// <summary>
/// Layout template for automatic page design
/// </summary>
public class LayoutTemplate
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public List<DropZone> DropZones { get; set; } = new();
    public BackgroundElement? Background { get; set; }
    public string ThumbnailPath { get; set; } = string.Empty;
    public int PhotoCount { get; set; }
}

/// <summary>
/// Photo filter/effect
/// </summary>
public class PhotoFilter
{
    public string Name { get; set; } = string.Empty;
    public FilterType Type { get; set; }
    public Dictionary<string, object> Parameters { get; set; } = new();
}

public enum FilterType
{
    None,
    BlackAndWhite,
    Sepia,
    Vintage,
    Brightness,
    Contrast,
    Saturation,
    Custom
}

/// <summary>
/// Detected face in a photo
/// </summary>
public class DetectedFace
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public double X { get; set; }
    public double Y { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Confidence { get; set; }
    public string? PersonId { get; set; }
    public string? PersonName { get; set; }
}

/// <summary>
/// Sub-project for multi-book albums
/// </summary>
public class SubProject
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public SubProjectType Type { get; set; }
    public AlbumSettings Settings { get; set; } = new();
    public List<AlbumSpread> Spreads { get; set; } = new();
}

public enum SubProjectType
{
    ParentAlbum,
    GuestBook,
    SigningBook,
    MiniAlbum,
    ThankYouCards
}

/// <summary>
/// Photo metadata for the image well
/// </summary>
public class PhotoMetadata
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FilePath { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public DateTime DateTaken { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public long FileSize { get; set; }
    public string ThumbnailPath { get; set; } = string.Empty;
    public List<DetectedFace> Faces { get; set; } = new();
    public List<string> Tags { get; set; } = new();
    public bool IsUsed { get; set; }
    public int UsageCount { get; set; }
}
