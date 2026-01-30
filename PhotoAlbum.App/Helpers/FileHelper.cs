using Windows.Storage;
using Windows.Storage.Pickers;

namespace PhotoAlbum.App.Helpers;

/// <summary>
/// Helper class for file and folder operations
/// </summary>
public static class FileHelper
{
    /// <summary>
    /// Open a file picker to select image files
    /// </summary>
    public static async Task<IReadOnlyList<StorageFile>> PickImageFilesAsync(nint windowHandle)
    {
        var picker = new FileOpenPicker
        {
            ViewMode = PickerViewMode.Thumbnail,
            SuggestedStartLocation = PickerLocationId.PicturesLibrary
        };

        picker.FileTypeFilter.Add(".jpg");
        picker.FileTypeFilter.Add(".jpeg");
        picker.FileTypeFilter.Add(".png");
        picker.FileTypeFilter.Add(".bmp");
        picker.FileTypeFilter.Add(".tiff");
        picker.FileTypeFilter.Add(".gif");

        // Initialize the picker with the window handle
        WinRT.Interop.InitializeWithWindow.Initialize(picker, windowHandle);

        return await picker.PickMultipleFilesAsync();
    }

    /// <summary>
    /// Open a file picker to save a project file
    /// </summary>
    public static async Task<StorageFile?> PickSaveProjectFileAsync(nint windowHandle, string suggestedFileName)
    {
        var picker = new FileSavePicker
        {
            SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
            SuggestedFileName = suggestedFileName
        };

        picker.FileTypeChoices.Add("PhotoAlbum Project", new List<string> { ".pap" });

        WinRT.Interop.InitializeWithWindow.Initialize(picker, windowHandle);

        return await picker.PickSaveFileAsync();
    }

    /// <summary>
    /// Open a file picker to open a project file
    /// </summary>
    public static async Task<StorageFile?> PickOpenProjectFileAsync(nint windowHandle)
    {
        var picker = new FileOpenPicker
        {
            ViewMode = PickerViewMode.List,
            SuggestedStartLocation = PickerLocationId.DocumentsLibrary
        };

        picker.FileTypeFilter.Add(".pap");

        WinRT.Interop.InitializeWithWindow.Initialize(picker, windowHandle);

        return await picker.PickSingleFileAsync();
    }

    /// <summary>
    /// Open a folder picker to select an export location
    /// </summary>
    public static async Task<StorageFolder?> PickExportFolderAsync(nint windowHandle)
    {
        var picker = new FolderPicker
        {
            SuggestedStartLocation = PickerLocationId.DocumentsLibrary
        };

        picker.FileTypeFilter.Add("*");

        WinRT.Interop.InitializeWithWindow.Initialize(picker, windowHandle);

        return await picker.PickSingleFolderAsync();
    }
}
