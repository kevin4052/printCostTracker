using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using printCostTracker.Data;
using printCostTracker.Models;
using printCostTracker.Repositories;

namespace printCostTracker.Controllers;

public class PrintersController(IPrinterRepository printerRepository, PrintCostTrackerContext context) : Controller
{
    private readonly IPrinterRepository _printerRepository = printerRepository;
    private readonly PrintCostTrackerContext _context = context;

    public async Task<IActionResult> Index()
    {
        var printers = await _printerRepository.GetPrintersAsync();
        return View(printers);
    }

    public async Task<IActionResult> Details(int id)
    {
        var printer = await _printerRepository.GetPrinterByIdAsync(id);
        if (printer == null)
        {
            return NotFound();
        }
        return View(printer);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Description,Brand,Model,SerialNumber,Price,PurchaseDate,Location,Status,Notes,PrinterType,PrinterSize,PrintingLifetime,PrinterLifetimeCost,WattsPerHour,CostPerHour")] Printer printer)
    {
        if (ModelState.IsValid)
        {
            await _printerRepository.CreatePrinterAsync(printer);
            return RedirectToAction(nameof(Index));
        }
        return View(printer);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var printer = await _printerRepository.GetPrinterByIdAsync(id);
        if (printer == null)
        {
            return NotFound();
        }
        return View(printer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Brand,Model,SerialNumber,Price,PurchaseDate,Location,Status,Notes,PrinterType,PrinterSize,PrintingLifetime,PrinterLifetimeCost,WattsPerHour,CostPerHour")] Printer printer)
    {
        if (id != printer.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _printerRepository.UpdatePrinterAsync(printer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrinterExists(printer.Id))
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
        return View(printer);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var printer = await _printerRepository.GetPrinterByIdAsync(id);
        if (printer == null)
        {
            return NotFound();
        }
        return View(printer);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _printerRepository.DeletePrinterAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private bool PrinterExists(int id)
    {
        return _context.Printers.Any(e => e.Id == id);
    }
} 