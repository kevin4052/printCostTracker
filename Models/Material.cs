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
    
    public int MaterialType { get; set; }

    public decimal SpoolWeight { get; set; } // kg

    public decimal CostPerSpool { get; set; } // $
    
    public decimal CostPerGram { get; set; } // $/g    
    
    [StringLength(50)]
    public string? Color { get; set; }
    
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
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