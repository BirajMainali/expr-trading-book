using System.Collections.Generic;
using System.Threading.Tasks;
using Portfolio_Management.Entities;
using Portfolio_Management.ViewModel.ResponseViewModel;

namespace Portfolio_Management.Repository.Interface
{
    public interface IStockTransactionRepository : IBaseRepository<StockTransaction>
    {
        Task<IEnumerable<StockTransactionResponse>> GetTransactions();
        Task<IEnumerable<StockTransactionResponse>> GetStockHistoryById(long id);
        Task<double> GetInvestment(long id);
        Task<double> GetTotalSold(long id);
        Task<long> GetTotalPurchaseQuantity(long id);
        Task<long> GetTotalSoldQuantity(long id);
        Task<long> GetRemaining(long id);
    }
}