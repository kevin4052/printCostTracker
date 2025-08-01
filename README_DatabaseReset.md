# Database Reset for Development

This document explains how to completely reset the database for development purposes.

## Overview

The database reset process involves:
1. Stopping any running application instances
2. Deleting the SQLite database file
3. Removing all migration files
4. Cleaning and rebuilding the project
5. Creating a fresh database with initial data

## Available Scripts

### For Windows (PowerShell)
```powershell
.\reset-database.ps1
```

### For Linux/Mac (Bash)
```bash
./reset-database.sh
```

## What the Scripts Do

1. **Stop Running Instances**: Kills any running dotnet processes for this project
2. **Delete Database**: Removes the `PrintCostTracker.db` file
3. **Remove Migrations**: Deletes all `.cs` files in the `Data/Migrations` directory
4. **Clean Project**: Runs `dotnet clean` to remove build artifacts
5. **Restore Packages**: Runs `dotnet restore` to ensure all packages are available
6. **Build Project**: Runs `dotnet build` to rebuild the project

## Initial Data

When you run `dotnet run` after the reset, the application will:
- Create a new database using `EnsureCreated()`
- Seed the database with initial data defined in `PrintCostTrackerContext.OnModelCreating()`

### Sample Data Included

- **Materials**: PLA, ABS, PETG, TPU, Resin
- **Printers**: Ender 3 Pro, Elegoo Mars 3, Prusa i3 MK3S+
- **Sample Print Jobs**: Test Cube, Phone Stand, Miniature Figure
- **Sample Costs**: Electricity costs for each print job

## Usage

### Complete Reset (Recommended for Development)

1. **Run the reset script**:
   ```bash
   # Linux/Mac
   ./reset-database.sh
   
   # Windows
   .\reset-database.ps1
   ```

2. **Start the application**:
   ```bash
   dotnet run
   ```

### Manual Reset (Alternative)

If you prefer to do it manually:

```bash
# Stop any running instances
pkill -f "dotnet.*printCostTracker"  # Linux/Mac
# or use Task Manager on Windows

# Delete database and migrations
rm -f PrintCostTracker.db
rm -f Data/Migrations/*.cs

# Clean and rebuild
dotnet clean
dotnet restore
dotnet build

# Start the application
dotnet run
```

## When to Use

Use the database reset script when you need to:

- **Start fresh**: Remove all data and start with a clean slate
- **Schema changes**: After modifying entity models
- **Development testing**: Reset to a known state for testing
- **Migration issues**: When migrations become inconsistent

## Safety

⚠️ **Warning**: This process will **permanently delete** all data in the database. Make sure to:

- Backup any important data before running the script
- Only use this in development environments
- Never run this in production

## Troubleshooting

### Script Permission Issues (Linux/Mac)
```bash
chmod +x reset-database.sh
```

### PowerShell Execution Policy (Windows)
If you get execution policy errors:
```powershell
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
```

### Database Locked
If the database is locked:
1. Stop all running instances of the application
2. Wait a few seconds
3. Run the reset script again

### Build Errors
If you get build errors after reset:
1. Delete the `bin` and `obj` directories
2. Run `dotnet restore`
3. Run `dotnet build`

## File Structure

```
├── reset-database.sh          # Linux/Mac reset script
├── reset-database.ps1         # Windows reset script
├── PrintCostTracker.db        # SQLite database (deleted by script)
└── Data/
    └── Migrations/            # Migration files (deleted by script)
        └── *.cs
``` 