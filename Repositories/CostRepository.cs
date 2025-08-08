using Microsoft.EntityFrameworkCore;
using printCostTracker.Data;
using printCostTracker.Models;

namespace printCostTracker.Repositories;

public interface ICostRepository
{
    Task<List<Cost>> GetCostsByPrintJobAsync(int printJobId);
    Task<Cost> AddCostToPrintJobAsync(int printJobId, Cost cost);
    Task DeleteCostAsync(int costId);
}

public class CostRepository(PrintCostTrackerContext context) : ICostRepository
{
    private readonly PrintCostTrackerContext _context = context;

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
}


