using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using printCostTracker.Data;
using printCostTracker.Models;
using printCostTracker.Repositories;

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
        ViewBag.Printers = await _printerRepository.GetPrintersAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Description,PrinterId,PrintSettings")] PrintJob printJob)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Printers = await _printerRepository.GetPrintersAsync();
            return View(printJob);
        }

        // Initial total cost will be 0. Additional costs can be added separately via Costs entries
        await _printJobRepository.CreatePrintJobAsync(printJob);
        return RedirectToAction(nameof(Index));        
    }

    public async Task<IActionResult> Edit(int id)
    {
        var printJob = await _printJobRepository.GetPrintJobByIdAsync(id);
        if (printJob == null)
        {
            return NotFound();
        }
        
        ViewBag.Printers = await _printerRepository.GetPrintersAsync();
        return View(printJob);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,PrinterId,PrintSettings,TotalCost,CompletedAt,CreatedAt")] PrintJob printJob)
    {
        if (id != printJob.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _printJobRepository.UpdatePrintJobAsync(printJob);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrintJobExists(printJob.Id))
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
        
        ViewBag.Printers = await _printerRepository.GetPrintersAsync();
        return View(printJob);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var printJob = await _printJobRepository.GetPrintJobByIdAsync(id);
        if (printJob == null)
        {
            return NotFound();
        }
        return View(printJob);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _printJobRepository.DeletePrintJobAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private bool PrintJobExists(int id)
    {
        return _context.PrintJobs.Any(e => e.Id == id);
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