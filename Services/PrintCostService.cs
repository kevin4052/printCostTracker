using printCostTracker.Models;
using printCostTracker.Repositories;

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
    private readonly IPrintJobRepository _printJobRepository;
    private readonly IMaterialRepository _materialRepository;
    private readonly ICostRepository _costRepository;
    private readonly IPrinterRepository _printerRepository;

    public PrintCostService(
        IPrintJobRepository printJobRepository,
        IMaterialRepository materialRepository,
        ICostRepository costRepository,
        IPrinterRepository printerRepository)
    {
        _printJobRepository = printJobRepository;
        _materialRepository = materialRepository;
        _costRepository = costRepository;
        _printerRepository = printerRepository;
    }

    public async Task<List<PrintJob>> GetPrintJobsAsync()
    {
        return await _printJobRepository.GetPrintJobsAsync();
    }

    public async Task<PrintJob?> GetPrintJobByIdAsync(int id)
    {
        return await _printJobRepository.GetPrintJobByIdAsync(id);
    }

    public async Task<PrintJob> CreatePrintJobAsync(PrintJob printJob)
    {
        return await _printJobRepository.CreatePrintJobAsync(printJob);
    }

    public async Task<PrintJob> UpdatePrintJobAsync(PrintJob printJob)
    {
        return await _printJobRepository.UpdatePrintJobAsync(printJob);
    }

    public async Task DeletePrintJobAsync(int id)
    {
        await _printJobRepository.DeletePrintJobAsync(id);
    }

    public async Task<List<Material>> GetActiveMaterialsAsync()
    {
        return await _materialRepository.GetActiveMaterialsAsync();
    }

    public async Task<List<Material>> GetMaterialsAsync()
    {
        return await _materialRepository.GetMaterialsAsync();
    }

    public async Task<Material?> GetMaterialByIdAsync(int id)
    {
        return await _materialRepository.GetMaterialByIdAsync(id);
    }

    public async Task<Material> CreateMaterialAsync(Material material)
    {
        return await _materialRepository.CreateMaterialAsync(material);
    }

    public async Task<Material> UpdateMaterialAsync(Material material)
    {
        return await _materialRepository.UpdateMaterialAsync(material);
    }

    public async Task DeleteMaterialAsync(int id)
    {
        await _materialRepository.DeleteMaterialAsync(id);
    }

    public async Task<List<Cost>> GetCostsByPrintJobAsync(int printJobId)
    {
        return await _costRepository.GetCostsByPrintJobAsync(printJobId);
    }

    public async Task<Cost> AddCostToPrintJobAsync(int printJobId, Cost cost)
    {
        return await _costRepository.AddCostToPrintJobAsync(printJobId, cost);
    }

    public async Task DeleteCostAsync(int costId)
    {
        await _costRepository.DeleteCostAsync(costId);
    }

    public async Task<List<Printer>> GetPrintersAsync()
    {
        return await _printerRepository.GetPrintersAsync();
    }

    public async Task<Printer?> GetPrinterByIdAsync(int id)
    {
        return await _printerRepository.GetPrinterByIdAsync(id);
    }

    public async Task<Printer> CreatePrinterAsync(Printer printer)
    {
        return await _printerRepository.CreatePrinterAsync(printer);
    }

    public async Task<Printer> UpdatePrinterAsync(Printer printer)
    {
        return await _printerRepository.UpdatePrinterAsync(printer);
    }

    public async Task DeletePrinterAsync(int id)
    {
        await _printerRepository.DeletePrinterAsync(id);
    }
} 