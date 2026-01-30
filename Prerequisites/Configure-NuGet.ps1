<#
.SYNOPSIS
    Configures and verifies NuGet package sources for PhotoAlbum.

.DESCRIPTION
    This script ensures NuGet package sources are properly configured and can be accessed.
    It also clears any corrupted NuGet cache that might cause package resolution issues.

.PARAMETER Reset
    Clear all NuGet cache and reset to defaults.

.EXAMPLE
    .\Configure-NuGet.ps1
    Configures NuGet sources.

.EXAMPLE
    .\Configure-NuGet.ps1 -Reset
    Clears cache and resets NuGet configuration.
#>

[CmdletBinding()]
param(
    [switch]$Reset
)

$ErrorActionPreference = "Continue"

# Helper Functions
function Write-Header {
    param([string]$Message)
    Write-Host "`n========================================" -ForegroundColor Cyan
    Write-Host $Message -ForegroundColor Cyan
    Write-Host "========================================`n" -ForegroundColor Cyan
}

function Write-Success {
    param([string]$Message)
    Write-Host "[✓] $Message" -ForegroundColor Green
}

function Write-WarningMessage {
    param([string]$Message)
    Write-Host "[!] $Message" -ForegroundColor Yellow
}

function Write-ErrorMessage {
    param([string]$Message)
    Write-Host "[✗] $Message" -ForegroundColor Red
}

function Write-Info {
    param([string]$Message)
    Write-Host "[i] $Message" -ForegroundColor Cyan
}

function Test-InternetConnection {
    Write-Header "Checking Internet Connection"
    
    try {
        $response = Test-Connection -ComputerName "api.nuget.org" -Count 1 -Quiet -ErrorAction SilentlyContinue
        
        if ($response) {
            Write-Success "Internet connection is available"
            return $true
        }
        else {
            Write-WarningMessage "Cannot reach api.nuget.org"
            Write-Host "  This may indicate network issues or firewall blocking NuGet." -ForegroundColor Yellow
            return $false
        }
    }
    catch {
        Write-WarningMessage "Unable to test internet connection"
        return $false
    }
}

function Get-NuGetSources {
    Write-Header "Checking NuGet Package Sources"
    
    try {
        $sources = dotnet nuget list source 2>&1
        
        if ($LASTEXITCODE -eq 0) {
            Write-Success "NuGet sources configured:"
            Write-Host $sources -ForegroundColor Gray
            return $true
        }
        else {
            Write-ErrorMessage "Failed to list NuGet sources"
            Write-Host $sources -ForegroundColor Red
            return $false
        }
    }
    catch {
        Write-ErrorMessage "Error checking NuGet sources: $_"
        return $false
    }
}

function Add-DefaultNuGetSource {
    Write-Header "Adding Default NuGet Source"
    
    try {
        # Check if nuget.org source exists
        $sources = dotnet nuget list source 2>&1 | Out-String
        
        if ($sources -notmatch "nuget.org") {
            Write-Info "Adding nuget.org as package source..."
            dotnet nuget add source "https://api.nuget.org/v3/index.json" --name "nuget.org" 2>&1
            
            if ($LASTEXITCODE -eq 0) {
                Write-Success "nuget.org source added successfully"
                return $true
            }
            else {
                Write-ErrorMessage "Failed to add nuget.org source"
                return $false
            }
        }
        else {
            Write-Success "nuget.org source is already configured"
            return $true
        }
    }
    catch {
        Write-ErrorMessage "Error adding NuGet source: $_"
        return $false
    }
}

function Clear-NuGetCache {
    Write-Header "Clearing NuGet Cache"
    
    if ($Reset) {
        try {
            Write-Info "Clearing all NuGet caches..."
            dotnet nuget locals all --clear 2>&1
            
            if ($LASTEXITCODE -eq 0) {
                Write-Success "NuGet cache cleared successfully"
                Write-Host "  This will force re-download of all packages on next restore." -ForegroundColor Gray
                return $true
            }
            else {
                Write-ErrorMessage "Failed to clear NuGet cache"
                return $false
            }
        }
        catch {
            Write-ErrorMessage "Error clearing NuGet cache: $_"
            return $false
        }
    }
    else {
        Write-Info "Skipping cache clear (use -Reset to clear cache)"
        return $true
    }
}

