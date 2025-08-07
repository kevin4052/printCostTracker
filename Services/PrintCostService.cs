using Microsoft.EntityFrameworkCore;
using printCostTracker.Data;
using printCostTracker.Models;

namespace printCostTracker.Services;

public interface IPrintCostService
{
    Task<List<PrintJob>> GetPrintJobsAsync();
    Task<PrintJob?> GetPrintJobByIdAsync(int id);
    Task<PrintJob> CreatePrintJobAsync(PrintJob printJob);
    Task<PrintJob> UpdatePrintJobAsync(PrintJob printJob);
    Task DeletePrintJobAsync(int id);
    
    Task<List<Material>> GetMaterialsAsync();
    Task<Material?> GetMaterialByIdAsync(int id);
    Task<Material> CreateMaterialAsync(Material material);
    Task<Material> UpdateMaterialAsync(Material material);
    Task DeleteMaterialAsync(int id);
    
    Task<List<Cost>> GetCostsByPrintJobAsync(int printJobId);
    Task<Cost> AddCostToPrintJobAsync(int printJobId, Cost cost);
    Task DeleteCostAsync(int costId);
    
    Task<List<Printer>> GetPrintersAsync();
    Task<Printer?> GetPrinterByIdAsync(int id);
    Task<Printer> CreatePrinterAsync(Printer printer);
    Task<Printer> UpdatePrinterAsync(Printer printer);
    Task DeletePrinterAsync(int id);
}

public class PrintCostService : IPrintCostService
{
    private readonly PrintCostTrackerContext _context;

    public PrintCostService(PrintCostTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<PrintJob>> GetPrintJobsAsync()
    {
        return await _context.PrintJobs
            .Include(p => p.Material)
            .Include(p => p.Printer)
            .Include(p => p.Costs)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<PrintJob?> GetPrintJobByIdAsync(int id)
    {
        return await _context.PrintJobs
            .Include(p => p.Material)
            .Include(p => p.Printer)
            .Include(p => p.Costs)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<PrintJob> CreatePrintJobAsync(PrintJob printJob)
    {
        _context.PrintJobs.Add(printJob);
        await _context.SaveChangesAsync();
        return printJob;
    }

    public async Task<PrintJob> UpdatePrintJobAsync(PrintJob printJob)
    {
        _context.Entry(printJob).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return printJob;
    }

    public async Task DeletePrintJobAsync(int id)
    {
        var printJob = await _context.PrintJobs.FindAsync(id);
        if (printJob != null)
        {
            _context.PrintJobs.Remove(printJob);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Material>> GetActiveMaterialsAsync()
    {
        return await _context.Materials
            .Where(m => m.IsActive)
            .OrderBy(m => m.Name)
            .ToListAsync();
    }

    public async Task<List<Material>> GetMaterialsAsync()
    {
        return await _context.Materials
            .OrderBy(m => m.Name)
            .ToListAsync();
    }

    public async Task<Material?> GetMaterialByIdAsync(int id)
    {
        return await _context.Materials.FindAsync(id);
    }

    public async Task<Material> CreateMaterialAsync(Material material)
    {
        // Calculate CostPerGram if SpoolWeight and CostPerSpool are provided
        if (material.SpoolWeight > 0 && material.CostPerSpool > 0)
        {
            material.CostPerGram = material.CostPerSpool / (material.SpoolWeight * 1000);
        }
        
        _context.Materials.Add(material);
        await _context.SaveChangesAsync();
        return material;
    }

    public async Task<Material> UpdateMaterialAsync(Material material)
    {
        // Calculate CostPerGram if SpoolWeight and CostPerSpool are provided
        if (material.SpoolWeight > 0 && material.CostPerSpool > 0)
        {
            material.CostPerGram = material.CostPerSpool / (material.SpoolWeight * 1000);
        }
        
        _context.Entry(material).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return material;
    }

    public async Task DeleteMaterialAsync(int id)
    {
        var material = await _context.Materials.FindAsync(id);
        if (material != null)
        {
            material.IsActive = false;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Cost>> GetCostsByPrintJobAsync(int printJobId)
    {
        return await _context.Costs
            .Where(c => c.PrintJobId == printJobId)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<Cost> AddCostToPrintJobAsync(int printJobId, Cost cost)
    {
        cost.PrintJobId = printJobId;
        _context.Costs.Add(cost);
        await _context.SaveChangesAsync();
        return cost;
    }

    public async Task DeleteCostAsync(int costId)
    {
        var cost = await _context.Costs.FindAsync(costId);
        if (cost != null)
        {
            _context.Costs.Remove(cost);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Printer>> GetPrintersAsync()
    {
        return await _context.Printers
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<Printer?> GetPrinterByIdAsync(int id)
    {
        return await _context.Printers.FindAsync(id);
    }

    public async Task<Printer> CreatePrinterAsync(Printer printer)
    {
        _context.Printers.Add(printer);
        await _context.SaveChangesAsync();
        return printer;
    }

    public async Task<Printer> UpdatePrinterAsync(Printer printer)
    {
        _context.Entry(printer).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return printer;
    }

    public async Task DeletePrinterAsync(int id)
    {
        var printer = await _context.Printers.FindAsync(id);
        if (printer != null)
        {
            _context.Printers.Remove(printer);
            await _context.SaveChangesAsync();
        }
    }
} 