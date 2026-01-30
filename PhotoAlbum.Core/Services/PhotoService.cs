using PhotoAlbum.Core.Interfaces;
using PhotoAlbum.Core.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace PhotoAlbum.Core.Services;

/// <summary>
/// Service for photo management and processing
/// </summary>
public class PhotoService : IPhotoService
{
    private readonly IFaceDetectionService _faceDetectionService;

    public PhotoService(IFaceDetectionService faceDetectionService)
    {
        _faceDetectionService = faceDetectionService;
    }

    public async Task<List<PhotoMetadata>> ImportPhotosAsync(string[] paths)
    {
        var photos = new List<PhotoMetadata>();
        
        foreach (var path in paths)
        {
            if (File.Exists(path))
            {
                var metadata = await AnalyzePhotoAsync(path);
                photos.Add(metadata);
            }
        }
        
        return photos;
    }

    public async Task<string> GenerateThumbnailAsync(string imagePath, int maxWidth, int maxHeight)
    {
        using var image = await Image.LoadAsync(imagePath);
        
        image.Mutate(x => x.Resize(new ResizeOptions
        {
            Size = new Size(maxWidth, maxHeight),
            Mode = ResizeMode.Max
        }));
        
        var thumbnailPath = Path.Combine(
            Path.GetTempPath(),
            "PhotoAlbumThumbnails",
            $"{Guid.NewGuid()}.jpg"
        );
        
        var directory = Path.GetDirectoryName(thumbnailPath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
        
        await image.SaveAsJpegAsync(thumbnailPath, new JpegEncoder { Quality = 80 });
        
        return thumbnailPath;
    }

    public async Task<byte[]> ApplyFilterAsync(string imagePath, PhotoFilter filter)
    {
        using var image = await Image.LoadAsync(imagePath);
        
        switch (filter.Type)
        {
            case FilterType.BlackAndWhite:
                image.Mutate(x => x.Grayscale());
                break;
            case FilterType.Sepia:
                image.Mutate(x => x.Sepia());
                break;
            case FilterType.Brightness:
                if (filter.Parameters.TryGetValue("amount", out var brightness))
                {
                    image.Mutate(x => x.Brightness(Convert.ToSingle(brightness)));
                }
                break;
            case FilterType.Contrast:
                if (filter.Parameters.TryGetValue("amount", out var contrast))
                {
                    image.Mutate(x => x.Contrast(Convert.ToSingle(contrast)));
                }
                break;
            case FilterType.Saturation:
                if (filter.Parameters.TryGetValue("amount", out var saturation))
                {
                    image.Mutate(x => x.Saturate(Convert.ToSingle(saturation)));
                }
                break;
        }
        
        using var ms = new MemoryStream();
        await image.SaveAsJpegAsync(ms);
        return ms.ToArray();
    }

    public async Task<PhotoMetadata> AnalyzePhotoAsync(string imagePath)
    {
        var fileInfo = new FileInfo(imagePath);
        
        using var image = await Image.LoadAsync(imagePath);
        
        var metadata = new PhotoMetadata
        {
            FilePath = imagePath,
            FileName = fileInfo.Name,
            Width = image.Width,
            Height = image.Height,
            FileSize = fileInfo.Length,
            DateTaken = fileInfo.CreationTime
        };
        
        // Generate thumbnail
        metadata.ThumbnailPath = await GenerateThumbnailAsync(imagePath, 200, 200);
        
        // Detect faces
        metadata.Faces = await _faceDetectionService.DetectFacesAsync(imagePath);
        
        return metadata;
    }
}
