# PhotoAlbum Technical Architecture

## Overview

PhotoAlbum is a professional wedding album design application built with modern .NET technologies and WinUI3. This document describes the technical architecture, design patterns, and implementation details.

## Technology Stack

### Core Technologies
- **.NET 9.0**: Latest .NET framework for modern C# features
- **WinUI3**: Windows App SDK for native Windows UI
- **C# 13**: Language features including nullable reference types, records, etc.

### Key Libraries

#### UI & MVVM
- **Microsoft.WindowsAppSDK**: WinUI3 framework
- **CommunityToolkit.Mvvm**: Source generators for MVVM pattern
- **Microsoft.Extensions.DependencyInjection**: Built-in DI container
- **Microsoft.Extensions.Hosting**: Host builder pattern

#### Image Processing
- **SixLabors.ImageSharp**: Cross-platform image manipulation
  - Resize, crop, rotate operations
  - Format conversion
  - Filter application
  - High-performance processing

#### AI/ML
- **Microsoft.ML.OnnxRuntime**: ONNX model execution
  - Face detection models
  - Person identification
  - Image analysis
  - Cross-platform ML inference

#### Data
- **Newtonsoft.Json**: JSON serialization for project files
  - Project save/load
  - Settings persistence
  - Template storage

## Architecture Patterns

### Clean Architecture

The solution follows clean architecture principles with clear separation of concerns:

```
┌──────────────────────────────────────┐
│         PhotoAlbum.App               │  ← Presentation Layer
│         (WinUI3 Views)               │
├──────────────────────────────────────┤
│         ViewModels                   │  ← Presentation Logic
│         (MVVM Pattern)               │
├──────────────────────────────────────┤
│         PhotoAlbum.Core              │  ← Business Logic
│         (Services & Models)          │
└──────────────────────────────────────┘
```

#### Layers

1. **Presentation Layer** (`PhotoAlbum.App`)
   - XAML views and code-behind
   - Custom controls
   - UI-specific logic
   - Platform-specific code

2. **View Models** (`PhotoAlbum.App/ViewModels`)
   - MVVM ViewModels
   - UI state management
   - Command handling
   - Data binding

3. **Business Logic** (`PhotoAlbum.Core`)
   - Domain models
   - Service interfaces
   - Service implementations
   - Business rules

### MVVM Pattern

Complete separation of UI and logic using Model-View-ViewModel:

```
View (XAML)  ←→  ViewModel  ←→  Model/Services
    ↑                ↑              ↑
    │                │              │
  Bindings       Commands       Business
   Events        Properties       Logic
```

#### Benefits
- **Testability**: ViewModels can be unit tested
- **Maintainability**: Clear separation of concerns
- **Reusability**: ViewModels independent of views
- **Designer-friendly**: XAML can be designed independently

### Dependency Injection

All services and ViewModels use constructor injection:

```csharp
// Service registration
services.AddSingleton<IFaceDetectionService, FaceDetectionService>();
services.AddTransient<MainViewModel>();

// Constructor injection
public class MainViewModel
{
    public MainViewModel(IProjectService projectService, ...)
    {
        _projectService = projectService;
    }
}
```

#### Benefits
- **Loose coupling**: Easy to swap implementations
- **Testability**: Mock dependencies in tests
- **Lifetime management**: Control object lifecycle
- **Configuration**: Change behavior via DI

## Project Structure

### PhotoAlbum.Core

Core business logic library (platform-agnostic):

```
PhotoAlbum.Core/
├── Models/                    # Domain models
│   ├── AlbumProject.cs       # Main project model
│   ├── AlbumSpread.cs        # Page and spread models
│   └── LayoutTemplate.cs     # Template models
├── Interfaces/               # Service contracts
│   └── IServices.cs          # All service interfaces
└── Services/                 # Service implementations
    ├── FaceDetectionService.cs
    ├── ProjectService.cs
    ├── PhotoService.cs
    ├── LayoutService.cs
    └── AutoDesignService.cs
```

### PhotoAlbum.App

WinUI3 application:

```
PhotoAlbum.App/
├── ViewModels/               # MVVM ViewModels
│   ├── MainViewModel.cs
│   ├── DesignCanvasViewModel.cs
│   ├── ImageWellViewModel.cs
│   └── PlannerViewModel.cs
├── Views/                    # XAML views (future)
├── Controls/                 # Custom controls
│   ├── DropZoneControl.cs
│   ├── DropZoneControl.xaml
│   └── AlbumPageCanvas.cs
├── Helpers/                  # Utility classes
│   ├── FileHelper.cs
│   └── Converters.cs
├── App.xaml                  # Application definition
├── App.xaml.cs               # App startup and DI
└── MainWindow.xaml           # Main window
```

## Core Components

### 1. Album Project System

#### AlbumProject
Root model containing entire album state:
- Project metadata (name, client, dates)
- Settings (size, orientation, guides)
- Collection of spreads
- Sub-projects

#### AlbumSpread
Two-page spread model:
- Left and right pages
- Spread type (cover, regular, back)
- Spread number for ordering

#### AlbumPage
Single page model:
- Collection of drop zones
- Text elements
- Background settings
- Applied template reference

### 2. Photo Management

#### PhotoMetadata
Metadata for imported photos:
- File information (path, size, dimensions)
- EXIF data (date taken, camera settings)
- Detected faces
- Tags and usage tracking

#### PhotoService
Photo processing service:
- Import and analyze photos
- Generate thumbnails
- Apply filters and effects
- Extract metadata

### 3. AI Features

#### FaceDetectionService
Face detection using ONNX Runtime:
- Detect faces in photos
- Extract face embeddings
- Group photos by people
- Identify persons

