using System;
using Portfolio_Management.Entities;
using Portfolio_Management.Enum;

namespace Portfolio_Management.Dto
{
    public record StockTransactionDto(Stock Stock, long Quantity, TransactionType TransactionType, double Price, DateTime TransactionDate);
}