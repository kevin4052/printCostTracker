using System.ComponentModel.DataAnnotations;

namespace printCostTracker.Models;

public class PrintJob
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? CompletedAt { get; set; }
    
    public decimal TotalCost { get; set; }
    
    public int MaterialId { get; set; }
    public Material Material { get; set; } = null!;
    
    public List<Cost> Costs { get; set; } = new();
} 