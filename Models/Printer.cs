namespace printCostTracker.Models;

public class Printer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public string PurchaseDate { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string PrinterType { get; set; } = string.Empty;
    public string PrinterSize { get; set; } = string.Empty;
    public int PrintingLifetime { get; set; } = 0;
    public int WattsPerHour { get; set; } = 0;
    public int CostPerHour { get; set; } = 0;
}

public enum PrinterType
{
    FDM,
    SLA,
    MJF,
    DLP,
    SLS
}

public enum PrinterSize
{
    _100x100x100,
    _150x150x150,
    _200x200x200,
    _220x220x250,
    _256x256x256,
    _300x300x300,
    _300x300x400,
    _400x400x400,
    _400x400x450,
    _500x500x500,
    Custom
}

public enum PrinterStatus
{
    Active,
    Inactive,
    InRepair,
    InStorage,
    InTransit,
    InUse
}