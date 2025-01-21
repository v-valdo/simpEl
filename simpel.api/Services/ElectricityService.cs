using System.Globalization;
using simpel.api.Enums;
using simpel.api.Models;

namespace simpel.api.Services;

public class ElectricityPriceService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://www.elprisetjustnu.se/api/v1/prices";
    public ElectricityPriceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ElectricityPrice>> GetPricesAsync(DateTime date, string area)
    {
        try
        {
            var url = $"{BaseUrl}/{date:yyyy}/{date:MM-dd}_{area}.json";
            Console.WriteLine($"Trying URL: {url}");

            var response = await _httpClient.GetFromJsonAsync<List<ElectricityPrice>>(url);

            if (response == null)
            {
                Console.WriteLine("API response is null");
                return [];
            }
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return [];
        }
    }

    public async Task<ElectricityPrice> GetCurrentPriceAsync(string area)
    {
        var prices = await GetPricesAsync(DateTime.Today, area);
        return prices.FirstOrDefault(p =>
            p.time_start <= DateTime.Now &&
            p.time_end > DateTime.Now);
    }

    public async Task<IEnumerable<ElectricityPrice>> GetTodayPricesAsync(string area)
    {
        return await GetPricesAsync(DateTime.Today, area);
    }

    public async Task<IEnumerable<ElectricityPrice>> GetTomorrowPricesAsync(string area)
    {
        return await GetPricesAsync(DateTime.Today.AddDays(1), area);
    }

    public async Task<decimal> GetAveragePriceAsync(DateTime date, string area)
    {
        var prices = await GetPricesAsync(date, area);
        return prices.Any() ? prices.Average(p => p.SEK_per_kWh) : 0;
    }

    public async Task<PriceData> GetPriceDataAsync(string area)
    {
        if (!Enum.TryParse<PriceArea>(area, true, out var parsedArea) || !Enum.IsDefined(parsedArea))
        {
            Console.WriteLine($"Invalid price area - {area}");
            return new();
        }

        var todaysFuturePrices = (await GetTodayPricesAsync(area))
            .Where(p => p.time_start > DateTime.Now)
            .ToList();

        if (todaysFuturePrices.Count == 0) return new();

        var dailyAverage = await GetAveragePriceAsync(DateTime.Today, area);

        var (lowestPrice, lowestDiff) = FindPriceAnomaly(todaysFuturePrices, false, dailyAverage);
        var (highestPrice, highestDiff) = FindPriceAnomaly(todaysFuturePrices, true, dailyAverage);

        if (highestPrice == null || lowestPrice == null)
        {
            Console.WriteLine("Unable to find price anomalies (highestPrice or lowestPrice is null).");
            return new PriceData();
        }

        return new PriceData
        {
            Average = dailyAverage,
            HighestPrice = highestPrice.SEK_per_kWh,
            HighestTime = highestPrice.time_start,
            HighestDiff = highestDiff,
            LowestPrice = lowestPrice.SEK_per_kWh,
            LowestTime = lowestPrice.time_start,
            LowestDiff = lowestDiff
        };
    }
    private static (ElectricityPrice? priceAnomaly, decimal percentageDiff) FindPriceAnomaly(List<ElectricityPrice> prices, bool highest, decimal dailyAvg)
    {
        var filteredPrices = highest
            ? prices.Where(p => p.SEK_per_kWh >= dailyAvg).ToList()
            : prices.Where(p => p.SEK_per_kWh <= dailyAvg).ToList();

        if (filteredPrices.Count == 0) return (null, 0);

        var priceAnomaly = highest
            ? filteredPrices.OrderByDescending(p => p.SEK_per_kWh).First()
            : filteredPrices.OrderBy(p => p.SEK_per_kWh).First();

        decimal percentageDiff = highest
            ? (priceAnomaly.SEK_per_kWh - dailyAvg) / dailyAvg * 100
            : (dailyAvg - priceAnomaly.SEK_per_kWh) / dailyAvg * 100;

        return (priceAnomaly, percentageDiff);
    }
}
