# Database Reset Script for Windows
# This script deletes the database, removes migration files, and rebuilds the project

Write-Host "=== Database Reset Script ===" -ForegroundColor Green
Write-Host "This will delete the database and all migration files, then rebuild the project." -ForegroundColor Yellow
Write-Host ""

# Ask for confirmation
$confirmation = Read-Host "Are you sure you want to continue? (y/N)"
if ($confirmation -ne "y" -and $confirmation -ne "Y") {
    Write-Host "Operation cancelled." -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "Step 1: Stopping any running instances..." -ForegroundColor Cyan
# Kill any running dotnet processes for this project
Get-Process -Name "dotnet" -ErrorAction SilentlyContinue | Where-Object { $_.ProcessName -eq "dotnet" } | Stop-Process -Force -ErrorAction SilentlyContinue

Write-Host "Step 2: Deleting database file..." -ForegroundColor Cyan
# Delete the SQLite database file
$dbFile = "PrintCostTracker.db"
if (Test-Path $dbFile) {
    Remove-Item $dbFile -Force
    Write-Host "Database file deleted: $dbFile" -ForegroundColor Green
} else {
    Write-Host "Database file not found: $dbFile" -ForegroundColor Yellow
}

Write-Host "Step 3: Removing migration files..." -ForegroundColor Cyan
# Remove all migration files from the Migrations directory
$migrationsDir = "Migrations"
if (Test-Path $migrationsDir) {
    Get-ChildItem -Path $migrationsDir -Filter "*.cs" | Remove-Item -Force
    Write-Host "Migration files removed from: $migrationsDir" -ForegroundColor Green
} else {
    Write-Host "Migrations directory not found: $migrationsDir" -ForegroundColor Yellow
}

Write-Host "Step 4: Cleaning build artifacts..." -ForegroundColor Cyan
# Clean the project
dotnet clean
if ($LASTEXITCODE -eq 0) {
    Write-Host "Project cleaned successfully" -ForegroundColor Green
} else {
    Write-Host "Failed to clean project" -ForegroundColor Red
    exit 1
}

Write-Host "Step 5: Restoring packages..." -ForegroundColor Cyan
# Restore packages
dotnet restore
if ($LASTEXITCODE -eq 0) {
    Write-Host "Packages restored successfully" -ForegroundColor Green
} else {
    Write-Host "Failed to restore packages" -ForegroundColor Red
    exit 1
}

Write-Host "Step 6: Building project..." -ForegroundColor Cyan
# Build the project
dotnet build
if ($LASTEXITCODE -eq 0) {
    Write-Host "Project built successfully" -ForegroundColor Green
} else {
    Write-Host "Failed to build project" -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "=== Database Reset Complete ===" -ForegroundColor Green
Write-Host "The database has been reset and the project has been rebuilt." -ForegroundColor Green
Write-Host "You can now run 'dotnet run' to start the application with a fresh database." -ForegroundColor Green 