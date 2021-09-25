using System.Threading.Tasks;
using Portfolio_Management.Dto;
using Portfolio_Management.Entities;

namespace Portfolio_Management.Services.Interface
{
    public interface IStockService
    {
        Task CreateStock(StockDto dto);
        Task Remove(Stock stock);
        Task Update(Stock stock, StockDto dto);
    }
}