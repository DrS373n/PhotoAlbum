@echo off
REM Build script for PhotoAlbum on Windows

echo ====================================
echo Building PhotoAlbum
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

echo Restoring NuGet packages...
dotnet restore
if %errorlevel% neq 0 (
    echo Error: Failed to restore packages
    pause
    exit /b 1
)

echo.
echo Building solution...
dotnet build --configuration Release
if %errorlevel% neq 0 (
    echo Error: Build failed
    pause
    exit /b 1
)

echo.
echo ====================================
echo Build completed successfully!
echo ====================================
echo.
echo You can now run the application using run.bat
echo or: dotnet run --project PhotoAlbum.App
echo.
pause
