## Print Cost Tracker

A web app for tracking and estimating the costs of 3D printing jobs. Built with ASP.NET Core 8 and SQLite. Manage materials, printers, and print jobs with basic material cost calculations.

### 🚀 Features

- **Print jobs**: Create and view print jobs
- **Materials**: Manage materials with cost per gram/spool and color
- **Printers**: Track printers and key specs (type, size, power)
- **Cost calculation**: Calculates material cost from weight × cost/gram during job creation
- **DB health endpoint**: `/api/test-db` to verify database connectivity
- **Reset scripts**: Cross‑platform scripts to reset the SQLite database for development

### 🛠️ Technology Stack

- **Backend**: ASP.NET Core 8 (MVC)
- **ORM/DB**: Entity Framework Core 8 + SQLite
- **UI**: Razor Views + Bootstrap
- **Pattern**: MVC with repository layer
- **OS**: Works on Windows, macOS, Linux

### 📋 Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Git

### 🚀 Quick Start

1) Clone

```bash
git clone <repository-url>
cd printCostTracker
```

2) Run

```bash
dotnet restore
dotnet run
```

3) Open the app

By default (from `Properties/launchSettings.json`):
- HTTPS: https://localhost:7118
- HTTP:  http://localhost:5031

On first run, the app will create the database and seed initial data (materials and printers).

### 🔧 Configuration

- Connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=PrintCostTracker.db"
  }
}
```

- Environment: `ASPNETCORE_ENVIRONMENT=Development` in launch profiles

### 📊 Data Models (overview)

- **PrintJob**: `Name`, `Description?`, `MaterialId`, `MaterialWeight`, `EstimatedPrintTime`, `PrinterId?`, `TotalCost`, timestamps, `Costs[]`
  - Material cost = `MaterialWeight × Material.CostPerGram`
- **Material**: `Name`, `Description?`, `MaterialType` (int), `SpoolWeight`, `CostPerGram`, `CostPerSpool`, `Color?`, `IsActive`, timestamps
- **Printer**: `Name`, `Description`, `Brand`, `Model`, `SerialNumber`, `Price`, `PurchaseDate`, `Location`, `Status`, `PrinterType`, `PrinterSize`, `PrintingLifetime`, `PrinterLifetimeCost`, `WattsPerHour`, `CostPerHour`
- **Cost**: `Name`, `Description?`, `Amount`, `Type` (Material, Electricity, Labor, Maintenance, Other), `PrintJobId`, timestamp

### 🗄️ Database

- Created on startup via `EnsureCreated()` in `Program.cs`
- Seeded in `Data/PrintCostTrackerContext.cs`:
  - Materials: PLA, ABS, PETG
  - Printers: BambuLab A1 Combo, Elegoo Mars 3

### 🔁 Reset database (development only)

- Windows (PowerShell):
  ```powershell
  .\reset-database.ps1
  ```
- Linux/macOS (Bash):
  ```bash
  ./reset-database.sh
  ```

What it does: deletes the SQLite DB, removes migrations (if any), cleans, restores, builds. This permanently deletes local data.

### 🧭 Navigation & endpoints

- Default route: `/{controller=Home}/{action=Index}/{id?}` → `Home/Index`
- UI:
  - `Materials` CRUD pages
  - `Printers` CRUD pages
  - `PrintJobs` index and create pages
- Health check: `GET /api/test-db` returns JSON with DB status and counts

### 📁 Project Structure

```
printCostTracker/
├── Controllers/
│   ├── HomeController.cs
│   ├── MaterialsController.cs
│   ├── PrintersController.cs
│   └── PrintJobsController.cs
├── Data/
│   └── PrintCostTrackerContext.cs
├── Models/
│   ├── Cost.cs
│   ├── Material.cs
│   ├── Printer.cs
│   └── PrintJob.cs
├── Repositories/
│   ├── CostRepository.cs
│   ├── MaterialRepository.cs
│   ├── PrinterRepository.cs
│   └── PrintJobRepository.cs
├── Views/
├── wwwroot/
├── Program.cs
├── appsettings.json
├── appsettings.Development.json
├── reset-database.ps1
├── reset-database.sh
└── printCostTracker.csproj
```

### 🚀 Deployment

- Local dev: `dotnet run`
- Publish: `dotnet publish -c Release`

### 🆘 Troubleshooting

- Database locked: stop all running instances, wait a few seconds, retry or run the reset script
- Clean build:
  ```bash
  rm -rf bin obj
  dotnet restore && dotnet build
  ```
- Windows PowerShell policy:
  ```powershell
  Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
  ```

---

Happy 3D printing! 🖨️ 