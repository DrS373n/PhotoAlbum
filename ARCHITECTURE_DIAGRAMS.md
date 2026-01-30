# PhotoAlbum Application Structure

## Visual Architecture Diagram

```
┌─────────────────────────────────────────────────────────────────────┐
│                         PhotoAlbum Solution                          │
└─────────────────────────────────────────────────────────────────────┘
                                   │
                   ┌───────────────┴────────────────┐
                   │                                │
                   ▼                                ▼
    ┌──────────────────────────┐    ┌──────────────────────────┐
    │   PhotoAlbum.App         │    │   PhotoAlbum.Core        │
    │   (WinUI3 Application)   │───▶│   (Business Logic)       │
    └──────────────────────────┘    └──────────────────────────┘
                   │                                │
        ┌──────────┼──────────┐          ┌─────────┼─────────┐
        │          │          │          │         │         │
        ▼          ▼          ▼          ▼         ▼         ▼
    ┌────┐    ┌────┐    ┌────┐    ┌────────┐ ┌────────┐ ┌────────┐
    │View│    │View│    │Ctrl│    │ Models │ │Services│ │  I/F   │
    │Model│   │XAML│    │    │    │        │ │        │ │        │
    └────┘    └────┘    └────┘    └────────┘ └────────┘ └────────┘
```

## Component Breakdown

### PhotoAlbum.App (Presentation Layer)

```
PhotoAlbum.App/
│
├── 📱 App.xaml / App.xaml.cs
│   └── Application startup and DI container configuration
│
├── 🪟 MainWindow.xaml / MainWindow.xaml.cs
│   └── Main application window with three-panel layout
│
├── 📊 ViewModels/
│   ├── MainViewModel.cs
│   │   └── Project management, file operations, photo import
│   ├── DesignCanvasViewModel.cs
│   │   └── Canvas state, zoom, templates, text elements
│   ├── ImageWellViewModel.cs
│   │   └── Photo library, search, filtering, grouping
│   └── PlannerViewModel.cs
│       └── Album overview, spread navigation, reordering
│
├── 🎨 Controls/
│   ├── DropZoneControl.cs + DropZoneControl.xaml
│   │   └── Interactive photo drop zones with drag-and-drop
│   └── AlbumPageCanvas.cs
│       └── Custom canvas for page rendering with guides
│
└── 🔧 Helpers/
    ├── FileHelper.cs
    │   └── File/folder picker dialogs
    └── Converters.cs
        └── XAML value converters (Bool→Visibility, etc.)
```

### PhotoAlbum.Core (Business Logic Layer)

```
PhotoAlbum.Core/
│
├── 📦 Models/
│   ├── AlbumProject.cs
│   │   └── Root model: project metadata, settings, spreads
│   ├── AlbumSpread.cs
│   │   └── Spread, Page, DropZone, PhotoElement, TextElement
│   └── LayoutTemplate.cs
│       └── Template, Filter, Face, SubProject, PhotoMetadata
│
├── 🔌 Interfaces/
│   └── IServices.cs
│       └── All service interfaces (6 total)
│
└── ⚙️ Services/
    ├── ProjectService.cs
    │   └── Create, open, save, export projects
    ├── PhotoService.cs
    │   └── Import, analyze, thumbnail, filter photos
    ├── FaceDetectionService.cs
    │   └── AI face detection, grouping, identification
    ├── LayoutService.cs
    │   └── Templates (6 built-in), custom creation, application
    └── AutoDesignService.cs
        └── Auto-generate albums, smart placement, optimization
```

## Data Flow Diagram

### User Creates New Project

```
User clicks "New Project"
         │
         ▼
MainViewModel.CreateNewProjectCommand
         │
         ▼
ProjectService.CreateProjectAsync()
         │
         ├─→ Create AlbumProject model
         ├─→ Add initial cover spread
         └─→ Return project
         │
         ▼
MainViewModel updates properties
         │
         ├─→ CurrentProject = new project
         ├─→ IsProjectOpen = true
         └─→ StatusMessage = "Created..."
         │
         ▼
DesignCanvas.LoadProject() + Planner.LoadProject()
         │
         ▼
UI updates via data binding
         │
         └─→ User sees new project in UI
```

