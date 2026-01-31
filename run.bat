@echo off
REM Run script for PhotoAlbum on Windows

echo ====================================
echo Running PhotoAlbum
echo ====================================
echo.

REM Check if .NET SDK is installed
dotnet --version >nul 2>&1
if %errorlevel% neq 0 (
    echo Error: .NET SDK is not installed or not in PATH
    echo Please install .NET 9.0 SDK from https://dotnet.microsoft.com/download/dotnet/9.0
    pause
    exit /b 1
)

echo Starting PhotoAlbum application...
echo.
dotnet run --project PhotoAlbum.App

if %errorlevel% neq 0 (
    echo.
    echo Error: Failed to run the application
    echo Please ensure you have built the project first using build.bat
    pause
    exit /b 1
)
