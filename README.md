# PhotoAlbum - Professional Wedding Album Designer

A professional wedding album design software built with C# and WinUI3.

> **📘 Documentation**: New to PhotoAlbum? Check out our [Windows Setup Guide](WINDOWS_SETUP.md) for detailed installation instructions, or jump to [Getting Started](GETTING_STARTED.md) to learn how to use the application.

## Features

### Core Design Features
- **Professional Design Canvas**: Intuitive drag-and-drop interface for creating stunning album layouts
- **Smart Drop Zones**: Flexible photo placement areas with support for various shapes (rectangle, circle, ellipse)
- **Planner View**: Bird's-eye view of your entire album for easy navigation and organization
- **Photo Library**: Organized photo library with thumbnails, search, and filtering capabilities
- **Templates Library**: Pre-designed layout templates for quick album creation
- **Custom Text Tool**: Add text in multiple languages with full formatting control
- **Cut Lines & Safe Zones**: Professional print guides to ensure perfect output

### AI-Powered Features
- **Face Detection**: Automatic face detection in photos using ONNX Runtime
- **Smart Photo Grouping**: Group photos by detected faces to find all pictures of specific people
- **Auto-Design**: Intelligent album generation based on your photos
- **Optimized Placement**: AI-assisted photo placement to avoid cutting faces at gutters

### Project Management
- **Multi-Project Support**: Work on multiple albums simultaneously
- **Sub-Projects**: Create parent albums, guest books, signing books, and mini albums
- **Save/Load Projects**: Save your work and resume later
- **Export Options**: Export to PDF, JPG, PNG, and TIFF formats

### Design Tools
- **Background Patterns & Images**: Apply backgrounds to pages
- **Photo Filters**: Apply effects like B&W, sepia, vintage, brightness, contrast
- **Zoom Controls**: Zoom in/out for precise editing
- **Undo/Redo**: Full undo/redo support
- **Page Management**: Add, remove, duplicate, and reorder spreads

## Technology Stack

- **Framework**: .NET 9.0
- **UI**: WinUI3 (Windows App SDK)
- **MVVM**: CommunityToolkit.Mvvm
- **DI**: Microsoft.Extensions.DependencyInjection
- **AI/ML**: Microsoft.ML.OnnxRuntime for face detection
- **Image Processing**: SixLabors.ImageSharp

## Architecture

The solution is organized into two main projects:

### PhotoAlbum.Core
Core business logic and domain models:
- **Models**: AlbumProject, AlbumSpread, AlbumPage, DropZone, PhotoElement, etc.
- **Services**: 
  - `FaceDetectionService`: AI-powered face detection
  - `ProjectService`: Project management
  - `PhotoService`: Photo import and processing
  - `LayoutService`: Template management
  - `AutoDesignService`: Automatic album generation
- **Interfaces**: Service abstractions for dependency injection

### PhotoAlbum.App
WinUI3 application:
- **ViewModels**: MVVM pattern with CommunityToolkit.Mvvm
  - `MainViewModel`: Main application logic
  - `DesignCanvasViewModel`: Canvas and design tools
  - `ImageWellViewModel`: Photo library management
  - `PlannerViewModel`: Album overview
- **Views**: XAML-based user interface
- **Controls**: Custom WinUI3 controls

## Getting Started

### Prerequisites
- **Windows 10 version 1809 (build 17763) or later** (or Windows 11)
- **.NET 9.0 SDK** - [Download here](https://dotnet.microsoft.com/download/dotnet/9.0)
- **Visual Studio 2022** (Optional, for development)
  - Windows App SDK workload
  - .NET desktop development workload

### Quick Start for Windows Users

#### Option 1: Using Batch Scripts (Easiest)
```cmd
# Build the application
build.bat

# Run the application
run.bat
```

#### Option 2: Using PowerShell Scripts
```powershell
# Build the application
.\build.ps1

# Run the application
.\run.ps1
```

#### Option 3: Using .NET CLI
```bash
# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run the application
dotnet run --project PhotoAlbum.App
```

### Building
```bash
dotnet restore
dotnet build
```

### Running
```bash
dotnet run --project PhotoAlbum.App
```

## Key Features

1. **Project Creation Wizard**: Easy setup for new wedding albums
2. **Design Templates**: Quick-start templates for common layouts
3. **Photo Library**: Organized photo management with thumbnails
4. **Drop Zones**: Flexible photo placement areas
5. **Planner View**: Overview of all album pages
6. **Custom Text Tool**: Multi-language text support
7. **Background Patterns**: Professional backgrounds and patterns
8. **Cut Lines & Safe Zones**: Print-ready design guides
9. **Auto-Design**: Intelligent automatic layout generation
10. **Sub-Projects**: Multiple book types in one project
11. **Custom Covers**: Design custom album covers
12. **Layout Saving**: Save and reuse custom layouts

## AI Features

### Face Detection
The application uses ONNX Runtime for AI-powered face detection:
- Automatic detection of faces in photos
- Confidence scoring for detected faces
- Face coordinates for smart cropping and placement

### Photo Grouping
- Automatically group photos containing the same people
- Find all photos of specific individuals
- Smart suggestions based on face detection

### Auto-Design
- Intelligent selection of layouts based on photo count and content
- Optimized photo placement to avoid cutting faces
- Balanced visual composition across spreads

## Project Structure

```
PhotoAlbum/
├── PhotoAlbum.sln
├── PhotoAlbum.Core/
│   ├── Models/           # Domain models
│   ├── Services/         # Business logic
│   └── Interfaces/       # Service abstractions
└── PhotoAlbum.App/
    ├── ViewModels/       # MVVM ViewModels
    ├── Views/            # XAML Views
    ├── Controls/         # Custom controls
    └── Helpers/          # Utility classes
```

## Future Enhancements

- Cloud storage integration
- Collaborative editing
- Advanced AI features (auto-enhancement, smart selections)
- Mobile companion app
- Print vendor integration
- 3D album preview
- Video integration for hybrid albums

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contributing

Contributions are welcome! Please read [CONTRIBUTING.md](CONTRIBUTING.md) for details on our code of conduct and the process for submitting pull requests.

## Changelog

See [CHANGELOG.md](CHANGELOG.md) for a list of changes and version history.

## Acknowledgments

- Built with [WinUI3](https://docs.microsoft.com/en-us/windows/apps/winui/winui3/) and the Windows App SDK
- Face detection powered by [ONNX Runtime](https://onnxruntime.ai/)
- Image processing with [ImageSharp](https://sixlabors.com/products/imagesharp/)
- MVVM with [CommunityToolkit.Mvvm](https://learn.microsoft.com/en-us/dotnet/communitytoolkit/mvvm/)

## Support

For questions, issues, or feature requests, please:
- Check the [Windows Setup Guide](WINDOWS_SETUP.md) for detailed installation instructions
- Check the [Getting Started Guide](GETTING_STARTED.md) for usage tutorials
- Search [existing issues](https://github.com/DrS373n/PhotoAlbum/issues)
- Create a [new issue](https://github.com/DrS373n/PhotoAlbum/issues/new)

---

Made with ❤️ for wedding photographers and designers
