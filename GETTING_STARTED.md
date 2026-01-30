# Getting Started with PhotoAlbum

This guide will help you get started with PhotoAlbum wedding album designer software.

## Installation

### Prerequisites
1. **Windows 10 or later** (version 1809, build 17763 or higher recommended)
2. **.NET 9.0 SDK** - [Download here](https://dotnet.microsoft.com/download/dotnet/9.0)
3. **Visual Studio 2022** (Optional, for development)
   - Windows App SDK workload
   - .NET desktop development workload

### Automated Installation (Recommended)

We provide automated scripts to install all prerequisites:

**Option 1: Using PowerShell (Recommended)**

Open PowerShell and run:
```powershell
cd Prerequisites
.\Install-Prerequisites.ps1
```

For development setup with Visual Studio guidance:
```powershell
.\Install-Prerequisites.ps1 -Development
```

**Option 2: Using Batch File**

Double-click `Prerequisites\Install-Prerequisites.bat` or run from Command Prompt:
```cmd
cd Prerequisites
Install-Prerequisites.bat
```

**Verify Installation**

After installation, verify everything is set up correctly:
```powershell
cd Prerequisites
.\Verify-Prerequisites.ps1
```

For detailed installation instructions, see [Prerequisites/README.md](Prerequisites/README.md).

### Building from Source

1. Clone the repository:
```bash
git clone https://github.com/DrS373n/PhotoAlbum.git
cd PhotoAlbum
```

2. Restore NuGet packages:
```bash
dotnet restore
```

3. Build the solution:
```bash
dotnet build
```

4. Run the application:
```bash
dotnet run --project PhotoAlbum.App
```

## Quick Start Tutorial

### Creating Your First Album

1. **Launch PhotoAlbum**
   - Run the application
   - You'll see the main window with three panels

2. **Create a New Project**
   - Click **File > New Project** or press `Ctrl+N`
   - Enter project details:
     - Project Name: "Sarah & John's Wedding"
     - Client Name: "Sarah & John"
     - Wedding Date: Select the date
   - Click **Create**

3. **Import Your Photos**
   - Click **File > Import Photos** or press `Ctrl+I`
   - Select wedding photos from your computer
   - Photos will appear in the **Image Well** (left panel)
   
4. **Design Your Album**

   **Option A: Auto-Design (Quick)**
   - Click **Design > Auto Design**
   - PhotoAlbum will automatically:
     - Detect faces in photos
     - Group related photos
     - Create layouts
     - Place photos in drop zones
   
   **Option B: Manual Design (Custom)**
   - Click on a spread in the **Planner** (right panel)
   - Select a template from the **Templates** tab
   - Drag photos from Image Well to drop zones
   - Adjust positioning and sizing

5. **Add Text**
   - Click **Design > Add Text** or click the text icon
   - Double-click the text to edit
   - Format using the Properties panel

6. **Save Your Project**
   - Click **File > Save Project** or press `Ctrl+S`
   - Choose a location and filename
   - Project is saved with `.pap` extension

## Understanding the Interface

### Main Window Layout

```
┌─────────────────────────────────────────────────────────┐
│ Menu Bar: File, Edit, Pages, Design, View, Help        │
├─────────────────────────────────────────────────────────┤
│ Toolbar: Quick access to common actions                │
├──────────┬───────────────────────────┬──────────────────┤
│  Image   │   Design Canvas           │   Templates/     │
│  Well    │   (Main editing area)     │   Properties/    │
│          │                            │   Planner        │
│  (Left)  │   (Center)                 │   (Right)        │
├──────────┴───────────────────────────┴──────────────────┤
│ Status Bar: Messages and zoom level                     │
└─────────────────────────────────────────────────────────┘
```

### Left Panel: Image Well
- **Purpose**: Manage your photo library
- **Features**:
  - Thumbnail view of all imported photos
  - Search box to find photos
  - Filter dropdown (All, Used, Unused, People)
  - Photo count indicator
  
**Tips**:
- Photos with faces show a badge
- Used photos are marked
- Click to select, drag to drop zones

### Center Panel: Design Canvas
- **Purpose**: Main design and editing area
- **Features**:
  - Page/spread view
  - Drop zones for photos
  - Text elements
  - Background preview
  - Cut lines (red) and safe zones (blue)
  
**Controls**:
- Mouse wheel: Zoom in/out
- Middle click + drag: Pan
- Click on element: Select
- Drag element: Move
- Corner handles: Resize

### Right Panel: Tabs

#### Templates Tab
- Browse layout templates
- Categories: Single, Two Photos, Three Photos, etc.
- Click to apply to current page

#### Properties Tab
- Selected element properties
- Adjust size, position, rotation
- Photo scale and filters
- Text formatting

#### Planner Tab
- Overview of all album spreads
- Thumbnail view
- Drag to reorder
- Click to navigate

## Common Tasks

### Adding a New Spread
1. Click **Pages > Add Spread**
2. New blank spread is added
3. Apply a template or design manually

### Applying a Template
1. Select a page in the canvas or planner
2. Go to **Templates** tab (right panel)
3. Click on desired template
4. Drop zones appear on the page

### Adding Photos to Drop Zones
**Method 1: Drag and Drop**
1. Select a photo in Image Well
2. Drag to a drop zone on the canvas
3. Photo fills the zone

**Method 2: Click to Fill**
1. Select a drop zone
2. Select a photo in Image Well
3. Right-click > "Place in Zone"

### Adjusting Photos in Zones
1. Click on a photo in a drop zone
2. Use handles to:
   - Resize: Corner handles
   - Rotate: Rotation handle
   - Move within zone: Drag
3. Properties panel shows:
   - Scale percentage
   - Position offset
   - Rotation angle

### Adding and Formatting Text
1. Click **Design > Add Text** or text icon
2. Text element appears on page
3. Double-click to edit text
4. Use Properties panel to format:
   - Font family and size
   - Color and alignment
   - Bold, italic, underline
   - Rotation

### Using Face Detection
1. Import photos (faces are detected automatically)
2. Click **View > Group by Faces** in Image Well
3. Photos are grouped by detected people
4. Use groups to:
   - Find all photos of a person
   - Create focused spreads
   - Balance couple appearances

### Auto-Design Your Album
1. Import all photos
2. Click **Design > Auto Design**
3. Configure options:
   - Number of pages
   - Photos per page (average)
   - Style preference
4. Click **Generate**
5. Review and adjust as needed

### Customizing Backgrounds
1. Select a page
2. Properties panel > Background
3. Choose:
   - **Solid Color**: Pick from palette
   - **Pattern**: Select from library
   - **Image**: Choose background photo
4. Adjust opacity if needed

### Working with Cut Lines and Safe Zones
- **Cut Lines (Red)**: Show trim edge with bleed
- **Safe Zones (Blue)**: Keep important content inside

**Toggle Visibility**:
- Menu: **View > Show Cut Lines**
- Menu: **View > Show Safe Zones**

**Best Practices**:
- Keep faces and text inside safe zones
- Extend backgrounds to cut lines
- Don't place critical content near edges

### Saving Layouts as Templates
1. Design a page layout you like
2. Click **Design > Save as Template**
3. Enter template name
4. Template is saved to "Custom" category
5. Reuse on other pages

## Keyboard Shortcuts

### File Operations
- `Ctrl+N` - New Project
- `Ctrl+O` - Open Project
- `Ctrl+S` - Save Project
- `Ctrl+I` - Import Photos
- `Ctrl+E` - Export

### Editing
- `Ctrl+Z` - Undo
- `Ctrl+Y` - Redo
- `Ctrl+C` - Copy
- `Ctrl+V` - Paste
- `Delete` - Delete Selected
- `Ctrl+D` - Duplicate

### View
- `Ctrl++` - Zoom In
- `Ctrl+-` - Zoom Out
- `Ctrl+0` - Zoom to Fit
- `Ctrl+1` - 100% Zoom
- `F11` - Full Screen

### Navigation
- `Page Up` - Previous Spread
- `Page Down` - Next Spread
- `Home` - First Spread
- `End` - Last Spread
- `Arrow Keys` - Move Selection

## Tips and Best Practices

### Photo Management
- **Organize before import**: Name files descriptively
- **Use RAW or high-res JPGs**: Better quality
- **Backup originals**: Keep copies safe
- **Tag important photos**: Use custom tags

### Design Tips
- **Tell a story**: Chronological or thematic order
- **Vary layouts**: Mix single, double, multi-photo pages
- **Balance color and B&W**: Create visual rhythm
- **Use white space**: Don't overcrowd pages
- **Feature key moments**: Larger photos for important shots

### Face Detection
- **Good lighting helps**: Clearer faces = better detection
- **Review detections**: AI isn't perfect
- **Name people**: Helps with grouping
- **Use for balance**: Ensure equal representation

### Performance
- **Work at display resolution**: Export at print resolution
- **Save frequently**: `Ctrl+S` habit
- **Close unused projects**: Free up memory
- **Use thumbnails**: Faster browsing

### Export Settings
- **For print**: 300 DPI, CMYK if required
- **For client review**: 72-150 DPI, RGB
- **Include bleed**: 0.125" minimum
- **Embed fonts**: Ensures consistency

## Troubleshooting

### Photos won't import
- Check file format (JPG, PNG supported)
- Verify file isn't corrupted
- Check file permissions
- Try smaller batches

### Face detection not working
- Ensure photos have clear faces
- Check lighting and resolution
- Update face detection model
- Try manual grouping

### Application is slow
- Close other applications
- Reduce number of open spreads
- Lower canvas zoom level
- Clear thumbnail cache
- Check available RAM

### Can't see drop zones
- Check if page has template applied
- Try applying a template
- Zoom in for better visibility
- Check canvas zoom level

### Export fails
- Check disk space
- Verify export path exists
- Reduce page count for testing
- Check file permissions
- Try different format

## Getting Help

### Documentation
- Read `FEATURES.md` for detailed feature list
- Check `README.md` for technical details
- Browse code comments for implementation

### Support
- GitHub Issues: Report bugs
- Discussions: Ask questions
- Wiki: Community guides

### Learning Resources
- Tutorial videos (coming soon)
- Sample projects (coming soon)
- Design tips blog (coming soon)

## Next Steps

1. **Complete the Quick Start Tutorial** above
2. **Experiment with templates** to find your style
3. **Try auto-design** to see AI capabilities
4. **Create a sample album** with test photos
5. **Learn keyboard shortcuts** for efficiency
6. **Explore advanced features** in FEATURES.md

Happy designing!
