<#
.SYNOPSIS
    Installs prerequisites for PhotoAlbum application.

.DESCRIPTION
    This script checks for and installs the required prerequisites for building and running PhotoAlbum:
    - .NET 9.0 SDK
    - Windows App SDK (for development)
    
.PARAMETER Development
    Include development tools (Visual Studio workload information).

.PARAMETER SkipDotNet
    Skip .NET SDK installation check and installation.

.PARAMETER Force
    Force download and installation even if components are already installed.

.EXAMPLE
    .\Install-Prerequisites.ps1
    Installs prerequisites for running PhotoAlbum.

.EXAMPLE
    .\Install-Prerequisites.ps1 -Development
    Installs prerequisites for developing PhotoAlbum.

.EXAMPLE
    .\Install-Prerequisites.ps1 -Force
    Forces reinstallation of all prerequisites.
#>

[CmdletBinding()]
param(
    [switch]$Development,
    [switch]$SkipDotNet,
    [switch]$Force
)

$ErrorActionPreference = "Stop"

# Script configuration
$DotNetVersion = "9.0"
$DotNetDownloadUrl = "https://dotnet.microsoft.com/download/dotnet/9.0"
$WindowsAppSdkVersion = "1.6"
$InstallerDir = Join-Path $PSScriptRoot "Installers"

# Ensure Installers directory exists
if (-not (Test-Path $InstallerDir)) {
    New-Item -ItemType Directory -Path $InstallerDir -Force | Out-Null
}

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

function Write-Warning {
    param([string]$Message)
    Write-Host "[!] $Message" -ForegroundColor Yellow
}

function Write-Error {
    param([string]$Message)
    Write-Host "[✗] $Message" -ForegroundColor Red
}

function Test-DotNetInstalled {
    try {
        $dotnetVersion = dotnet --version 2>$null
        if ($dotnetVersion) {
            $majorVersion = $dotnetVersion.Split('.')[0]
            if ($majorVersion -eq "9") {
                return $true
            }
        }
    }
    catch {
        return $false
    }
    return $false
}

function Get-Architecture {
    $arch = [System.Environment]::GetEnvironmentVariable("PROCESSOR_ARCHITECTURE")
    switch ($arch) {
        "AMD64" { return "x64" }
        "ARM64" { return "arm64" }
        "x86" { return "x86" }
        default { return "x64" }
    }
}

function Install-DotNetSDK {
    Write-Header "Installing .NET 9.0 SDK"
    
    if (Test-DotNetInstalled -and -not $Force) {
        Write-Success ".NET 9.0 SDK is already installed."
        $version = dotnet --version
        Write-Host "  Current version: $version" -ForegroundColor Gray
        return
    }

    Write-Host "Downloading .NET 9.0 SDK installer..." -ForegroundColor Yellow
    
    $arch = Get-Architecture
    $installerName = "dotnet-sdk-9.0-win-$arch.exe"
    $installerPath = Join-Path $InstallerDir $installerName
    
    # Download URLs for different architectures
    $downloadUrls = @{
        "x64" = "https://download.visualstudio.microsoft.com/download/pr/42f39ab2-ff0a-4b93-8dfe-26c6543ce3f9/7d2822d57e3fb02981484e8d79c8c26f/dotnet-sdk-9.0.101-win-x64.exe"
        "x86" = "https://download.visualstudio.microsoft.com/download/pr/d9d43c59-b9f4-47b7-a520-da3a7fa255dc/8f77dd8e2e84d5e5d8c8e19b3f81b9e8/dotnet-sdk-9.0.101-win-x86.exe"
        "arm64" = "https://download.visualstudio.microsoft.com/download/pr/f55ee1e8-e3d5-4c0f-ba15-f8dbcf8c1c5e/8e0d2e3c5e3f5c0e5e0e0e0e0e0e0e0e/dotnet-sdk-9.0.101-win-arm64.exe"
    }
    
    $downloadUrl = $downloadUrls[$arch]
    
    Write-Host "  Architecture: $arch" -ForegroundColor Gray
    Write-Host "  Installer: $installerName" -ForegroundColor Gray
    Write-Host "  Download URL: $downloadUrl" -ForegroundColor Gray
    
    try {
        # Download installer
        Write-Host "`nDownloading installer..." -ForegroundColor Yellow
        Invoke-WebRequest -Uri $downloadUrl -OutFile $installerPath -UseBasicParsing
        Write-Success "Installer downloaded successfully."
        
        # Run installer
        Write-Host "`nRunning installer..." -ForegroundColor Yellow
        Write-Host "  NOTE: This may take several minutes and will open an installation window." -ForegroundColor Gray
        Write-Host "  Please follow the installation prompts." -ForegroundColor Gray
        
        $process = Start-Process -FilePath $installerPath -ArgumentList "/quiet", "/norestart" -Wait -PassThru
        
        if ($process.ExitCode -eq 0) {
            Write-Success ".NET 9.0 SDK installed successfully!"
            Write-Host "  Please restart your terminal/PowerShell to use the new SDK." -ForegroundColor Yellow
        }
        elseif ($process.ExitCode -eq 3010) {
            Write-Success ".NET 9.0 SDK installed successfully!"
            Write-Warning "A system restart is required to complete the installation."
        }
        else {
            Write-Error "Installation failed with exit code: $($process.ExitCode)"
            Write-Host "  You can run the installer manually from: $installerPath" -ForegroundColor Yellow
        }
    }
    catch {
        Write-Error "Failed to download or install .NET SDK: $_"
        Write-Host "`nYou can download it manually from:" -ForegroundColor Yellow
        Write-Host "  $DotNetDownloadUrl" -ForegroundColor Cyan
    }
}

