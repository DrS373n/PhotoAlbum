# Contributing to PhotoAlbum

Thank you for your interest in contributing to PhotoAlbum! This document provides guidelines and information for contributors.

## Code of Conduct

We are committed to providing a welcoming and inclusive environment. Please be respectful and professional in all interactions.

## How to Contribute

### Reporting Bugs

If you find a bug, please create an issue with:
- Clear, descriptive title
- Steps to reproduce the problem
- Expected vs. actual behavior
- Screenshots if applicable
- System information (OS version, .NET version)

### Suggesting Features

Feature suggestions are welcome! Please:
- Check if the feature already exists or is planned
- Clearly describe the feature and its benefits
- Provide examples or mockups if possible
- Explain the use case

### Submitting Code

1. **Fork the repository**
2. **Create a feature branch**
   ```bash
   git checkout -b feature/your-feature-name
   ```
3. **Make your changes**
4. **Write tests** (if applicable)
5. **Commit with clear messages**
   ```bash
   git commit -m "Add feature: description"
   ```
6. **Push to your fork**
   ```bash
   git push origin feature/your-feature-name
   ```
7. **Create a Pull Request**

## Development Setup

### Prerequisites
- Windows 10 1809+ or Windows 11
- .NET 9.0 SDK
- Visual Studio 2022 (recommended) or VS Code
- Windows App SDK workload (for UI development)

### Getting Started

1. Clone the repository:
```bash
git clone https://github.com/DrS373n/PhotoAlbum.git
cd PhotoAlbum
```

2. Restore packages:
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

## Project Structure

```
PhotoAlbum/
├── PhotoAlbum.sln            # Solution file
├── PhotoAlbum.Core/          # Core business logic (platform-agnostic)
│   ├── Models/               # Domain models
│   ├── Services/             # Service implementations
│   └── Interfaces/           # Service contracts
├── PhotoAlbum.App/           # WinUI3 application
│   ├── ViewModels/           # MVVM ViewModels
│   ├── Views/                # XAML views
│   ├── Controls/             # Custom controls
│   └── Helpers/              # Utility classes
└── docs/                     # Documentation
```

## Coding Standards

### C# Style Guide

Follow standard C# conventions:

```csharp
// Use PascalCase for public members
public class AlbumProject
{
    // Use PascalCase for properties
    public string Name { get; set; }
    
    // Use camelCase with _ prefix for private fields
    private readonly IProjectService _projectService;
    
    // Use camelCase for local variables
    var albumName = "My Album";
}
```

### XAML Conventions

```xml
<!-- Use 4-space indentation -->
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto"/>
    </Grid.RowDefinitions>
    
    <!-- Use x:Name for code-behind references -->
    <TextBlock x:Name="TitleText" Text="Title"/>
</Grid>
```

### Comments

- Use XML comments for public APIs
- Add inline comments for complex logic
- Keep comments up-to-date with code changes

```csharp
/// <summary>
/// Creates a new album project
/// </summary>
/// <param name="name">Project name</param>
/// <returns>New album project</returns>
public async Task<AlbumProject> CreateProjectAsync(string name)
{
    // Implementation
}
```

## Testing

### Unit Tests

- Write tests for all new features
- Use xUnit framework
- Mock dependencies with Moq
- Aim for >80% code coverage

```csharp
[Fact]
public async Task CreateProject_ShouldReturnNewProject()
{
    // Arrange
    var service = new ProjectService();
    
    // Act
    var project = await service.CreateProjectAsync("Test");
    
    // Assert
    Assert.NotNull(project);
    Assert.Equal("Test", project.Name);
}
```

### Integration Tests

- Test service interactions
- Test file I/O operations
- Use test data folders

### UI Tests

- Test user workflows
- Validate data binding
- Check command execution

## Pull Request Guidelines

### PR Checklist

Before submitting a PR, ensure:

- [ ] Code builds without errors
- [ ] All tests pass
- [ ] New code has tests
- [ ] Code follows style guidelines
- [ ] XML comments for public APIs
- [ ] No unnecessary dependencies added
- [ ] PR description explains changes
- [ ] Related issue is referenced

### PR Description Template

```markdown
## Description
Brief description of changes

## Related Issue
Fixes #123

## Changes Made
- Change 1
- Change 2

## Testing
How was this tested?

## Screenshots
(if UI changes)

## Checklist
- [ ] Tests added/updated
- [ ] Documentation updated
- [ ] No breaking changes
```

## Areas to Contribute

### High Priority
- [ ] PDF export implementation
- [ ] Undo/redo functionality
- [ ] Template gallery
- [ ] Performance optimization
- [ ] Unit tests

### Medium Priority
- [ ] Additional layout templates
- [ ] Photo filters and effects
- [ ] Export to other formats
- [ ] Localization
- [ ] Accessibility improvements

### Low Priority
- [ ] Plugin system
- [ ] Cloud storage integration
- [ ] Mobile companion app
- [ ] Advanced AI features

## Working with AI Features

### Face Detection

The face detection service uses ONNX Runtime. To add/improve:

1. Choose an ONNX model (RetinaFace, MTCNN, etc.)
2. Place model in `Models/` folder
3. Update `FaceDetectionService` to load model
4. Implement inference logic
5. Test with various photos

### Auto-Design

Improve the auto-design algorithms:

1. Analyze photo composition
2. Consider face positions
3. Balance visual weight
4. Implement layout scoring
5. Test with real wedding photos

## Documentation

### When to Update Documentation

Update docs when:
- Adding new features
- Changing existing behavior
- Fixing bugs that affect usage
- Improving architecture

### Documentation Files

- `README.md`: Overview and quick start
- `FEATURES.md`: Detailed feature list
- `GETTING_STARTED.md`: Tutorial and guide
- `ARCHITECTURE.md`: Technical details
- Code comments: API documentation

## Issue Labels

- `bug`: Something isn't working
- `enhancement`: New feature or request
- `documentation`: Documentation improvements
- `good first issue`: Good for newcomers
- `help wanted`: Extra attention needed
- `question`: Further information requested
- `wontfix`: Will not be worked on

## Git Workflow

### Branching Strategy

- `main`: Stable release branch
- `develop`: Development branch
- `feature/*`: New features
- `bugfix/*`: Bug fixes
- `hotfix/*`: Critical fixes

### Commit Messages

Follow conventional commits:

```
feat: Add PDF export functionality
fix: Correct face detection bounds
docs: Update API documentation
test: Add unit tests for ProjectService
refactor: Simplify template selection logic
style: Format code per style guide
```

## Release Process

1. Update version in project files
2. Update CHANGELOG.md
3. Create release branch
4. Test thoroughly
5. Merge to main
6. Tag release
7. Build and publish

## Getting Help

- **Questions**: Open a discussion
- **Bugs**: Create an issue
- **Feature ideas**: Start a discussion
- **Code help**: Comment on PR

## Recognition

Contributors will be recognized in:
- CONTRIBUTORS.md file
- Release notes
- Project README

## License

By contributing, you agree that your contributions will be licensed under the same license as the project.

## Thank You!

Your contributions make PhotoAlbum better for everyone. We appreciate your time and effort!
