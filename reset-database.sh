#!/bin/bash

# Database Reset Script for Linux/Mac
# This script deletes the database, removes migration files, and rebuilds the project

echo "=== Database Reset Script ==="
echo "This will delete the database and all migration files, then rebuild the project."
echo ""

# Ask for confirmation
read -p "Are you sure you want to continue? (y/N): " confirmation
if [[ ! $confirmation =~ ^[Yy]$ ]]; then
    echo "Operation cancelled."
    exit 1
fi

echo ""
echo "Step 1: Stopping any running instances..."
# Kill any running dotnet processes for this project
pkill -f "dotnet.*printCostTracker" 2>/dev/null || true

echo "Step 2: Deleting database file..."
# Delete the SQLite database file
DB_FILE="PrintCostTracker.db"
if [ -f "$DB_FILE" ]; then
    rm -f "$DB_FILE"
    echo "Database file deleted: $DB_FILE"
else
    echo "Database file not found: $DB_FILE"
fi

echo "Step 3: Removing migration files..."
# Remove all migration files from the Data/Migrations directory
MIGRATIONS_DIR="Data/Migrations"
if [ -d "$MIGRATIONS_DIR" ]; then
    find "$MIGRATIONS_DIR" -name "*.cs" -type f -delete
    echo "Migration files removed from: $MIGRATIONS_DIR"
else
    echo "Migrations directory not found: $MIGRATIONS_DIR"
fi

echo "Step 4: Cleaning build artifacts..."
# Clean the project
if dotnet clean; then
    echo "Project cleaned successfully"
else
    echo "Failed to clean project"
    exit 1
fi

echo "Step 5: Restoring packages..."
# Restore packages
if dotnet restore; then
    echo "Packages restored successfully"
else
    echo "Failed to restore packages"
    exit 1
fi

echo "Step 6: Building project..."
# Build the project
if dotnet build; then
    echo "Project built successfully"
else
    echo "Failed to build project"
    exit 1
fi

echo ""
echo "=== Database Reset Complete ==="
echo "The database has been reset and the project has been rebuilt."
echo "You can now run 'dotnet run' to start the application with a fresh database." 