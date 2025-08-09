using System.ComponentModel.DataAnnotations;

namespace printCostTracker.Models;

public class Part
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; set; }

    public PartMaterial[] PartMaterial { get; set; } = [];

    public decimal TotalWeight { get; set; } // kg
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public bool IsActive { get; set; } = true;
}

public class PartMaterial
{
    public required Material Material { get; set; }
    public int MaterialWeight { get; set; } // grams
}
