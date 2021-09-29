using System.Threading.Tasks;
using Portfolio_Management.Dto;
using Portfolio_Management.Entities;

namespace Portfolio_Management.Services.Interface
{
    public interface IStockTransactionService
    {
        Task<StockTransaction> RecordStockTransaction(StockTransactionDto dto);
    }
}