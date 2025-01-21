
using System.Globalization;
using simpel.api.Dtos;
using simpel.api.Models;

namespace simpel.api.Mappers;

public static class PriceDataMapper
{
    public static PriceDataDto ToDto(this PriceData priceData)
    {
        return new PriceDataDto
        {
            Average = priceData.Average.ToString("F3", CultureInfo.InvariantCulture),
            HighestPrice = priceData.HighestPrice.ToString("F3", CultureInfo.InvariantCulture),
            HighestTime = priceData.HighestTime.ToString("HH:mm", CultureInfo.InvariantCulture),
            HighestDiff = priceData.HighestDiff.ToString("F2", CultureInfo.InvariantCulture),
            LowestPrice = priceData.LowestPrice.ToString("F3", CultureInfo.InvariantCulture),
            LowestTime = priceData.LowestTime.ToString("HH:mm", CultureInfo.InvariantCulture),
            LowestDiff = priceData.LowestDiff.ToString("F2", CultureInfo.InvariantCulture)
        };
    }
}
