using System.Threading.Tasks;
using Portfolio_Management.Dto;
using Portfolio_Management.Entities;
using Portfolio_Management.Infrastructure.Helper;
using Portfolio_Management.Repository.Interface;
using Portfolio_Management.Services.Interface;
using Portfolio_Management.Valuator.Interface;

namespace Portfolio_Management.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IStockValuator _stockValuator;

        public StockService(IStockRepository stockRepository, IStockValuator stockValuator)
        {
            _stockRepository = stockRepository;
            _stockValuator = stockValuator;
        }

        public async Task CreateStock(StockDto dto)
        {
            using var tsc = TransactionScopeHelper.Scope();
            await _stockValuator.EnsureUniqueStock(dto.StockName);
            await _stockValuator.EnsureUniquePrefix(dto.Prefix);
            var stock = new Stock(dto.StockName, dto.Quantity, dto.OpeningAmount, dto.Prefix, dto.ClosingRate);
            stock.AuditLog = $"{stock.StockName} ~ {stock.Quantity} ~ {stock.OpeningRate}";
            await _stockRepository.CreateAsync(stock);
            await _stockRepository.FlushAsync();
            tsc.Complete();
        }


        public async Task Update(Stock stock, StockDto dto)
        {
            using var tsc = TransactionScopeHelper.Scope();
            await _stockValuator.EnsureUniqueStock(dto.StockName, stock.Id);
            await _stockValuator.EnsureUniquePrefix(dto.Prefix, stock.Id);
            stock.Update(dto.StockName, dto.Quantity, dto.OpeningAmount, dto.Prefix, dto.ClosingRate);
            tsc.Complete();
        }

        public async Task Remove(Stock stock)
        {
            using var tsc = TransactionScopeHelper.Scope();
            _stockRepository.Remove(stock);
            await _stockRepository.FlushAsync();
            tsc.Complete();
        }
    }
}