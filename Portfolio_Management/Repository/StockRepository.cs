using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Portfolio_Management.Entities;
using Portfolio_Management.Infrastructure.Repository;
using Portfolio_Management.Repository.Interface;

namespace Portfolio_Management.Repository
{
    public class StockRepository : BaseRepository<Stock>, IStockRepository
    {
        public StockRepository(DbContext context) : base(context)
        {
        }

        public async Task<bool> IsNameUsed(string name, long? excludedId = null) =>
            await CheckIfExistAsync(x =>
                (excludedId == null || x.Id != excludedId) && x.StockName.Trim().ToLower() == name.ToLower().Trim());

        public async Task<bool> IsPrefixUsed(string? prefix = null, long? excludedId = null) =>
            await CheckIfExistAsync(x =>
                (excludedId == null || x.Id != excludedId) && x.Prefix.Trim().ToLower() == prefix.ToLower().Trim());
    }
}