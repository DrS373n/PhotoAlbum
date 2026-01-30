namespace PhotoAlbum.Core.Models;

/// <summary>
/// Represents a wedding album project
/// </summary>
public class AlbumProject
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public DateTime WeddingDate { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime ModifiedDate { get; set; } = DateTime.Now;
    public AlbumSettings Settings { get; set; } = new();
    public List<AlbumSpread> Spreads { get; set; } = new();
    public List<SubProject> SubProjects { get; set; } = new();
    public string ProjectPath { get; set; } = string.Empty;
}

/// <summary>
/// Album settings including size, orientation, and other properties
/// </summary>
public class AlbumSettings
{
    public double Width { get; set; } = 12.0; // inches
    public double Height { get; set; } = 12.0; // inches
    public PageOrientation Orientation { get; set; } = PageOrientation.Landscape;
    public int PagesPerSpread { get; set; } = 2;
    public double BleedSize { get; set; } = 0.125; // inches
    public double SafeZoneSize { get; set; } = 0.25; // inches
    public bool ShowCutLines { get; set; } = true;
    public bool ShowSafeZones { get; set; } = true;
    public string CoverType { get; set; } = "Standard";
}

public enum PageOrientation
{
    Portrait,
    Landscape,
    Square
}
