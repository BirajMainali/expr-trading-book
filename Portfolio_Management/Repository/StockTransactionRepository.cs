using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Portfolio_Management.Entities;
using Portfolio_Management.Infrastructure.Enum;
using Portfolio_Management.Infrastructure.Repository;
using Portfolio_Management.Repository.Interface;

namespace Portfolio_Management.Repository
{
    public class StockTransactionRepository : BaseRepository<StockTransaction>, IStockTransactionRepository
    {
        public StockTransactionRepository(DbContext context) : base(context)
        {
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