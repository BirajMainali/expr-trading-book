using Microsoft.EntityFrameworkCore;
using Portfolio_Management.Entities;
using Portfolio_Management.Infrastructure.Repository;
using Portfolio_Management.Repository.Interface;

namespace Portfolio_Management.Repository
{
    public class StockTransactionRepository : BaseRepository<StockTransaction>, IStockTransactionRepository
    {
        public StockTransactionRepository(DbContext context) : base(context)
        {
        }
    }
}