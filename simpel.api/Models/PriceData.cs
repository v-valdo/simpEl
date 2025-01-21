namespace simpel.api.Models;

public class PriceData
{
    public decimal Average { get; set; }
    public decimal HighestPrice { get; set; }
    public DateTime HighestTime { get; set; }
    public decimal HighestDiff { get; set; }
    public decimal LowestPrice { get; set; }
    public DateTime LowestTime { get; set; }
    public decimal LowestDiff { get; set; }
}