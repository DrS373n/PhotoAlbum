@echo off
REM PhotoAlbum Prerequisites Installer
REM This batch file runs the PowerShell installation script

echo.
echo ========================================
echo   PhotoAlbum Prerequisites Installer
echo ========================================
echo.

REM Check if PowerShell is available
where powershell >nul 2>nul
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: PowerShell is not found on this system.
    echo PowerShell is required to run this installer.
    echo.
    pause
    exit /b 1
)

REM Check if running as administrator
net session >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo WARNING: Not running as Administrator.
    echo Some installations may require administrator privileges.
    echo.
    echo Right-click this file and select "Run as administrator" for best results.
    echo.
    pause
)

REM Run the PowerShell script
echo Running PowerShell installation script...
echo.

powershell -ExecutionPolicy Bypass -File "%~dp0Install-Prerequisites.ps1" %*

if %ERRORLEVEL% NEQ 0 (
    echo.
    echo Installation script completed with errors.
    echo Please review the output above for details.
    echo.
) else (
    echo.
    echo Installation script completed successfully!
    echo.
)

pause
