<#
.SYNOPSIS
    Verifies that all prerequisites for PhotoAlbum are installed correctly.

.DESCRIPTION
    This script checks if all required components for PhotoAlbum are properly installed:
    - .NET 9.0 SDK
    - Windows App SDK (via Visual Studio or standalone)
    
.PARAMETER Detailed
    Show detailed version information for all components.

.EXAMPLE
    .\Verify-Prerequisites.ps1
    Checks if prerequisites are installed.

.EXAMPLE
    .\Verify-Prerequisites.ps1 -Detailed
    Checks prerequisites and shows detailed version information.
#>

[CmdletBinding()]
param(
    [switch]$Detailed
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

function Write-Failure {
    param([string]$Message)
    Write-Host "[✗] $Message" -ForegroundColor Red
}

function Write-Info {
    param([string]$Message)
    Write-Host "[i] $Message" -ForegroundColor Cyan
}

function Test-DotNetSDK {
    Write-Header "Checking .NET SDK"
    
    try {
        $dotnetVersion = dotnet --version 2>$null
        if ($dotnetVersion) {
            $majorVersion = $dotnetVersion.Split('.')[0]
            
            if ($majorVersion -eq "9") {
                Write-Success ".NET 9.0 SDK is installed"
                Write-Host "  Version: $dotnetVersion" -ForegroundColor Gray
                
                if ($Detailed) {
                    Write-Host "`n  Installed SDKs:" -ForegroundColor Gray
                    dotnet --list-sdks | ForEach-Object { Write-Host "    $_" -ForegroundColor Gray }
                }
                
                return $true
            }
            else {
                Write-Failure ".NET SDK is installed but version $majorVersion is not 9.0"
                Write-Host "  Current version: $dotnetVersion" -ForegroundColor Yellow
                Write-Host "  Required: 9.0.x" -ForegroundColor Yellow
                return $false
            }
        }
        else {
            Write-Failure ".NET SDK is not installed"
            return $false
        }
    }
    catch {
        Write-Failure ".NET SDK not found or not in PATH"
        Write-Host "  Error: $_" -ForegroundColor Red
        return $false
    }
}

function Test-DotNetRuntime {
    Write-Header "Checking .NET Runtime"
    
    try {
        $runtimes = dotnet --list-runtimes 2>$null
        if ($runtimes) {
            $hasNet9 = $false
            
            foreach ($runtime in $runtimes) {
                if ($runtime -match "Microsoft\.NETCore\.App 9\.") {
                    $hasNet9 = $true
                    if (-not $Detailed) { break }
                }
            }
            
            if ($hasNet9) {
                Write-Success ".NET 9.0 Runtime is installed"
                
                if ($Detailed) {
                    Write-Host "`n  Installed Runtimes:" -ForegroundColor Gray
                    $runtimes | ForEach-Object { Write-Host "    $_" -ForegroundColor Gray }
                }
                
                return $true
            }
            else {
                Write-Failure ".NET 9.0 Runtime not found"
                return $false
            }
        }
        else {
            Write-Failure "Unable to list .NET runtimes"
            return $false
        }
    }
    catch {
        Write-Failure "Error checking .NET runtime"
        Write-Host "  Error: $_" -ForegroundColor Red
        return $false
    }
}

function Test-WindowsVersion {
    Write-Header "Checking Windows Version"
    
    try {
        $osVersion = [System.Environment]::OSVersion.Version
        $build = $osVersion.Build
        
        # Windows 10 build 17763 (version 1809) or later is required
        $minBuild = 17763
        
        if ($build -ge $minBuild) {
            Write-Success "Windows version is compatible"
            Write-Host "  Version: $($osVersion.Major).$($osVersion.Minor) (Build $build)" -ForegroundColor Gray
            
            # Friendly name
            if ($build -ge 22000) {
                Write-Host "  OS: Windows 11" -ForegroundColor Gray
            }
            else {
                Write-Host "  OS: Windows 10" -ForegroundColor Gray
            }
            
            return $true
        }
        else {
            Write-Failure "Windows version is too old"
            Write-Host "  Current build: $build" -ForegroundColor Yellow
            Write-Host "  Required build: $minBuild or later (Windows 10 version 1809)" -ForegroundColor Yellow
            return $false
        }
    }
    catch {
        Write-Failure "Error checking Windows version"
        Write-Host "  Error: $_" -ForegroundColor Red
        return $false
    }
}

function Test-VisualStudio {
    Write-Header "Checking Visual Studio (Optional)"
    
    $vsWherePath = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
    
    if (Test-Path $vsWherePath) {
        try {
            $vsInstances = & $vsWherePath -version "[17.0,18.0)" -products * -requires Microsoft.Component.MSBuild -property installationPath 2>$null
            
            if ($vsInstances) {
                Write-Success "Visual Studio 2022 is installed"
                
                if ($Detailed) {
                    $vsInfo = & $vsWherePath -version "[17.0,18.0)" -products * -format json | ConvertFrom-Json
                    foreach ($vs in $vsInfo) {
                        Write-Host "  Path: $($vs.installationPath)" -ForegroundColor Gray
                        Write-Host "  Version: $($vs.installationVersion)" -ForegroundColor Gray
                        Write-Host "  Product: $($vs.displayName)" -ForegroundColor Gray
                    }
                }
                
                return $true
            }
            else {
                Write-Info "Visual Studio 2022 not found (Optional for development)"
                return $false
            }
        }
        catch {
            Write-Info "Unable to determine Visual Studio installation status"
            return $false
        }
    }
    else {
        Write-Info "Visual Studio not installed (Optional for development)"
        return $false
    }
}

function Test-ProjectBuild {
    Write-Header "Checking Project Build Capability"
    
    $projectRoot = Split-Path -Parent $PSScriptRoot
    $slnFile = Join-Path $projectRoot "PhotoAlbum.slnx"
    
    if (-not (Test-Path $slnFile)) {
        Write-Failure "PhotoAlbum.slnx not found in expected location"
        Write-Host "  Expected: $slnFile" -ForegroundColor Yellow
        return $false
    }
    
    Write-Info "Solution file found: PhotoAlbum.slnx"
    
    try {
        Write-Host "  Attempting to restore packages..." -ForegroundColor Gray
        $restoreOutput = dotnet restore $slnFile 2>&1
        
        if ($LASTEXITCODE -eq 0) {
            Write-Success "Package restore successful"
            
            if ($Detailed) {
                Write-Host "`n  Restore output:" -ForegroundColor Gray
                $restoreOutput | ForEach-Object { Write-Host "    $_" -ForegroundColor Gray }
            }
            
            return $true
        }
        else {
            Write-Failure "Package restore failed"
            Write-Host "`n  Error output:" -ForegroundColor Yellow
            $restoreOutput | ForEach-Object { Write-Host "    $_" -ForegroundColor Yellow }
            return $false
        }
    }
    catch {
        Write-Failure "Error during package restore"
        Write-Host "  Error: $_" -ForegroundColor Red
        return $false
    }
}

# Main Verification Process
Write-Host @"

╔═══════════════════════════════════════════════════════════╗
║                                                           ║
║        PhotoAlbum Prerequisites Verification              ║
║                                                           ║
╚═══════════════════════════════════════════════════════════╝

"@ -ForegroundColor Cyan

Write-Host "Verifying PhotoAlbum prerequisites...`n" -ForegroundColor White

# Run all checks
$results = @{
    "WindowsVersion" = Test-WindowsVersion
    "DotNetSDK" = Test-DotNetSDK
    "DotNetRuntime" = Test-DotNetRuntime
    "VisualStudio" = Test-VisualStudio
    "ProjectBuild" = Test-ProjectBuild
}

# Summary
Write-Header "Verification Summary"

$requiredChecks = @("WindowsVersion", "DotNetSDK", "DotNetRuntime", "ProjectBuild")
$allRequiredPassed = $true

foreach ($check in $requiredChecks) {
    if (-not $results[$check]) {
        $allRequiredPassed = $false
    }
}

if ($allRequiredPassed) {
    Write-Success "All required prerequisites are installed!"
    Write-Host "`nYou can now build and run PhotoAlbum:" -ForegroundColor Green
    Write-Host "  cd $(Split-Path -Parent $PSScriptRoot)" -ForegroundColor Gray
    Write-Host "  dotnet build" -ForegroundColor Gray
    Write-Host "  dotnet run --project PhotoAlbum.App" -ForegroundColor Gray
}
else {
    Write-Failure "Some required prerequisites are missing or incorrectly configured."
    Write-Host "`nPlease run the installation script:" -ForegroundColor Yellow
    Write-Host "  .\Install-Prerequisites.ps1" -ForegroundColor Cyan
    Write-Host "`nOr install missing components manually. See Prerequisites\README.md for details." -ForegroundColor Yellow
}

Write-Host "`nDetailed Results:" -ForegroundColor Cyan
foreach ($check in $results.Keys) {
    $status = if ($results[$check]) { "[PASS]" } else { "[FAIL]" }
    $color = if ($results[$check]) { "Green" } else { "Red" }
    
    $optional = if ($check -eq "VisualStudio") { " (Optional)" } else { "" }
    Write-Host "  $status $check$optional" -ForegroundColor $color
}

Write-Host ""

# Exit with appropriate code
if ($allRequiredPassed) {
    exit 0
}
else {
    exit 1
}