### User Imports Photos

```
User clicks "Import Photos"
         │
         ▼
FileHelper.PickImageFilesAsync()
         │
         └─→ User selects files
         │
         ▼
PhotoService.ImportPhotosAsync()
         │
         ├─→ For each photo:
         │   ├─→ Load image
         │   ├─→ Generate thumbnail
         │   ├─→ Extract metadata
         │   └─→ FaceDetectionService.DetectFaces()
         │
         └─→ Return PhotoMetadata[]
         │
         ▼
ImageWellViewModel.AddPhotosAsync()
         │
         └─→ AllPhotos.Add() + ApplyFilter()
         │
         ▼
UI updates
         │
         └─→ Photos appear in Image Well
```

### User Applies Auto-Design

```
User clicks "Auto Design"
         │
         ▼
AutoDesignService.AutoGenerateAlbumAsync()
         │
         ├─→ Get all photos
         ├─→ Get layout templates
         │
         └─→ For each page:
             ├─→ Select template based on photo count
             ├─→ Fill drop zones with photos
             ├─→ Optimize placement (avoid cutting faces)
             └─→ Balance composition
         │
         ▼
Return updated spreads
         │
         ▼
Update AlbumProject.Spreads
         │
         ▼
ViewModels refresh
         │
         ▼
Canvas re-renders
         │
         └─→ User sees designed album
```

## UI Layout Structure

```
┌─────────────────────────────────────────────────────────────────┐
│ Menu: File | Edit | Pages | Design | View | Help                │
├─────────────────────────────────────────────────────────────────┤
│ Toolbar: [New] [Open] [Save] | [Import] | [Undo] [Redo] | [+][-]│
├───────────┬──────────────────────────────────┬──────────────────┤
│ IMAGE WELL│      DESIGN CANVAS               │  TEMPLATES       │
│           │                                  │                  │
│ [Search]  │   ┌────────────────────┐         │ ┌──────────┐    │
│ [Filter▼] │   │                    │         │ │ Template │    │
│           │   │   Page Preview     │         │ │  Preview │    │
│ ┌────┐    │   │                    │         │ └──────────┘    │
│ │IMG │    │   │   [Drop Zones]     │         │                  │
│ └────┘    │   │   [Photos]         │         │ ┌──────────┐    │
│ ┌────┐    │   │   [Text]           │         │ │ Template │    │
│ │IMG │    │   │                    │         │ │  Preview │    │
│ └────┘    │   │   [Cut Lines]      │         │ └──────────┘    │
│ ┌────┐    │   │   [Safe Zones]     │         │                  │
│ │IMG │    │   │                    │         │ [Properties]     │
│ └────┘    │   └────────────────────┘         │                  │
│           │                                  │ [Planner]        │
│ 250px     │         Flexible                 │     300px        │
├───────────┴──────────────────────────────────┴──────────────────┤
│ Status: Ready                                    Zoom: 100%     │
└─────────────────────────────────────────────────────────────────┘
```

## Technology Stack Visualization

```
┌─────────────────────────────────────────────┐
│           User Interface Layer               │
│                                              │
│  WinUI3 (Windows App SDK)                    │
│  XAML + Code-behind                          │
│  CommunityToolkit.Mvvm                       │
└──────────────┬───────────────────────────────┘
               │ Data Binding
               │ Commands
               ▼
┌─────────────────────────────────────────────┐
│         ViewModel Layer (MVVM)               │
│                                              │
│  MainViewModel                               │
│  DesignCanvasViewModel                       │
│  ImageWellViewModel                          │
│  PlannerViewModel                            │
└──────────────┬───────────────────────────────┘
               │ Service Calls
               │ Dependency Injection
               ▼
┌─────────────────────────────────────────────┐
│         Service Layer                        │
│                                              │
│  ProjectService    LayoutService             │
│  PhotoService      AutoDesignService         │
│  FaceDetectionService                        │
└──────────────┬───────────────────────────────┘
               │ Operates On
               ▼
┌─────────────────────────────────────────────┐
│         Domain Model Layer                   │
│                                              │
│  AlbumProject  AlbumSpread  AlbumPage        │
│  DropZone  PhotoElement  TextElement         │
│  LayoutTemplate  PhotoMetadata               │
└──────────────┬───────────────────────────────┘
               │
               ▼
┌─────────────────────────────────────────────┐
│         External Libraries                   │
│                                              │
│  ImageSharp → Image Processing               │
│  ONNX Runtime → AI/ML Inference             │
│  Newtonsoft.Json → Serialization            │
└─────────────────────────────────────────────┘
```

