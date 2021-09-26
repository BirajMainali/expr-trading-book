using System.Collections.Generic;
using System.Threading.Tasks;
using Portfolio_Management.Entities;
using Portfolio_Management.ViewModel.ResponseViewModel;

namespace Portfolio_Management.Repository.Interface
{
    public interface IStockRepository : IBaseRepository<Stock>
    {
        Task<bool> IsNameUsed(string name, long? excludedId = null);
        Task<bool> IsPrefixUsed(string? prefix = null, long? excludedId = null);
        Task<IEnumerable<StockResponse>> GetStocks();
    }
}