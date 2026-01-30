# PhotoAlbum Installers Directory

This directory is used to store downloaded prerequisite installers.

## Auto-Downloaded Files

When you run `Install-Prerequisites.ps1`, the following files will be downloaded here:

- `dotnet-sdk-9.0-win-x64.exe` - .NET 9.0 SDK for 64-bit Windows
- `dotnet-sdk-9.0-win-x86.exe` - .NET 9.0 SDK for 32-bit Windows  
- `dotnet-sdk-9.0-win-arm64.exe` - .NET 9.0 SDK for ARM64 Windows

## Manual Download

If you prefer to download installers manually, place them in this directory:

### .NET 9.0 SDK

Download from: https://dotnet.microsoft.com/download/dotnet/9.0

Select the appropriate installer for your architecture:
- **x64** (most common): Windows x64 Installer
- **x86** (32-bit): Windows x86 Installer
- **ARM64**: Windows ARM64 Installer

### Windows App SDK Runtime

Download from: https://learn.microsoft.com/windows/apps/windows-app-sdk/downloads

Version required: 1.6 or later

Choose the appropriate architecture package:
- **x64**: For 64-bit Windows
- **x86**: For 32-bit Windows
- **ARM64**: For ARM64 Windows

## Notes

- These installer files are NOT committed to git (see `.gitignore`)
- The files will be automatically downloaded when needed by the install script
- You can manually run installers from this directory if the script fails
- Total download size is approximately 200-300 MB

## Cleanup

To remove downloaded installers and free up space:

```powershell
Remove-Item *.exe, *.msi, *.msix -Force
```

Or simply delete the files through Windows Explorer.
