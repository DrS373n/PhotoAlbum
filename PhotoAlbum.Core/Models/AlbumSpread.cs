namespace PhotoAlbum.Core.Models;

/// <summary>
/// Represents a spread (two facing pages) in the album
/// </summary>
public class AlbumSpread
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int SpreadNumber { get; set; }
    public AlbumPage? LeftPage { get; set; }
    public AlbumPage? RightPage { get; set; }
    public SpreadType Type { get; set; } = SpreadType.Regular;
}

public enum SpreadType
{
    Cover,
    Regular,
    BackCover
}

/// <summary>
/// Represents a single page in the album
/// </summary>
public class AlbumPage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int PageNumber { get; set; }
    public List<DropZone> DropZones { get; set; } = new();
    public List<TextElement> TextElements { get; set; } = new();
    public BackgroundElement? Background { get; set; }
    public LayoutTemplate? Template { get; set; }
}

/// <summary>
/// Drop zone for placing photos
/// </summary>
public class DropZone
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public double X { get; set; }
    public double Y { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Rotation { get; set; }
    public PhotoElement? Photo { get; set; }
    public ZoneShape Shape { get; set; } = ZoneShape.Rectangle;
    public int ZIndex { get; set; }
}

public enum ZoneShape
{
    Rectangle,
    Circle,
    Ellipse,
    Custom
}

/// <summary>
/// Photo element in a drop zone
/// </summary>
public class PhotoElement
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FilePath { get; set; } = string.Empty;
    public double Scale { get; set; } = 1.0;
    public double OffsetX { get; set; }
    public double OffsetY { get; set; }
    public double Rotation { get; set; }
    public PhotoFilter? Filter { get; set; }
    public List<DetectedFace> DetectedFaces { get; set; } = new();
}

/// <summary>
/// Text element on a page
/// </summary>
public class TextElement
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Text { get; set; } = string.Empty;
    public double X { get; set; }
    public double Y { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public string FontFamily { get; set; } = "Arial";
    public double FontSize { get; set; } = 12;
    public string Color { get; set; } = "#000000";
    public TextAlignment Alignment { get; set; } = TextAlignment.Left;
    public bool IsBold { get; set; }
    public bool IsItalic { get; set; }
    public double Rotation { get; set; }
    public string Language { get; set; } = "en-US";
}

public enum TextAlignment
{
    Left,
    Center,
    Right,
    Justify
}

/// <summary>
/// Background element for a page
/// </summary>
public class BackgroundElement
{
    public string? ImagePath { get; set; }
    public string? PatternId { get; set; }
    public string Color { get; set; } = "#FFFFFF";
    public double Opacity { get; set; } = 1.0;
}
