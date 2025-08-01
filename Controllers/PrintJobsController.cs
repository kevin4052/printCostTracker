using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using printCostTracker.Data;
using printCostTracker.Models;
using printCostTracker.Services;

namespace printCostTracker.Controllers;

public class PrintJobsController : Controller
{
    private readonly IPrintCostService _printCostService;
    private readonly PrintCostTrackerContext _context;

    public PrintJobsController(IPrintCostService printCostService, PrintCostTrackerContext context)
    {
        _printCostService = printCostService;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var printJobs = await _printCostService.GetPrintJobsAsync();
        return View(printJobs);
    }

    public async Task<IActionResult> Details(int id)
    {
        var printJob = await _printCostService.GetPrintJobByIdAsync(id);
        if (printJob == null)
        {
            return NotFound();
        }
        return View(printJob);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Materials = await _printCostService.GetMaterialsAsync();
        ViewBag.Printers = await _printCostService.GetPrintersAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Description,MaterialId,MaterialWeight,EstimatedPrintTime,PrinterId,PrintSettings")] PrintJob printJob)
    {
        if (ModelState.IsValid)
        {
            // Calculate total cost based on material weight
            if (printJob.MaterialWeight > 0 && printJob.MaterialId > 0)
            {
                var material = await _printCostService.GetMaterialByIdAsync(printJob.MaterialId);
                if (material != null)
                {
                    printJob.TotalCost = printJob.MaterialWeight * material.CostPerGram;
                }
            }
            
            await _printCostService.CreatePrintJobAsync(printJob);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Materials = await _printCostService.GetMaterialsAsync();
        ViewBag.Printers = await _printCostService.GetPrintersAsync();
        return View(printJob);
    }

    // API endpoint to test database connection
    [HttpGet]
    [Route("api/test-db")]
    public async Task<IActionResult> TestDatabase()
    {
        try
        {
            // Ensure database is created
            await _context.Database.EnsureCreatedAsync();
            
            // Get materials count
            var materialsCount = await _context.Materials.CountAsync();
            var printJobsCount = await _context.PrintJobs.CountAsync();
            
            return Json(new { 
                success = true, 
                message = "Database connection successful",
                materialsCount,
                printJobsCount
            });
        }
        catch (Exception ex)
        {
            return Json(new { 
                success = false, 
                message = "Database connection failed",
                error = ex.Message 
            });
        }
    }
} 