using Microsoft.EntityFrameworkCore;
using Portfolio_Management.Entities;

namespace Portfolio_Management.Application
{
    public static class EntityRegisterer
    {
        private const string Stock = "stock";

        public static ModelBuilder AddModels(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>().ToTable("stocks", Stock);
            modelBuilder.Entity<StockTransaction>().ToTable("stock_transaction", Stock);
            return modelBuilder;
        }
    }
}