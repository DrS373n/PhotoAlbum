# Troubleshooting NuGet Package Resolution Errors

This guide helps you resolve NuGet package resolution errors (NU1100) when building PhotoAlbum.

## Common Error

```
error NU1100: Unable to resolve 'PackageName (>= Version)' for 'net9.0'
```

## Quick Fix

Run the NuGet configuration script:

```powershell
cd Prerequisites
.\Configure-NuGet.ps1
```

If that doesn't work, try resetting NuGet cache:

```powershell
.\Configure-NuGet.ps1 -Reset
```

## Detailed Solutions

### Solution 1: Clear NuGet Cache

Corrupted NuGet cache can cause package resolution issues.

```powershell
# Clear all NuGet caches
dotnet nuget locals all --clear

# Try restore again
dotnet restore
```

### Solution 2: Verify NuGet Sources

Ensure NuGet.org is configured as a package source.

```powershell
# List current sources
dotnet nuget list source

# Add nuget.org if missing
dotnet nuget add source https://api.nuget.org/v3/index.json --name nuget.org

# Enable the source if disabled
dotnet nuget enable source nuget.org
```

### Solution 3: Check Internet Connection

NuGet needs internet access to download packages.

```powershell
# Test connection to NuGet.org
Test-Connection api.nuget.org

# If this fails, check:
# - Your internet connection
# - Firewall settings
# - Proxy configuration
```

### Solution 4: Configure Corporate Proxy

If you're behind a corporate proxy:

**Method 1: NuGet.config file**

Create/edit `%APPDATA%\NuGet\NuGet.config`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <config>
    <add key="http_proxy" value="http://proxy-server:port" />
    <add key="https_proxy" value="http://proxy-server:port" />
  </config>
  <packageSources>
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" protocolVersion="3" />
  </packageSources>
</configuration>
```

**Method 2: Environment Variables**

```powershell
$env:HTTP_PROXY = "http://proxy-server:port"
$env:HTTPS_PROXY = "http://proxy-server:port"
```

### Solution 5: Restore with Force

Force a complete package restore:

```powershell
# Navigate to project root
cd ..

# Force restore without cache
dotnet restore --force --no-cache

# If that works, try building
dotnet build
```

### Solution 6: Restore Individual Projects

Sometimes restoring each project separately helps:

```powershell
# Restore Core project
dotnet restore PhotoAlbum.Core/PhotoAlbum.Core.csproj

# Restore App project
dotnet restore PhotoAlbum.App/PhotoAlbum.App.csproj

# Build solution
dotnet build
```

### Solution 7: Check .NET SDK Installation

Ensure .NET 9.0 SDK is properly installed:

```powershell
# Check installed SDKs
dotnet --list-sdks

# Should show: 9.0.x [install path]
# If not, reinstall .NET 9.0 SDK
cd Prerequisites
.\Install-Prerequisites.ps1 -Force
```

### Solution 8: Check .NET SDK Version

Ensure you have the latest .NET 9.0 SDK:

```powershell
# Check current SDK version
dotnet --version

# Check all installed SDKs
dotnet --list-sdks

# If .NET 9.0 is not the latest, reinstall
cd Prerequisites
.\Install-Prerequisites.ps1 -Force
```

### Solution 9: Disable Parallel Restore

Sometimes parallel restore causes issues:

```powershell
dotnet restore --disable-parallel
```

### Solution 10: Check for Offline Packages

If you have offline package sources configured, they might be causing issues:

```powershell
# List all sources
dotnet nuget list source

# Disable offline sources temporarily
dotnet nuget disable source <offline-source-name>

# Try restore
dotnet restore

# Re-enable if needed
dotnet nuget enable source <offline-source-name>
```

## Firewall/Antivirus Configuration

If your firewall or antivirus is blocking NuGet:

1. **Add exceptions for:**
   - `dotnet.exe` (usually in `C:\Program Files\dotnet\`)
   - NuGet.exe
   - Allow outbound HTTPS to `api.nuget.org`
   - Allow outbound HTTPS to `*.nuget.org`

2. **Common firewall ports:**
   - HTTPS: 443
   - HTTP: 80

## Specific Package Issues

### Microsoft.WindowsAppSDK

If only Windows App SDK packages fail:

1. Ensure you're running on Windows 10 1809 or later
2. Install Visual Studio 2022 with "Windows application development" workload
3. Or install standalone Windows App SDK runtime

### Microsoft.ML.OnnxRuntime

If ONNX Runtime packages fail:

1. Clear NuGet cache: `dotnet nuget locals all --clear`
2. Try restore with verbose logging: `dotnet restore --verbosity detailed`
3. Check the detailed output for the exact failure reason

### Platform-Specific Packages

If you see errors for `win-x86`, `win-x64`, or `win-arm64`:

1. This is normal - .NET restores packages for all configured platforms
2. As long as packages for YOUR platform restore, you can ignore others
3. To build for specific platform only:
   ```powershell
   dotnet build -r win-x64
   ```

## Getting More Information

Run restore with detailed logging to see exact error:

```powershell
dotnet restore --verbosity detailed > restore-log.txt 2>&1
```

Then review `restore-log.txt` for detailed error information.

## Still Having Issues?

If none of the above solutions work:

1. **Check the restore log** for specific errors:
   ```powershell
   dotnet restore --verbosity detailed
   ```

2. **Verify .NET installation**:
   ```powershell
   dotnet --info
   ```
   Should show .NET 9.0 SDK in the list

3. **Try a different network**:
   - Mobile hotspot
   - Different WiFi
   - VPN on/off

4. **Create a test project** to verify NuGet works:
   ```powershell
   dotnet new console -n TestProject
   cd TestProject
   dotnet add package Newtonsoft.Json
   dotnet restore
   ```

5. **Check Windows Updates**:
   - Ensure Windows is up to date
   - Some NuGet features require recent Windows updates

6. **Contact Support**:
   - Create an issue on GitHub with the output of:
     - `dotnet --info`
     - `dotnet nuget list source`
     - Detailed restore log

## Prevention

To avoid these issues in the future:

1. **Always run from Prerequisites directory first**:
   ```powershell
   cd Prerequisites
   .\Configure-NuGet.ps1
   cd ..
   dotnet build
   ```

2. **Keep .NET SDK updated**:
   - Check for updates monthly
   - Use stable releases

3. **Monitor NuGet status**:
   - https://status.nuget.org/
   - Check if NuGet.org is experiencing issues

4. **Maintain clean cache**:
   - Clear cache monthly: `dotnet nuget locals all --clear`
   - Keeps package cache healthy
