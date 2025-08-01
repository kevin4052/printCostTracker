using System.ComponentModel.DataAnnotations;

namespace printCostTracker.Models;

public class Material
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    public decimal CostPerGram { get; set; }
    
    public decimal Density { get; set; } // g/cm³

    public int MaterialType { get; set; }
    
    [StringLength(50)]
    public string? Color { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public List<PrintJob> PrintJobs { get; set; } = new();
} 

enum MaterialType
{
    PLA,
    ABS,
    PETG,
    PC,
    PVA,
    TPU,
    TPE,
    TPEE,
    TPEF,
}