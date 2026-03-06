@echo off
title GaslightingClipboard - Compiler
color 0A

echo.
echo  =========================================
echo   GaslightingClipboard - Build Script
echo  =========================================
echo.

:: Check if dotnet is installed
where dotnet >nul 2>&1
if %errorlevel% neq 0 (
    color 0C
    echo  [ERROR] .NET SDK not found!
    echo.
    echo  Please install .NET 8 SDK from:
    echo  https://dotnet.microsoft.com/download/dotnet/8.0
    echo.
    pause
    exit /b 1
)

echo  [*] .NET SDK found. Starting build...
echo.

:: Clean previous publish output
if exist "bin\Release\net8.0-windows\win-x64\publish\" (
    echo  [*] Cleaning previous build...
    rmdir /s /q "bin\Release\net8.0-windows\win-x64\publish\"
)

echo  [*] Publishing single-file EXE...
echo.

dotnet publish GaslightingClipboard.csproj ^
  -c Release ^
  -r win-x64 ^
  --self-contained true ^
  -p:PublishSingleFile=true ^
  -p:IncludeNativeLibrariesForSelfExtract=true ^
  -p:DebugType=none ^
  -p:DebugSymbols=false

echo.

if %errorlevel% neq 0 (
    color 0C
    echo  [ERROR] Build failed! Check the errors above.
    echo.
    pause
    exit /b 1
)

:: Copy EXE to project root for convenience
copy /y "bin\Release\net8.0-windows\win-x64\publish\GaslightingClipboard.exe" "GaslightingClipboard.exe" >nul

color 0A
echo  =========================================
echo   BUILD SUCCESSFUL!
echo  =========================================
echo.
echo  Output: GaslightingClipboard.exe
echo  (Also in bin\Release\net8.0-windows\win-x64\publish\)
echo.
echo  Your clipboard has been upgraded. Enjoy! :)
echo.
pause