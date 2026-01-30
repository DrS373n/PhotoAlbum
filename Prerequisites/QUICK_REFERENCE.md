# PhotoAlbum Prerequisites - Quick Reference Card

## Installation Commands

### Automated Installation (Recommended)
```powershell
cd Prerequisites
.\Install-Prerequisites.ps1
```

### Development Setup
```powershell
.\Install-Prerequisites.ps1 -Development
```

### Fix NuGet Issues
```powershell
.\Configure-NuGet.ps1
```

### Reset NuGet Cache
```powershell
.\Configure-NuGet.ps1 -Reset
```

### Verify Installation
```powershell
.\Verify-Prerequisites.ps1
```

## Manual Installation Links

| Component | Download Link |
|-----------|--------------|
| .NET 9.0 SDK | https://dotnet.microsoft.com/download/dotnet/9.0 |
| Visual Studio 2022 | https://visualstudio.microsoft.com/downloads/ |
| Windows App SDK | https://learn.microsoft.com/windows/apps/windows-app-sdk/downloads |

## Common Issues & Quick Fixes

### NU1100: Unable to resolve packages
```powershell
.\Configure-NuGet.ps1 -Reset
cd ..
dotnet restore --force --no-cache
```

### .NET SDK not found
```powershell
.\Install-Prerequisites.ps1 -Force
# Then restart PowerShell
```

### Build fails after restore
```powershell
dotnet clean
dotnet restore
dotnet build
```

### Behind corporate proxy
Edit `%APPDATA%\NuGet\NuGet.config`:
```xml
<config>
  <add key="http_proxy" value="http://proxy:port" />
</config>
```

## Build Commands

### Restore packages
```bash
dotnet restore
```

### Build solution
```bash
dotnet build
```

### Run application
```bash
dotnet run --project PhotoAlbum.App
```

### Clean build
```bash
dotnet clean
dotnet build
```

## System Requirements

- **OS**: Windows 10 (1809+) or Windows 11
- **SDK**: .NET 9.0
- **RAM**: 4GB minimum, 8GB recommended
- **Disk**: 2GB for prerequisites + dependencies
- **Internet**: Required for initial setup

## Troubleshooting Resources

| Issue Type | Resource |
|------------|----------|
| NuGet Errors | [TROUBLESHOOTING.md](TROUBLESHOOTING.md) |
| Installation | [README.md](README.md) |
| Getting Started | [../GETTING_STARTED.md](../GETTING_STARTED.md) |
| Build Issues | Run `.\Verify-Prerequisites.ps1` |

## Verification Checklist

After installation, verify:

- [ ] `dotnet --version` shows 9.0.x
- [ ] `dotnet --list-sdks` includes .NET 9.0
- [ ] `dotnet nuget list source` shows nuget.org
- [ ] `dotnet restore` completes successfully
- [ ] `dotnet build` completes successfully

## Support

If issues persist after trying the above:

1. Run with verbose output:
   ```powershell
   dotnet restore --verbosity detailed > restore.log 2>&1
   ```

2. Check the log file for specific errors

3. See [TROUBLESHOOTING.md](TROUBLESHOOTING.md) for detailed solutions

4. Create an issue on GitHub with:
   - Output of `dotnet --info`
   - Output of `dotnet nuget list source`
   - Relevant error messages

---

**Quick Start After Prerequisites**
```powershell
cd Prerequisites
.\Install-Prerequisites.ps1
.\Verify-Prerequisites.ps1
cd ..
dotnet restore
dotnet build
dotnet run --project PhotoAlbum.App
```

**Print this card and keep it handy while setting up PhotoAlbum!**
