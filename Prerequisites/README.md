# PhotoAlbum Prerequisites

This directory contains scripts and tools to help you install all the prerequisites needed to build and run PhotoAlbum.

## Prerequisites Overview

PhotoAlbum requires the following components:

1. **.NET 9.0 SDK** - Required to build and run the application
2. **Windows App SDK** - Required for WinUI3 development (bundled with Visual Studio workload)
3. **Visual Studio 2022** (Optional but recommended for development)
   - Windows App SDK workload
   - .NET desktop development workload

## Quick Installation

### For End Users (Running the Application)

Run the following command in PowerShell (as Administrator):

```powershell
.\Install-Prerequisites.ps1
```

This will:
- Check if .NET 9.0 SDK is installed
- Download and install .NET 9.0 SDK if needed
- Download and cache Windows App SDK installer
- Provide instructions for any manual steps required

### For Developers (Development Setup)

If you're developing PhotoAlbum, you'll also need Visual Studio 2022:

```powershell
.\Install-Prerequisites.ps1 -Development
```

This will guide you through installing:
- .NET 9.0 SDK
- Windows App SDK
- Instructions for Visual Studio 2022 installation

## Manual Installation

If you prefer to install components manually:

### 1. .NET 9.0 SDK

Download from: https://dotnet.microsoft.com/download/dotnet/9.0

Or use the installer in `Installers/` folder after running the download script.

### 2. Windows App SDK

The Windows App SDK is included with Visual Studio 2022 when you install the "Windows application development" workload.

Alternatively, download the standalone runtime from:
https://learn.microsoft.com/windows/apps/windows-app-sdk/downloads

### 3. Visual Studio 2022 (for Development)

Download Visual Studio 2022 Community (free) from:
https://visualstudio.microsoft.com/downloads/

During installation, select:
- **Workloads**:
  - ".NET desktop development"
  - "Windows application development" (includes Windows App SDK)

## Verification

After installation, verify everything is set up correctly:

```powershell
.\Verify-Prerequisites.ps1
```

This will check:
- .NET 9.0 SDK installation
- Windows App SDK availability
- Visual Studio installation (if applicable)

## Troubleshooting

### .NET SDK Not Found

If `dotnet --version` doesn't show version 9.0.x:

1. Ensure you downloaded the correct version
2. Restart your terminal/PowerShell
3. Check your PATH environment variable

### Windows App SDK Issues

If you encounter Windows App SDK errors:

1. Install Visual Studio 2022 with "Windows application development" workload
2. Or install the standalone Windows App SDK runtime
3. Restart your system after installation

### Build Errors

If the project doesn't build:

1. Run `dotnet restore` in the project root
2. Ensure all prerequisites are installed
3. Try cleaning: `dotnet clean` then `dotnet build`
4. Check the project targets .NET 9.0 in the .csproj files

## Additional Resources

- [.NET Documentation](https://docs.microsoft.com/dotnet/)
- [Windows App SDK Documentation](https://docs.microsoft.com/windows/apps/windows-app-sdk/)
- [Visual Studio Documentation](https://docs.microsoft.com/visualstudio/)
- [PhotoAlbum Getting Started Guide](../GETTING_STARTED.md)

## License

These scripts are part of the PhotoAlbum project and are licensed under the same MIT License.
