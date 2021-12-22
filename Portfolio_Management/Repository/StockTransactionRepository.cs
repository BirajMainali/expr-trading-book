using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Portfolio_Management.Entities;
using Portfolio_Management.Enum;
using Portfolio_Management.Infrastructure.Providers.Interface;
using Portfolio_Management.Repository.Interface;
using Portfolio_Management.ViewModel.ResponseViewModel;

namespace Portfolio_Management.Repository
{
    public class StockTransactionRepository : BaseRepository<StockTransaction>, IStockTransactionRepository
    {
        private readonly ISqlConnectionProvider _connectionProvider;

        public StockTransactionRepository(DbContext context, ISqlConnectionProvider connectionProvider) : base(context)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task<IEnumerable<StockTransactionResponse>> GetTransactions()
            => await GetQueryable()
                .Include(x => x.Stock)
                .Select(x => new StockTransactionResponse()
                {
                    Stock = x.Stock.StockName,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    TransactionDate = x.TransactionDate.ToShortDateString(),
                    TransactionType = (x.TransactionType == TransactionType.Buy) ? "BUY" : "SELL"
                }).ToListAsync();

        public async Task<List<PortfolioResponse>> GetPortfolio()
        {
            await using var conn = _connectionProvider.GetConnection();
            const string query = @"select
    aggr.""StockName"" Stock,
    sum(aggr.BuyUnit) TotalUnit, 
    sum(aggr.SoldUnit) TotalSold,
    sum(aggr.BuyUnit * aggr.""Price"") TotalInvestment,
    sum(aggr.SoldUnit * aggr.""Price"") SoldAmount,
       sum(aggr.BuyUnit - aggr.SoldUnit) Remaining,
    (aggr.""Quantity"" * aggr.""ClosingRate"") CurrentAmount

from
    (
        select
            ""StockId"",
            s.""StockName"" ,
            s.""ClosingRate"" ,
            s.""Quantity"",
            case
                when ""TransactionType"" = 1 then st.""Quantity""
                else 0
                end BuyUnit,
            case
                when ""TransactionType"" = 2 then st.""Quantity""
                else 0
                end SoldUnit,
            ""Price""
        from
            stock.stock_transaction st
                inner join stock.stocks s on
                    s.""Id"" = ""StockId"") aggr
group by
    aggr.""StockName"",
    aggr.""ClosingRate"",
    aggr.""Quantity""";
            return (await conn.QueryAsync<PortfolioResponse>(query)).ToList();
        }

        public async Task<IEnumerable<StockTransactionResponse>> GetStockHistoryById(long id)
        {
            var history = await GetAllAsync(x => x.StockId == id);
            return history.Select(x => new StockTransactionResponse()
            {
                Id = x.Id,
                Stock = x.Stock.StockName,
                Quantity = x.Quantity,
                Price = x.Price,
                TransactionDate = x.TransactionDate.ToShortDateString(),
                TransactionType = (x.TransactionType == TransactionType.Buy) ? "BUY" : "SELL",
            }).ToList();
        }

        public async Task<double> GetInvestment(long? stockId = null)
            => await GetQueryable().Where(x =>
                    x.TransactionType == TransactionType.Buy && (x.StockId == null || x.StockId == stockId))
                .Select(x => x.Price).SumAsync();

        public async Task<double> GetTotalSold(long? stockId = null)
            => await GetQueryable().Where(x =>
                    x.TransactionType == TransactionType.Sell && (x.StockId == null || x.StockId == stockId))
                .Select(x => x.Price).SumAsync();

        public async Task<decimal?> GetCurrentValuation()
        {
            var closing = await GetQueryable().Include(x => x.Stock).Select(x => x.Stock.ClosingRate).SumAsync();
            var buyQuantity = await GetQueryable().Where(x => x.TransactionType == TransactionType.Buy)
                .Select(x => x.Quantity).SumAsync();
            var salesQuantity = await GetQueryable().Where(x => x.TransactionType == TransactionType.Sell)
                .Select(x => x.Quantity).SumAsync();
            return (buyQuantity - salesQuantity) * closing;
        }

        public async Task<double> TotalUnit()
            => await GetQueryable().Where(x => x.TransactionType == TransactionType.Buy)
                .Select(x => x.Quantity).SumAsync();

        public async Task<DashBoardResponseVm> GetSummary()
            => new DashBoardResponseVm()
            {
                TotalInvestment = await GetInvestment(),
                TotalSold = await GetTotalSold(),
                CurrentAmount = await GetCurrentValuation(),
                TotalUnit = await TotalUnit()
            };
    }
}