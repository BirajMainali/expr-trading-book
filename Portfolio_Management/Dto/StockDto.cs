using System;

namespace Portfolio_Management.Dto
{
    public record StockDto(string StockName, long Quantity, decimal OpeningAmount, string Prefix, decimal? ClosingRate, DateTime TransactionDate);
}