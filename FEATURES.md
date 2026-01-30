# PhotoAlbum Features Documentation

This document describes the key features of PhotoAlbum wedding album designer software, inspired by Fundy Designer.

## 1. Project Management

### Create New Project
- Set project name, client name, and wedding date
- Choose album size and orientation (portrait, landscape, square)
- Configure bleed and safe zone settings
- Select cover type

### Open/Save Projects
- Save projects in .pap format (PhotoAlbum Project)
- Auto-save functionality
- Recent projects list
- Project templates for quick start

### Sub-Projects
Support for multiple book types within a single project:
- **Parent Album**: Main wedding album
- **Guest Book**: Guest signing pages
- **Signing Book**: Signature collection
- **Mini Albums**: Smaller companion albums
- **Thank You Cards**: Card designs

## 2. Design Canvas

### Main Canvas Features
- High-resolution design surface
- Real-time zoom (10% to 300%)
- Pan with mouse/touch
- Grid and ruler guides
- Snap to guides and objects
- Multi-page spread view

### Cut Lines & Safe Zones
- **Cut Lines**: Red lines showing where the page will be trimmed (bleed area)
- **Safe Zones**: Blue lines showing the safe area for important content
- Toggle visibility on/off
- Customizable sizes

### Drop Zones
Photo placement areas with features:
- Drag-and-drop photo placement
- Multiple shapes: rectangle, circle, ellipse, custom
- Resize and rotate zones
- Lock aspect ratio
- Z-index layering
- Smart guides for alignment

## 3. Image Well

### Photo Library Management
- Import photos from folders
- Thumbnail view with multiple sizes
- Grid and list view options
- Sorting options (date, name, size)
- Color coding for used/unused photos

### Search & Filter
- **Search**: Find photos by filename or tags
- **Filter Groups**:
  - All Photos
  - Used Photos
  - Unused Photos
  - Photos with People (face detection)
  - By date range
  - By location (EXIF data)

### Photo Information
- Filename and path
- Resolution and file size
- Date taken (EXIF)
- Number of times used
- Detected faces count
- Custom tags

## 4. Templates & Layouts

### Template Categories
- **Single Photo**: Full bleed, portrait, landscape
- **Two Photos**: Side by side, top/bottom, asymmetric
- **Three Photos**: Hero with two, vertical stack
- **Four Photos**: Grid, magazine style
- **Multiple Photos**: Collage layouts (5-9 photos)

### Custom Templates
- Create your own drop zone layouts
- Save as reusable templates
- Share templates with others
- Import template packs

### Quick Design Picker
- Visual template browser
- Category filtering
- Preview before applying
- One-click application

## 5. Planner View

### Album Overview
- Thumbnail view of all spreads
- Drag-and-drop reordering
- Visual page numbering
- Spread status indicators:
  - Empty
  - In Progress
  - Complete
  - Needs Attention

### Navigation
- Click to jump to spread
- Keyboard navigation (arrow keys)
- Zoom levels for overview
- Filter by status

## 6. Text Tools

### Custom Text Tool
- Rich text formatting:
  - Font family, size, color
  - Bold, italic, underline
  - Alignment (left, center, right, justify)
  - Line spacing and kerning

### Multi-Language Support
- Unicode text support
- Right-to-left languages (Arabic, Hebrew)
- Asian character sets (Chinese, Japanese, Korean)
- Special characters and symbols

### Text Effects
- Drop shadow
- Outline
- Rotation
- Curved text
- Text on path

## 7. Background Options

### Background Types
- **Solid Colors**: Color picker with custom colors
- **Patterns**: Repeating patterns library
  - Textures
  - Geometrics
  - Florals
  - Vintage patterns
- **Images**: Full-bleed background photos
- **Gradients**: Linear and radial gradients

### Background Properties
- Opacity control
- Blend modes
- Tiling options for patterns
- Position and scale for images

## 8. AI-Powered Features

### Face Detection
- Automatic face detection in all imported photos
- Bounding boxes around detected faces
- Confidence scores
- Face landmarks (eyes, nose, mouth)

### Photo Grouping
- Group photos by detected people
- Name recognition and person identification
- Find all photos of specific people
- Couple detection for romantic poses

### Auto-Design
- **Smart Layout Selection**: Choose appropriate templates based on:
  - Number of photos
  - Photo orientations
  - Content (people vs. landscapes)
  - Variety and visual balance

- **Intelligent Photo Placement**:
  - Face-aware cropping (don't cut faces)
  - Avoid faces in gutter (binding area)
  - Balance visual weight across spreads
  - Group related photos together
  - Vary sizes for visual interest

### Auto-Enhancement
- Automatic color correction
- Exposure adjustment
- Smart cropping suggestions
- Duplicate detection

## 9. Photo Editing

### Basic Adjustments
- Brightness and contrast
- Saturation and vibrance
- Sharpness
- Temperature and tint

### Filters
- Black & White
- Sepia
- Vintage
- Custom filter presets

### Cropping & Rotation
- Freeform crop
- Aspect ratio presets
- 90° rotation
- Straighten tool
- Flip horizontal/vertical

## 10. Export & Output

### Export Formats
- **PDF**: Print-ready with bleed and crop marks
- **JPG**: Individual pages or spreads
- **PNG**: With transparency support
- **TIFF**: Uncompressed for professional printing

### Export Options
- Resolution selection (72-300 DPI)
- Color space (RGB, CMYK)
- Page range selection
- Include crop marks and bleed
- Embed fonts
- Flatten layers

### Print Vendor Presets
- Pre-configured settings for common print vendors
- Custom vendor profiles
- Upload directly to some vendors

## 11. Advanced Features

### Design Wizard
Step-by-step album creation:
1. Select album size and style
2. Choose number of pages
3. Import photos
4. Auto-design or manual layout
5. Customize and refine
6. Review and export

### Layout Saving
- Save current page as template
- Save entire spread layout
- Create layout collections
- Export/import layouts

### Keyboard Shortcuts
- Ctrl+N: New project
- Ctrl+O: Open project
- Ctrl+S: Save project
- Ctrl+I: Import photos
- Ctrl+Z: Undo
- Ctrl+Y: Redo
- Space: Pan tool
- +/-: Zoom in/out
- Delete: Remove selected element

### Undo/Redo
- Unlimited undo levels
- Visual undo history
- Selective undo (revert specific changes)

## 12. Collaboration Features (Future)

- Cloud storage integration
- Share projects with clients
- Client feedback and approval
- Version control
- Team collaboration

## 13. Custom Covers

### Cover Design
- Front cover design
- Spine customization
- Back cover design
- Dust jacket design

### Cover Options
- Debossing and embossing simulation
- Foil stamping preview
- Custom materials (leather, fabric, etc.)
- Window cutouts
- Custom sizes

## 14. Performance Features

### Optimization
- Thumbnail caching
- Lazy loading of images
- Background processing for AI
- GPU acceleration for rendering
- Multi-threaded operations

### File Management
- Linked vs embedded photos
- Missing file detection
- Batch operations
- Smart collections

## Technical Implementation Notes

### WinUI3 Features Used
- Modern Windows 11 design language
- Native Windows controls
- High DPI support
- Touch and pen input
- Smooth animations and transitions

### MVVM Architecture
- Clean separation of concerns
- Testable ViewModels
- Reactive UI updates
- Command pattern for actions

### AI/ML Integration
- ONNX Runtime for cross-platform AI
- Pre-trained face detection models
- Custom ML models support
- Privacy-focused (all processing local)

### Image Processing
- SixLabors.ImageSharp for cross-platform image operations
- Non-destructive editing
- Format conversion
- Batch processing support