## Dependency Graph

```
                    App.xaml.cs
                         │
                         │ Creates Host
                         │ Registers Services
                         │
         ┌───────────────┼───────────────┐
         │               │               │
         ▼               ▼               ▼
    MainViewModel  DesignCanvas   ImageWell
         │          ViewModel      ViewModel
         │               │               │
         │               │               │
    ┌────┴────┐     ┌────┴────┐     ┌───┴────┐
    ▼         ▼     ▼         ▼     ▼        ▼
Project   Photo  Layout   AutoDesign  Face
Service  Service Service   Service  Detection
    │         │      │         │     Service
    │         │      │         │         │
    └────┬────┴──┬───┴────┬────┴─────────┘
         │       │        │
         ▼       ▼        ▼
    AlbumProject Models + Interfaces
```

## Feature Map

```
PhotoAlbum Features
│
├── 📁 Project Management
│   ├── Create new projects
│   ├── Save/Load (.pap files)
│   └── Export (PDF, JPG, PNG, TIFF)
│
├── 🖼️ Photo Management
│   ├── Import photos
│   ├── Thumbnail generation
│   ├── Search and filter
│   ├── Usage tracking
│   └── Metadata extraction
│
├── 🎨 Design Tools
│   ├── Drop zones (Rectangle, Circle, Ellipse)
│   ├── Text elements (Multi-language)
│   ├── Backgrounds (Color, Pattern, Image)
│   ├── Cut lines & safe zones
│   └── Zoom and pan
│
├── 📑 Templates
│   ├── 6 Built-in layouts
│   ├── Custom template creation
│   ├── Template application
│   └── Quick design picker
│
├── 🤖 AI Features
│   ├── Face detection
│   ├── Photo grouping by people
│   ├── Auto-design generation
│   └── Smart photo placement
│
├── 📖 Album Structure
│   ├── Spreads (2-page layouts)
│   ├── Page management
│   ├── Sub-projects
│   └── Cover customization
│
└── 🔧 Advanced
    ├── Multi-language support
    ├── Undo/Redo (architecture ready)
    ├── Keyboard shortcuts
    └── Professional output
```

## File System Structure

```
PhotoAlbum/
│
├── 📄 Documentation (9 files)
│   ├── README.md (Overview)
│   ├── FEATURES.md (Feature details)
│   ├── GETTING_STARTED.md (Tutorial)
│   ├── ARCHITECTURE.md (Technical)
│   ├── CONTRIBUTING.md (Guidelines)
│   ├── CHANGELOG.md (History)
│   ├── PROJECT_SUMMARY.md (Summary)
│   ├── LICENSE (MIT)
│   └── sample-project.json (Example)
│
├── 🔧 Configuration
│   ├── PhotoAlbum.slnx (Solution)
│   ├── .gitignore
│   └── .gitattributes
│
├── 📦 PhotoAlbum.Core/ (Platform-agnostic)
│   ├── PhotoAlbum.Core.csproj
│   ├── Models/ (3 files)
│   ├── Services/ (5 files)
│   └── Interfaces/ (1 file)
│
└── 🖥️ PhotoAlbum.App/ (WinUI3)
    ├── PhotoAlbum.App.csproj
    ├── App.xaml + App.xaml.cs
    ├── MainWindow.xaml + MainWindow.xaml.cs
    ├── app.manifest
    ├── ViewModels/ (4 files)
    ├── Controls/ (3 files)
    └── Helpers/ (2 files)

Total: 30 project files + 9 documentation files
```

This diagram-based document provides a visual understanding of how all the pieces fit together in the PhotoAlbum application!
