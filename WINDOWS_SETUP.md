# Windows Setup Guide for PhotoAlbum

This guide provides detailed instructions for setting up and running PhotoAlbum on Windows.

## System Requirements

### Minimum Requirements
- **Operating System**: Windows 10 version 1809 (build 17763) or later
- **Processor**: x64 or ARM64 processor
- **RAM**: 4 GB minimum, 8 GB recommended
- **Storage**: 500 MB for application, additional space for photos
- **.NET**: .NET 9.0 SDK or Runtime

### Recommended Requirements
- **Operating System**: Windows 11
- **RAM**: 16 GB or more
- **Storage**: SSD with at least 10 GB free space
- **Display**: 1920x1080 or higher resolution

## Installation Steps

### Step 1: Install .NET 9.0 SDK

1. Visit the [.NET 9.0 download page](https://dotnet.microsoft.com/download/dotnet/9.0)
2. Download the **.NET 9.0 SDK** installer for Windows
3. Run the installer and follow the prompts
4. Verify installation by opening Command Prompt or PowerShell and running:
   ```cmd
   dotnet --version
   ```
   You should see version 9.0.x displayed

### Step 2: Clone or Download the Repository

#### Using Git
If you have Git installed:
```cmd
git clone https://github.com/DrS373n/PhotoAlbum.git
cd PhotoAlbum
```

#### Without Git
1. Go to https://github.com/DrS373n/PhotoAlbum
2. Click the green "Code" button
3. Select "Download ZIP"
4. Extract the ZIP file to a folder on your computer
5. Open Command Prompt or PowerShell and navigate to the extracted folder

### Step 3: Build the Application

Choose one of the following methods:

#### Method A: Using Batch Script (Recommended for beginners)
1. Double-click `build.bat` in the PhotoAlbum folder
2. The script will automatically restore packages and build the application
3. Wait for the "Build completed successfully!" message

#### Method B: Using PowerShell Script
1. Right-click on `build.ps1` and select "Run with PowerShell"
2. If you get a security warning, you may need to allow script execution:
   ```powershell
   Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
   ```
3. Run `.\build.ps1` again

#### Method C: Using Command Line
1. Open Command Prompt or PowerShell
2. Navigate to the PhotoAlbum folder
3. Run the following commands:
   ```cmd
   dotnet restore
   dotnet build
   ```

### Step 4: Run the Application

Choose one of the following methods:

#### Method A: Using Batch Script
1. Double-click `run.bat` in the PhotoAlbum folder
2. The application will start

#### Method B: Using PowerShell Script
1. Right-click on `run.ps1` and select "Run with PowerShell"
2. The application will start

#### Method C: Using Command Line
```cmd
dotnet run --project PhotoAlbum.App
```

## Development Setup (Optional)

If you want to modify the code or contribute to the project:

### Install Visual Studio 2022

1. Download [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
2. Choose "Community" edition (free) or a paid edition
3. During installation, select these workloads:
   - **.NET desktop development**
   - **Windows application development** (for WinUI3)
   - **Windows App SDK** components

### Open the Project in Visual Studio

1. Launch Visual Studio 2022
2. Select "Open a project or solution"
3. Navigate to the PhotoAlbum folder
4. Open `PhotoAlbum.slnx` or select the folder
5. Wait for Visual Studio to restore packages
6. Press F5 to build and run the application

## Troubleshooting

### "dotnet is not recognized"

**Problem**: Command Prompt doesn't recognize the `dotnet` command.

**Solution**:
1. Ensure .NET SDK is installed correctly
2. Restart Command Prompt or PowerShell
3. Add .NET to PATH manually if needed:
   - Open System Properties > Advanced > Environment Variables
   - Add `C:\Program Files\dotnet` to the PATH variable

### "Cannot run script" (PowerShell)

**Problem**: PowerShell won't run `.ps1` scripts due to execution policy.

**Solution**:
```powershell
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
```

### Build Errors

**Problem**: Build fails with errors about Windows SDK or WinUI3.

**Solution**:
1. Ensure you're running on Windows 10 1809 or later
2. Update Windows to the latest version
3. Install Visual Studio 2022 with Windows App SDK workload
4. Run Windows Update to get latest SDKs

### Application Won't Start

**Problem**: Application builds but doesn't start or crashes.

**Solution**:
1. Check Windows version (must be Windows 10 1809+)
2. Ensure all Windows updates are installed
3. Check Event Viewer for error details:
   - Press Win+X and select "Event Viewer"
   - Navigate to Windows Logs > Application
   - Look for PhotoAlbum errors
4. Try running as Administrator

### Out of Memory Errors

**Problem**: Application crashes when working with many photos.

**Solution**:
1. Close other applications to free up RAM
2. Work with smaller batches of photos
3. Reduce photo resolution before importing
4. Add more RAM to your system if possible

### Photos Won't Import

**Problem**: Photo import fails or photos don't appear.

**Solution**:
1. Ensure photo files are not corrupted
2. Check file permissions (ensure you can read the files)
3. Supported formats: JPG, JPEG, PNG
4. Try importing one photo at a time to identify problematic files

## Performance Tips for Windows

### Optimize for Best Performance

1. **Use an SSD**: Store your PhotoAlbum projects on an SSD for faster loading
2. **Close Background Apps**: Free up RAM and CPU by closing unnecessary programs
3. **Disable Animations**: Windows Settings > Ease of Access > Display > Show animations
4. **Update Graphics Drivers**: Keep your GPU drivers up to date
5. **Use Release Build**: Build with `dotnet build --configuration Release` for better performance

### Windows-Specific Optimizations

1. **Enable Hardware Acceleration**: Ensure your graphics drivers are updated
2. **Set Power Plan to High Performance**: Control Panel > Power Options
3. **Exclude from Windows Defender**: Add PhotoAlbum folder to exclusions for faster builds
4. **Use Fast Startup**: Control Panel > Power Options > Choose what the power buttons do

## Uninstallation

To remove PhotoAlbum from your system:

1. Delete the PhotoAlbum folder
2. (Optional) Remove .NET SDK if you don't need it for other applications:
   - Control Panel > Programs and Features
   - Uninstall "Microsoft .NET SDK 9.0.x"

## Additional Resources

- [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [WinUI3 Documentation](https://docs.microsoft.com/en-us/windows/apps/winui/winui3/)
- [PhotoAlbum GitHub Repository](https://github.com/DrS373n/PhotoAlbum)
- [Report Issues](https://github.com/DrS373n/PhotoAlbum/issues)

## Getting Help

If you encounter issues not covered in this guide:

1. Check the [main README](README.md)
2. Read the [Getting Started Guide](GETTING_STARTED.md)
3. Search [existing issues](https://github.com/DrS373n/PhotoAlbum/issues)
4. Create a [new issue](https://github.com/DrS373n/PhotoAlbum/issues/new) with:
   - Your Windows version
   - .NET SDK version
   - Error messages
   - Steps to reproduce the problem

## Contributing

Interested in contributing to PhotoAlbum? See [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

---

**Note**: PhotoAlbum is a Windows-only application built with WinUI3. It requires Windows 10 or later and cannot run on macOS or Linux.
