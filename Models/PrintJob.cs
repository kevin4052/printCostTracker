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
    
    [Range(0, double.MaxValue, ErrorMessage = "Material weight must be a positive number")]
    public decimal MaterialWeight { get; set; }
    
    [Range(0, double.MaxValue, ErrorMessage = "Estimated print time must be a positive number")]
    public decimal EstimatedPrintTime { get; set; }
    
    public int? PrinterId { get; set; }
    public Printer? Printer { get; set; }
    
    [StringLength(1000)]
    public string? PrintSettings { get; set; }
    
    public List<Cost> Costs { get; set; } = new();
} 