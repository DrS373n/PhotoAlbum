# Changelog

All notable changes to PhotoAlbum will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
- Initial WinUI3 application structure
- Core business logic library (PhotoAlbum.Core)
- MVVM architecture with ViewModels
- Dependency injection setup
- Main window with three-panel layout
- Album project model system
- Spread and page management
- Drop zone system for photo placement
- Layout template system with 6 built-in templates
- Face detection service (ONNX Runtime ready)
- Photo grouping by detected faces
- Auto-design service for automatic album generation
- Photo service for import and processing
- Project service for save/load functionality
- Image Well for photo library management
- Design canvas with zoom controls
- Planner view for album overview
- Template picker in properties panel
- Custom DropZoneControl for interactive photo zones
- AlbumPageCanvas for page rendering
- File picker helpers
- XAML value converters
- Text element support with multi-language
- Background patterns and images support
- Cut lines and safe zones visualization
- Sub-project support (guest books, signing books, etc.)
- Comprehensive documentation:
  - README.md with overview
  - FEATURES.md with detailed feature descriptions
  - GETTING_STARTED.md with tutorial
  - ARCHITECTURE.md with technical details
  - CONTRIBUTING.md with contribution guidelines
  - Sample project file format
- Code documentation and XML comments

### Security
- Updated SixLabors.ImageSharp to 3.1.7 to address known vulnerabilities
- All dependencies use latest stable versions
- Local AI processing for privacy

## [0.1.0] - 2024-01-30

### Added
- Initial project setup
- Solution structure
- Basic project files

---

## Version History

### Planned for v1.0.0
- Complete PDF export
- Undo/redo functionality
- Full face detection with ONNX models
- Production-ready auto-design
- Template gallery
- File picker integration
- Comprehensive unit tests
- Performance optimizations
- Installer packages

### Planned for v1.1.0
- Additional export formats (TIFF, high-res JPG)
- Advanced photo filters
- Cloud storage integration
- Collaboration features
- Template marketplace

### Planned for v2.0.0
- Plugin system
- Mobile companion app
- Advanced AI features
- 3D album preview
- Video integration
- Print vendor integration
