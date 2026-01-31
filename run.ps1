# Run script for PhotoAlbum on Windows (PowerShell)

Write-Host "====================================" -ForegroundColor Cyan
Write-Host "Running PhotoAlbum" -ForegroundColor Cyan
Write-Host "====================================" -ForegroundColor Cyan
Write-Host ""

# Check if .NET SDK is installed
try {
    $dotnetVersion = dotnet --version
    Write-Host "Found .NET SDK version: $dotnetVersion" -ForegroundColor Green
} catch {
    Write-Host "Error: .NET SDK is not installed or not in PATH" -ForegroundColor Red
    Write-Host "Please install .NET 9.0 SDK from https://dotnet.microsoft.com/download/dotnet/9.0" -ForegroundColor Yellow
    Read-Host -Prompt "Press Enter to exit"
    exit 1
}

Write-Host ""
Write-Host "Starting PhotoAlbum application..." -ForegroundColor Yellow
Write-Host ""

dotnet run --project PhotoAlbum.App

if ($LASTEXITCODE -ne 0) {
    Write-Host ""
    Write-Host "Error: Failed to run the application" -ForegroundColor Red
    Write-Host "Please ensure you have built the project first using .\build.ps1 or build.bat" -ForegroundColor Yellow
    Read-Host -Prompt "Press Enter to exit"
    exit 1
}
