using System.ComponentModel.DataAnnotations;

namespace printCostTracker.Models;

public class Cost
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    public decimal Amount { get; set; }
    
    public CostType Type { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public int PrintJobId { get; set; }
    public PrintJob PrintJob { get; set; } = null!;
}

public enum CostType
{
    Material,
    Electricity,
    Labor,
    Maintenance,
    Other
} 