using System.Threading.Tasks;
using Portfolio_Management.Dto;
using Portfolio_Management.Entities;
using Portfolio_Management.Infrastructure.Enum;
using Portfolio_Management.Infrastructure.Helper;
using Portfolio_Management.Repository.Interface;
using Portfolio_Management.Services.Interface;

namespace Portfolio_Management.Services
{
    public class StockTransactionService : IStockTransactionService
    {
        private readonly IStockTransactionRepository _stockTransactionRepository;

        public StockTransactionService(IStockTransactionRepository stockTransactionRepository)
        {
            _stockTransactionRepository = stockTransactionRepository;
        }

        public async Task RecordStockTransaction(StockTransactionDto dto)
        {
            using var tsc = TransactionScopeHelper.Scope();
            var transaction = new StockTransaction(dto.Stock, dto.Quantity, dto.TransactionType, dto.Price,
                dto.TransactionDate);
            if (transaction.TransactionType == TransactionType.Sell)
            {
                transaction.Stock.ClosingRate = (decimal?)dto.Price;
            }

            await _stockTransactionRepository.CreateAsync(transaction);
            await _stockTransactionRepository.FlushAsync();
            tsc.Complete();
        }
    }
}