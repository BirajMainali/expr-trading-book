using System.Collections.Generic;
using System.Threading.Tasks;
using Portfolio_Management.Entities;
using Portfolio_Management.ViewModel.ResponseViewModel;

namespace Portfolio_Management.Repository.Interface
{
    public interface IStockTransactionRepository : IBaseRepository<StockTransaction>
    {
        Task<IEnumerable<StockTransactionResponse>> GetTransactions();
        Task<List<PortfolioResponse>> GetPortfolio();
        Task<IEnumerable<StockTransactionResponse>> GetStockHistoryById(long id);
        Task<double> GetInvestment();
        Task<double> GetTotalSold();
        Task<decimal?> GetCurrentValuation();
        Task<DashBoardResponseVm> GetSummary();
        Task<double> TotalUnit();
    }
}