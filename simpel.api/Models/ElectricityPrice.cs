using simpel.api.Enums;

namespace simpel.api.Models;
public class ElectricityPrice
{
    public decimal SEK_per_kWh { get; set; }
    public DateTime time_start { get; set; }
    public DateTime time_end { get; set; }
}