function Show-WindowsAppSdkInfo {
    Write-Header "Windows App SDK Information"
    
    Write-Host "Windows App SDK is required for building and running PhotoAlbum." -ForegroundColor Yellow
    Write-Host "`nThere are two ways to get Windows App SDK:" -ForegroundColor Yellow
    
    Write-Host "`n1. Install Visual Studio 2022 (Recommended for Development):" -ForegroundColor White
    Write-Host "   - Download from: https://visualstudio.microsoft.com/downloads/" -ForegroundColor Cyan
    Write-Host "   - During installation, select these workloads:" -ForegroundColor Gray
    Write-Host "     • .NET desktop development" -ForegroundColor Gray
    Write-Host "     • Windows application development (includes Windows App SDK)" -ForegroundColor Gray
    
    Write-Host "`n2. Install Windows App SDK Runtime (For Running Only):" -ForegroundColor White
    Write-Host "   - Download from: https://learn.microsoft.com/windows/apps/windows-app-sdk/downloads" -ForegroundColor Cyan
    Write-Host "   - Version required: $WindowsAppSdkVersion or later" -ForegroundColor Gray
    
    if ($Development) {
        Write-Host "`nAs a developer, we recommend installing Visual Studio 2022 with the required workloads." -ForegroundColor Yellow
        Write-Host "This provides the best development experience for PhotoAlbum." -ForegroundColor Yellow
    }
}

function Show-Summary {
    Write-Header "Installation Summary"
    
    # Check .NET
    if (Test-DotNetInstalled) {
        Write-Success ".NET 9.0 SDK is installed"
        $version = dotnet --version
        Write-Host "  Version: $version" -ForegroundColor Gray
    }
    else {
        Write-Warning ".NET 9.0 SDK not detected"
        Write-Host "  Please restart your terminal and run 'dotnet --version' to verify." -ForegroundColor Yellow
    }
    
    # Windows App SDK
    Write-Host "`nWindows App SDK:" -ForegroundColor White
    Write-Host "  Please install via Visual Studio 2022 or download the standalone runtime." -ForegroundColor Yellow
    
    # Next steps
    Write-Host "`nNext Steps:" -ForegroundColor Cyan
    Write-Host "  1. Close and reopen your terminal/PowerShell" -ForegroundColor Gray
    Write-Host "  2. Run .\Verify-Prerequisites.ps1 to verify installation" -ForegroundColor Gray
    Write-Host "  3. Navigate to the project root and run: dotnet build" -ForegroundColor Gray
    
    if ($Development) {
        Write-Host "  4. Open PhotoAlbum.sln in Visual Studio 2022" -ForegroundColor Gray
    }
}

# Main Installation Process
Write-Host @"

╔═══════════════════════════════════════════════════════════╗
║                                                           ║
║           PhotoAlbum Prerequisites Installer              ║
║                                                           ║
╚═══════════════════════════════════════════════════════════╝

"@ -ForegroundColor Cyan

Write-Host "This script will install the required prerequisites for PhotoAlbum.`n" -ForegroundColor White

# Check if running as administrator
$isAdmin = ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)
if (-not $isAdmin) {
    Write-Warning "Not running as Administrator. Some installations may require elevation."
    Write-Host "  Consider running PowerShell as Administrator for best results.`n" -ForegroundColor Yellow
}

# Install .NET SDK
if (-not $SkipDotNet) {
    Install-DotNetSDK
}

# Show Windows App SDK information
Show-WindowsAppSdkInfo

# Configure NuGet
Write-Header "Configuring NuGet Package Sources"
Write-Host "Running NuGet configuration script..." -ForegroundColor Yellow
Write-Host "This ensures package sources are properly configured.`n" -ForegroundColor Gray

$configureScript = Join-Path $PSScriptRoot "Configure-NuGet.ps1"
if (Test-Path $configureScript) {
    & $configureScript
}
else {
    Write-Warning "Configure-NuGet.ps1 not found. You may need to configure NuGet manually."
    Write-Host "  See Prerequisites\TROUBLESHOOTING.md for help with NuGet issues." -ForegroundColor Yellow
}

# Show summary
Show-Summary

Write-Host "`nInstallation script completed!" -ForegroundColor Green
Write-Host "Please check the summary above for next steps.`n" -ForegroundColor White
