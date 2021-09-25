using System.Threading.Tasks;
using Portfolio_Management.Dto;

namespace Portfolio_Management.Services.Interface
{
    public interface IStockTransactionService
    {
        Task RecordStockTransaction(StockTransactionDto dto);
    }
}