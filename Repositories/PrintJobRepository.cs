using Microsoft.EntityFrameworkCore;
using printCostTracker.Data;
using printCostTracker.Models;

namespace printCostTracker.Repositories;

public interface IPrintJobRepository
{
    Task<List<PrintJob>> GetPrintJobsAsync();
    Task<PrintJob?> GetPrintJobByIdAsync(int id);
    Task<PrintJob> CreatePrintJobAsync(PrintJob printJob);
    Task<PrintJob> UpdatePrintJobAsync(PrintJob printJob);
    Task DeletePrintJobAsync(int id);
}

public class PrintJobRepository : IPrintJobRepository
{
    private readonly PrintCostTrackerContext _context;

    public PrintJobRepository(PrintCostTrackerContext context)
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
}


