using Microsoft.EntityFrameworkCore;
using printCostTracker.Models;

namespace printCostTracker.Data;

public class PrintCostTrackerContext : DbContext
{
    public PrintCostTrackerContext(DbContextOptions<PrintCostTrackerContext> options)
        : base(options)
    {
    }

    public DbSet<PrintJob> PrintJobs { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<Cost> Costs { get; set; }
    public DbSet<Printer> Printers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships
        modelBuilder.Entity<PrintJob>()
            .HasOne(p => p.Material)
            .WithMany(m => m.PrintJobs)
            .HasForeignKey(p => p.MaterialId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PrintJob>()
            .HasOne(p => p.Printer)
            .WithMany()
            .HasForeignKey(p => p.PrinterId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Cost>()
            .HasOne(c => c.PrintJob)
            .WithMany(p => p.Costs)
            .HasForeignKey(c => c.PrintJobId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure decimal precision
        modelBuilder.Entity<Material>()
            .Property(m => m.CostPerGram)
            .HasPrecision(10, 4);

        modelBuilder.Entity<Material>()
            .Property(m => m.SpoolWeight)
            .HasPrecision(8, 3);

        modelBuilder.Entity<Material>()
            .Property(m => m.CostPerSpool)
            .HasPrecision(10, 2);

        modelBuilder.Entity<PrintJob>()
            .Property(p => p.TotalCost)
            .HasPrecision(10, 2);

        modelBuilder.Entity<PrintJob>()
            .Property(p => p.MaterialWeight)
            .HasPrecision(10, 2);

        modelBuilder.Entity<PrintJob>()
            .Property(p => p.EstimatedPrintTime)
            .HasPrecision(8, 2);

        modelBuilder.Entity<Cost>()
            .Property(c => c.Amount)
            .HasPrecision(10, 2);

        // Configure Material entity
        modelBuilder.Entity<Material>()
            .Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Material>()
            .Property(m => m.Description)
            .HasMaxLength(500);

        modelBuilder.Entity<Material>()
            .Property(m => m.Color)
            .HasMaxLength(50);

        // Configure PrintJob entity
        modelBuilder.Entity<PrintJob>()
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<PrintJob>()
            .Property(p => p.Description)
            .HasMaxLength(500);

        // Configure Cost entity
        modelBuilder.Entity<Cost>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Cost>()
            .Property(c => c.Description)
            .HasMaxLength(500);

        // Configure CostType enum
        modelBuilder.Entity<Cost>()
            .Property(c => c.Type)
            .HasConversion<string>();

        // Configure Printer entity
        modelBuilder.Entity<Printer>()
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Printer>()
            .Property(p => p.Description)
            .HasMaxLength(500);

        modelBuilder.Entity<Printer>()
            .Property(p => p.Brand)
            .HasMaxLength(100);

        modelBuilder.Entity<Printer>()
            .Property(p => p.Model)
            .HasMaxLength(100);

        modelBuilder.Entity<Printer>()
            .Property(p => p.SerialNumber)
            .HasMaxLength(100);

        modelBuilder.Entity<Printer>()
            .Property(p => p.Price)
            .HasMaxLength(50);

        modelBuilder.Entity<Printer>()
            .Property(p => p.PurchaseDate)
            .HasMaxLength(50);

        modelBuilder.Entity<Printer>()
            .Property(p => p.Location)
            .HasMaxLength(200);

        modelBuilder.Entity<Printer>()
            .Property(p => p.Status)
            .HasMaxLength(50);

        modelBuilder.Entity<Printer>()
            .Property(p => p.Notes)
            .HasMaxLength(500);

        modelBuilder.Entity<Printer>()
            .Property(p => p.PrinterType)
            .HasMaxLength(50);

        modelBuilder.Entity<Printer>()
            .Property(p => p.PrinterSize)
            .HasMaxLength(50);

        // Configure PrinterType enum
        modelBuilder.Entity<Printer>()
            .Property(p => p.PrinterType)
            .HasConversion<string>();

        // Configure PrinterSize enum
        modelBuilder.Entity<Printer>()
            .Property(p => p.PrinterSize)
            .HasConversion<string>();

        // Configure PrinterStatus enum
        modelBuilder.Entity<Printer>()
            .Property(p => p.Status)
            .HasConversion<string>();

        // Configure MaterialType enum
        modelBuilder.Entity<Material>()
            .Property(m => m.MaterialType)
            .HasConversion<int>();

        // Seed some initial data
        modelBuilder.Entity<Material>().HasData(
            new Material
            {
                Id = 1,
                Name = "PLA",
                Description = "Polylactic Acid - Common 3D printing material",
                CostPerGram = 0.0200m,
                SpoolWeight = 1.0m,
                CostPerSpool = 20.00m,
                MaterialType = 0, // PLA
                Color = "White",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Material
            {
                Id = 2,
                Name = "ABS",
                Description = "Acrylonitrile Butadiene Styrene - Strong and durable",
                CostPerGram = 0.0300m,
                SpoolWeight = 1.0m,
                CostPerSpool = 30.00m,
                MaterialType = 1, // ABS
                Color = "Black",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            },
            new Material
            {
                Id = 3,
                Name = "PETG",
                Description = "Polyethylene Terephthalate Glycol - Good balance of properties",
                CostPerGram = 0.0350m,
                SpoolWeight = 1.0m,
                CostPerSpool = 35.00m,
                MaterialType = 2, // PETG
                Color = "Clear",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            }
        );

        // Seed initial printer data
        modelBuilder.Entity<Printer>().HasData(
            new Printer
            {
                Id = 1,
                Name = "BambuLab A1 Combo",
                Description = "Popular FDM 3D printer",
                Brand = "BambuLab",
                Model = "A1 Combo",
                SerialNumber = "BL001",
                Price = "550.00",
                PurchaseDate = "2023-01-15",
                Location = "Workshop A",
                Status = "Active",
                Notes = "Main production printer",
                PrinterType = "FDM",
                PrinterSize = "_256x256x256",
                PrintingLifetime = 3000,
                PrinterLifetimeCost = 0,
                WattsPerHour = 120,
                CostPerHour = 0.27m
            },
            new Printer
            {
                Id = 2,
                Name = "Elegoo Mars 3",
                Description = "Resin 3D printer for detailed prints",
                Brand = "Elegoo",
                Model = "Mars 3",
                SerialNumber = "EM001",
                Price = "199.99",
                PurchaseDate = "2023-03-20",
                Location = "Workshop B",
                Status = "Active",
                Notes = "Used for detailed miniatures",
                PrinterType = "SLA",
                PrinterSize = "_143x89x165",
                PrintingLifetime = 500,
                PrinterLifetimeCost = 0,
                WattsPerHour = 150,
                CostPerHour = 1
            }
        );
    }
} 