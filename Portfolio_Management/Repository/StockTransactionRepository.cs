using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Portfolio_Management.Entities;
using Portfolio_Management.Enum;
using Portfolio_Management.Repository.Interface;
using Portfolio_Management.ViewModel.ResponseViewModel;

namespace Portfolio_Management.Repository
{
    public class StockTransactionRepository : BaseRepository<StockTransaction>, IStockTransactionRepository
    {
        public StockTransactionRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<StockTransactionResponse>> GetTransactions()
        {
            var transactions = await GetAllAsync();
            return transactions.Select(x => new StockTransactionResponse()
            {
                Stock = x.Stock.StockName,
                Quantity = x.Quantity,
                Price = x.Price,
                TransactionDate = x.TransactionDate.ToShortDateString(),
                TransactionType = (x.TransactionType == TransactionType.Buy) ? "BUY" : "SELL"
            }).ToList();
        }

        public async Task<List<StockTransaction>> GetPortfolio()
        {
            return await GetAllAsync();
        }

        public async Task<IEnumerable<StockTransactionResponse>> GetStockHistoryById(long id)
        {
            var history = await GetAllAsync(x => x.StockId == id);
            return history.Select(x => new StockTransactionResponse()
            {
                Stock = x.Stock.StockName,
                Quantity = x.Quantity,
                Price = x.Price,
                TransactionDate = x.TransactionDate.ToShortDateString(),
                TransactionType = (x.TransactionType == TransactionType.Buy) ? "BUY" : "SELL",
            }).ToList();
        }

        public async Task<double> GetInvestment(long id)
            => await GetQueryable().Where(x => x.StockId == id && x.TransactionType == TransactionType.Buy)
                .Select(x => x.Price).SumAsync();

        public async Task<double> GetTotalSold(long id)
            => await GetQueryable().Where(x => x.StockId == id && x.TransactionType == TransactionType.Sell)
                .Select(x => x.Price).SumAsync();

        public async Task<long> GetTotalPurchaseQuantity(long id)
            => await GetQueryable().Where(x => x.TransactionType == TransactionType.Buy).Select(x => x.Quantity)
                .SumAsync();

        public async Task<long> GetTotalSoldQuantity(long id)
            => await GetQueryable().Where(x => x.TransactionType == TransactionType.Sell).Select(x => x.Quantity)
                .SumAsync();

        public async Task<long> GetRemaining(long id)
            => await GetTotalPurchaseQuantity(id) - await GetTotalSoldQuantity(id);
    }
}