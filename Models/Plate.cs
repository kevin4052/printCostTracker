namespace printCostTracker.Models;

public class Plate
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Part> Parts { get; set; } = [];
    public int PrintTime { get; set; } // minutes
}