# Print Cost Tracker

A web-based application for tracking and calculating the costs of 3D printing jobs. Built with ASP.NET Core 8.0 and SQLite, this application helps you manage materials, printers, and print jobs while automatically calculating costs based on material usage, electricity consumption, and other factors.

## 🚀 Features

- **Print Job Management**: Create, view, and track 3D printing jobs
- **Material Tracking**: Manage different printing materials with cost per gram and density
- **Printer Management**: Track multiple printers with specifications and power consumption
- **Cost Calculation**: Automatic cost calculation based on material usage and electricity
- **Database Management**: Easy database reset scripts for development
- **Web Interface**: Clean, responsive web interface for easy access

## 🛠️ Technology Stack

- **Backend**: ASP.NET Core 8.0
- **Database**: SQLite with Entity Framework Core
- **Frontend**: Razor Views with Bootstrap
- **Architecture**: MVC pattern with service layer
- **Development**: Cross-platform (Windows, Linux, macOS)

## 📋 Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Git (for cloning the repository)
- A modern web browser

## 🚀 Quick Start

### 1. Clone the Repository

```bash
git clone <repository-url>
cd printCostTracker
```

### 2. Run the Application

```bash
dotnet restore
dotnet run
```

### 3. Access the Application

Open your browser and navigate to:
- **Local**: https://localhost:5001 or http://localhost:5000
- **Development**: The application will automatically create the database and seed it with sample data

## 📊 Data Models

### PrintJob
- **Name**: Job name and description
- **Material**: Associated printing material
- **Costs**: Breakdown of all costs (material, electricity, etc.)
- **Timestamps**: Creation and completion dates
- **Status**: Job progress tracking

### Material
- **Properties**: Name, description, color
- **Cost**: Cost per gram
- **Physical Properties**: Density (g/cm³)
- **Type**: PLA, ABS, PETG, TPU, Resin, etc.

### Printer
- **Details**: Brand, model, serial number
- **Specifications**: Build volume, printer type (FDM, SLA, etc.)
- **Power**: Watts per hour for electricity cost calculation
- **Status**: Active, inactive, in repair, etc.

### Cost
- **Breakdown**: Material cost, electricity cost, other expenses
- **Calculation**: Automatic based on material usage and print time

## 🗄️ Database Management

### Development Reset

For development purposes, you can completely reset the database:

**Windows (PowerShell):**
```powershell
.\reset-database.ps1
```

**Linux/Mac (Bash):**
```bash
./reset-database.sh
```

### What Gets Reset
- Deletes the SQLite database file
- Removes all migration files
- Cleans and rebuilds the project
- Creates fresh database with sample data

⚠️ **Warning**: This will permanently delete all data. Only use in development!

### Sample Data Included
- **Materials**: PLA, ABS, PETG, TPU, Resin
- **Printers**: Ender 3 Pro, Elegoo Mars 3, Prusa i3 MK3S+
- **Sample Jobs**: Test Cube, Phone Stand, Miniature Figure

## 🔧 Configuration

### Database Connection
The application uses SQLite by default. The connection string is configured in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=PrintCostTracker.db"
  }
}
```

### Environment-Specific Settings
- `appsettings.json`: Default configuration
- `appsettings.Development.json`: Development-specific settings

## 📁 Project Structure

```
printCostTracker/
├── Controllers/           # MVC Controllers
│   ├── HomeController.cs
│   └── PrintJobsController.cs
├── Models/               # Data Models
│   ├── PrintJob.cs
│   ├── Material.cs
│   ├── Printer.cs
│   └── Cost.cs
├── Services/             # Business Logic
│   └── IPrintCostService.cs
├── Data/                 # Database Context
│   └── PrintCostTrackerContext.cs
├── Views/                # Razor Views
├── wwwroot/              # Static Files
├── Tools/                # Utility Scripts
├── Program.cs            # Application Entry Point
├── appsettings.json      # Configuration
└── printCostTracker.csproj
```

## 🧪 Testing

### Database Connection Test
Test the database connection by visiting:
```
/api/test-db
```

This endpoint returns:
- Database connection status
- Count of materials and print jobs
- Any error messages if connection fails

## 🚀 Deployment

### Local Development
```bash
dotnet run
```

### Production Build
```bash
dotnet publish -c Release
```

### Docker (Future Enhancement)
```bash
# Dockerfile can be added for containerized deployment
docker build -t print-cost-tracker .
docker run -p 8080:80 print-cost-tracker
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🆘 Support

### Common Issues

**Database Locked**
- Stop all running instances of the application
- Wait a few seconds
- Run the reset script again

**Build Errors After Reset**
```bash
rm -rf bin obj
dotnet restore
dotnet build
```

**PowerShell Execution Policy (Windows)**
```powershell
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
```

### Getting Help
- Check the [Database Reset Guide](README_DatabaseReset.md) for detailed reset instructions
- Review the application logs for error details
- Test database connection using the `/api/test-db` endpoint

## 🔮 Future Enhancements

- [ ] User authentication and authorization
- [ ] Advanced cost analytics and reporting
- [ ] Print job scheduling
- [ ] Material inventory management
- [ ] Printer maintenance tracking
- [ ] API endpoints for external integrations
- [ ] Mobile-responsive design improvements
- [ ] Export functionality (PDF, Excel)
- [ ] Multi-tenant support
- [ ] Docker containerization

---

**Happy 3D Printing! 🖨️** 