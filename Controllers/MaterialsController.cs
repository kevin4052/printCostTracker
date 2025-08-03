using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using printCostTracker.Data;
using printCostTracker.Models;
using printCostTracker.Services;

namespace printCostTracker.Controllers;

public class MaterialsController : Controller
{
    private readonly IPrintCostService _printCostService;
    private readonly PrintCostTrackerContext _context;

    public MaterialsController(IPrintCostService printCostService, PrintCostTrackerContext context)
    {
        _printCostService = printCostService;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var materials = await _printCostService.GetMaterialsAsync();
        return View(materials);
    }

    public async Task<IActionResult> Details(int id)
    {
        var material = await _printCostService.GetMaterialByIdAsync(id);
        if (material == null)
        {
            return NotFound();
        }
        return View(material);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Description,CostPerGram,Density,MaterialType,Color,IsActive")] Material material)
    {
        if (ModelState.IsValid)
        {
            await _printCostService.CreateMaterialAsync(material);
            return RedirectToAction(nameof(Index));
        }
        return View(material);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var material = await _printCostService.GetMaterialByIdAsync(id);
        if (material == null)
        {
            return NotFound();
        }
        return View(material);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CostPerGram,Density,MaterialType,Color,IsActive,CreatedAt")] Material material)
    {
        if (id != material.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _printCostService.UpdateMaterialAsync(material);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaterialExists(material.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(material);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var material = await _printCostService.GetMaterialByIdAsync(id);
        if (material == null)
        {
            return NotFound();
        }
        return View(material);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _printCostService.DeleteMaterialAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private bool MaterialExists(int id)
    {
        return _context.Materials.Any(e => e.Id == id);
    }
} 