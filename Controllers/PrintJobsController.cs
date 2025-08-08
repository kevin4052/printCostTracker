using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using printCostTracker.Data;
using printCostTracker.Models;
using printCostTracker.Repositories;
using printCostTracker.Services;

namespace printCostTracker.Controllers;

public class PrintJobsController(
    IPrintJobRepository printJobRepository, 
    IPrinterRepository printerRepository, 
    IMaterialRepository materialRepository, 
    PrintCostTrackerContext context) : Controller
{
    private readonly IPrintJobRepository _printJobRepository = printJobRepository;
    private readonly IPrinterRepository _printerRepository = printerRepository;
    private readonly IMaterialRepository _materialRepository = materialRepository;
    private readonly PrintCostTrackerContext _context = context;

    public async Task<IActionResult> Index()
    {
        var printJobs = await _printJobRepository.GetPrintJobsAsync();
        return View(printJobs);
    }

    public async Task<IActionResult> Details(int id)
    {
        var printJob = await _printJobRepository.GetPrintJobByIdAsync(id);
        if (printJob == null)
        {
            return NotFound();
        }
        return View(printJob);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Materials = await _materialRepository.GetMaterialsAsync();
        ViewBag.Printers = await _printerRepository.GetPrintersAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Description,MaterialId,MaterialWeight,EstimatedPrintTime,PrinterId,PrintSettings")] PrintJob printJob)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Materials = await _materialRepository.GetMaterialsAsync();
            ViewBag.Printers = await _printerRepository.GetPrintersAsync();
            return View(printJob);
        }

        // Calculate total cost based on material weight
        if (printJob.MaterialWeight > 0 && printJob.MaterialId > 0)
        {
            var material = await _materialRepository.GetMaterialByIdAsync(printJob.MaterialId);
            if (material != null)
            {
                printJob.TotalCost = printJob.MaterialWeight * material.CostPerGram;
            }
        }
            
        await _printJobRepository.CreatePrintJobAsync(printJob);
        return RedirectToAction(nameof(Index));        
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