function Test-PackageRestore {
    Write-Header "Testing Package Restore"
    
    $projectRoot = Split-Path -Parent $PSScriptRoot
    $coreProject = Join-Path $projectRoot "PhotoAlbum.Core\PhotoAlbum.Core.csproj"
    
    if (-not (Test-Path $coreProject)) {
        Write-WarningMessage "PhotoAlbum.Core.csproj not found, skipping restore test"
        return $true
    }
    
    try {
        Write-Info "Attempting to restore packages for PhotoAlbum.Core..."
        $output = dotnet restore $coreProject --verbosity minimal 2>&1
        
        if ($LASTEXITCODE -eq 0) {
            Write-Success "Package restore successful!"
            return $true
        }
        else {
            Write-ErrorMessage "Package restore failed"
            Write-Host "`nError output:" -ForegroundColor Yellow
            $output | ForEach-Object { Write-Host "  $_" -ForegroundColor Yellow }
            Write-Host "`nCommon causes:" -ForegroundColor Yellow
            Write-Host "  1. No internet connection or firewall blocking NuGet" -ForegroundColor Gray
            Write-Host "  2. Corporate proxy not configured" -ForegroundColor Gray
            Write-Host "  3. NuGet package source is offline or unreachable" -ForegroundColor Gray
            Write-Host "  4. Missing .NET runtime version" -ForegroundColor Gray
            return $false
        }
    }
    catch {
        Write-ErrorMessage "Error during package restore: $_"
        return $false
    }
}

function Show-ProxyInstructions {
    Write-Header "Corporate Proxy Configuration"
    
    Write-Host "If you're behind a corporate proxy, configure it for NuGet:" -ForegroundColor Yellow
    Write-Host "`n1. Create or edit NuGet.config file:" -ForegroundColor White
    Write-Host "   Location: %APPDATA%\NuGet\NuGet.config" -ForegroundColor Gray
    Write-Host "`n2. Add proxy configuration:" -ForegroundColor White
    Write-Host @"
   <?xml version="1.0" encoding="utf-8"?>
   <configuration>
     <config>
       <add key="http_proxy" value="http://proxy:port" />
       <add key="https_proxy" value="http://proxy:port" />
     </config>
   </configuration>
"@ -ForegroundColor Gray
    
    Write-Host "`n3. Or use environment variables:" -ForegroundColor White
    Write-Host '   $env:HTTP_PROXY = "http://proxy:port"' -ForegroundColor Gray
    Write-Host '   $env:HTTPS_PROXY = "http://proxy:port"' -ForegroundColor Gray
}

function Show-TroubleshootingSteps {
    Write-Header "Troubleshooting Steps"
    
    Write-Host "If you're still experiencing issues, try these steps:" -ForegroundColor Yellow
    Write-Host "`n1. Clear NuGet cache and try again:" -ForegroundColor White
    Write-Host "   .\Configure-NuGet.ps1 -Reset" -ForegroundColor Cyan
    Write-Host "   cd .. && dotnet restore" -ForegroundColor Cyan
    
    Write-Host "`n2. Verify internet connectivity:" -ForegroundColor White
    Write-Host "   Test-Connection api.nuget.org" -ForegroundColor Cyan
    
    Write-Host "`n3. Check firewall/antivirus settings:" -ForegroundColor White
    Write-Host "   - Allow dotnet.exe and NuGet.exe" -ForegroundColor Gray
    Write-Host "   - Allow access to api.nuget.org" -ForegroundColor Gray
    
    Write-Host "`n4. Update NuGet to latest version:" -ForegroundColor White
    Write-Host "   dotnet tool update -g nuget" -ForegroundColor Cyan
    
    Write-Host "`n5. Check .NET SDK installation:" -ForegroundColor White
    Write-Host "   dotnet --info" -ForegroundColor Cyan
    Write-Host "   Ensure .NET 9.0 SDK is listed" -ForegroundColor Gray
    
    Write-Host "`n6. Try manual NuGet restore:" -ForegroundColor White
    Write-Host "   dotnet restore --force --no-cache" -ForegroundColor Cyan
}

# Main Process
Write-Host @"

╔═══════════════════════════════════════════════════════════╗
║                                                           ║
║          PhotoAlbum NuGet Configuration Tool              ║
║                                                           ║
╚═══════════════════════════════════════════════════════════╝

"@ -ForegroundColor Cyan

Write-Host "This script will configure NuGet package sources for PhotoAlbum.`n" -ForegroundColor White

# Run configuration steps
$internetOk = Test-InternetConnection
$sourcesOk = Get-NuGetSources
$sourceAdded = Add-DefaultNuGetSource
$cacheCleared = Clear-NuGetCache
$restoreOk = Test-PackageRestore

# Summary
Write-Header "Configuration Summary"

if ($internetOk -and $sourcesOk -and $sourceAdded -and $restoreOk) {
    Write-Success "NuGet is configured correctly!"
    Write-Host "`nYou can now build the project:" -ForegroundColor Green
    Write-Host "  cd $(Split-Path -Parent $PSScriptRoot)" -ForegroundColor Gray
    Write-Host "  dotnet build" -ForegroundColor Gray
}
else {
    Write-WarningMessage "Some issues were detected during configuration."
    
    if (-not $internetOk) {
        Write-Host "`nInternet connectivity issues detected." -ForegroundColor Yellow
        Write-Host "Please check your network connection and firewall settings." -ForegroundColor Yellow
    }
    
    if (-not $restoreOk) {
        Write-Host "`nPackage restore failed." -ForegroundColor Yellow
        Show-ProxyInstructions
        Show-TroubleshootingSteps
    }
}

Write-Host ""