**Implementation Notes:**
- Currently uses simulated detection for demo
- Production version would load ONNX model
- Models: RetinaFace, MTCNN, or similar
- Privacy-first: All processing local

#### AutoDesignService
Intelligent album generation:
- Select appropriate templates
- Place photos intelligently
- Avoid cutting faces at gutters
- Balance visual composition

### 4. Layout System

#### LayoutTemplate
Reusable page layout:
- Pre-defined drop zone arrangements
- Background settings
- Category and metadata

#### LayoutService
Template management:
- Load built-in templates
- Create custom templates
- Apply templates to pages
- Suggest templates based on content

**Built-in Templates:**
- Full Bleed (1 photo)
- Side by Side (2 photos)
- Hero with Two (3 photos)
- Four Grid (4 photos)
- Magazine Style (6 photos)

### 5. Design Canvas

#### AlbumPageCanvas
Custom Canvas control for page rendering:
- Render drop zones
- Display photos with transforms
- Show text elements
- Draw guides (cut lines, safe zones)

#### DropZoneControl
Interactive drop zone control:
- Drag-and-drop support
- Resize and rotate handles
- Visual feedback
- Photo display

## Data Flow

### Project Loading
```
User Action
    ↓
MainViewModel.OpenProject()
    ↓
ProjectService.OpenProjectAsync()
    ↓
JSON Deserialization
    ↓
AlbumProject Model
    ↓
Update ViewModels
    ↓
UI Updates (via bindings)
```

### Photo Import
```
User Selects Photos
    ↓
MainViewModel.ImportPhotos()
    ↓
PhotoService.ImportPhotosAsync()
    ↓
For each photo:
    ├─→ Load image
    ├─→ Generate thumbnail
    ├─→ Extract metadata
    └─→ FaceDetectionService.DetectFaces()
    ↓
PhotoMetadata collection
    ↓
ImageWellViewModel.AddPhotos()
    ↓
UI Updates
```

### Auto-Design
```
User clicks Auto-Design
    ↓
MainViewModel initiates
    ↓
AutoDesignService.AutoGenerateAlbum()
    ↓
For each page:
    ├─→ LayoutService.GetTemplates()
    ├─→ Select template by photo count
    ├─→ FillPageWithPhotos()
    └─→ OptimizePlacement()
    ↓
Updated AlbumProject
    ↓
ViewModels refresh
    ↓
Canvas re-renders
```

## Performance Considerations

### Image Handling
- **Thumbnails**: Generate once, cache to disk
- **Lazy loading**: Load images on-demand
- **Resolution scaling**: Work at display resolution, export at print
- **Memory management**: Dispose of image resources

### UI Rendering
- **Virtualization**: Only render visible spreads
- **Async operations**: Keep UI responsive
- **Background tasks**: Face detection, thumbnail generation
- **Progressive loading**: Show UI before all data loaded

### File Operations
- **Streaming**: Don't load entire files into memory
- **Compression**: Compress project files
- **Incremental saves**: Only save changed data
- **Async I/O**: Non-blocking file operations

## Security Considerations

### Data Privacy
- **Local processing**: All AI runs locally, no cloud
- **No telemetry**: No usage data sent externally
- **User control**: User owns all data
- **Secure storage**: Standard file system permissions

### Vulnerability Management
- **Dependency scanning**: Regular NuGet updates
- **Known vulnerabilities**: Monitor advisories
- **Input validation**: Validate all user input
- **Path traversal**: Sanitize file paths

### Best Practices
- **Nullable reference types**: Reduce null reference exceptions
- **Input validation**: Validate all external data
- **Error handling**: Graceful degradation
- **Logging**: Track errors for debugging

## Extension Points

### Custom Templates
```csharp
var template = await layoutService.CreateCustomTemplateAsync(
    "My Template",
    dropZones
);
```

### Custom Filters
```csharp
public class CustomFilter : PhotoFilter
{
    public override byte[] Apply(byte[] image)
    {
        // Custom processing
    }
}
```

### Face Detection Models
```csharp
public class CustomFaceDetection : IFaceDetectionService
{
    // Use different ML model
}
```

## Testing Strategy

### Unit Tests
- Test ViewModels in isolation
- Mock service dependencies
- Test business logic
- Validate model behavior

### Integration Tests
- Test service interactions
- Test file I/O operations
- Test image processing
- Test serialization

### UI Tests
- Test user workflows
- Test data binding
- Test commands
- Visual regression tests

## Future Enhancements

### Planned Features
- **Cloud sync**: OneDrive, Google Drive integration
- **Collaboration**: Multi-user editing
- **Templates marketplace**: Share and download templates
- **Advanced AI**: Auto-enhancement, style transfer
- **Print integration**: Direct send to print vendors
- **Mobile app**: iOS/Android companion

### Technical Improvements
- **Performance**: GPU acceleration for rendering
- **Accessibility**: Screen reader support, high contrast
- **Localization**: Multiple language support
- **Plugins**: Extension system for third-party add-ons
- **Real-time preview**: Live 3D album preview

## Building and Deployment

### Development Build
```bash
dotnet build
dotnet run --project PhotoAlbum.App
```

### Release Build
```bash
dotnet publish -c Release -r win-x64 --self-contained
```

### Packaging
- MSIX package for Windows Store
- Standalone installer for direct distribution
- Portable version (no installation)

### System Requirements
- **OS**: Windows 10 1809+ or Windows 11
- **RAM**: 4GB minimum, 8GB recommended
- **Storage**: 500MB for app, space for projects
- **Display**: 1920x1080 minimum resolution
- **.NET**: Runtime included in package

## Conclusion

PhotoAlbum uses modern .NET technologies and architectural patterns to create a professional, extensible wedding album design application. The clean separation of concerns, dependency injection, and MVVM pattern make it maintainable and testable while providing excellent performance and user experience.
