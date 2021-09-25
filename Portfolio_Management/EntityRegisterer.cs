using Microsoft.EntityFrameworkCore;
using Portfolio_Management.Entities;

namespace Portfolio_Management
{
    public static class EntityRegisterer
    {
        public static ModelBuilder AddModels(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>().ToTable("stock", schema: "Stock");
            modelBuilder.Entity<StockTransaction>().ToTable("stock_transaction", "stock");
            return modelBuilder;
        }
    }
}