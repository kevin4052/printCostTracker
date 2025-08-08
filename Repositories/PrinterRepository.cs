using Microsoft.EntityFrameworkCore;
using printCostTracker.Data;
using printCostTracker.Models;

namespace printCostTracker.Repositories;

public interface IPrinterRepository
{
    Task<List<Printer>> GetPrintersAsync();
    Task<Printer?> GetPrinterByIdAsync(int id);
    Task<Printer> CreatePrinterAsync(Printer printer);
    Task<Printer> UpdatePrinterAsync(Printer printer);
    Task DeletePrinterAsync(int id);
}

public class PrinterRepository : IPrinterRepository
{
    private readonly PrintCostTrackerContext _context;

    public PrinterRepository(PrintCostTrackerContext context)
    {
        _context = context;
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


