using Microsoft.EntityFrameworkCore;
using printCostTracker.Data;
using printCostTracker.Models;

namespace printCostTracker.Repositories;

public interface IMaterialRepository
{
    Task<List<Material>> GetActiveMaterialsAsync();
    Task<List<Material>> GetMaterialsAsync();
    Task<Material?> GetMaterialByIdAsync(int id);
    Task<Material> CreateMaterialAsync(Material material);
    Task<Material> UpdateMaterialAsync(Material material);
    Task DeleteMaterialAsync(int id);
}

public class MaterialRepository(PrintCostTrackerContext context) : IMaterialRepository
{
    private readonly PrintCostTrackerContext _context = context;

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
            _context.Materials.Remove(material);
            await _context.SaveChangesAsync();
        }
    }
}


