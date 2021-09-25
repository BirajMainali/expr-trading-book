using System;
using Portfolio_Management.Infrastructure.Enum;
using Portfolio_Management.Infrastructure.Model;

namespace Portfolio_Management.Entities
{
    public class StockTransaction : BaseModel
    {
        public virtual Stock Stock { get; set; }
        public long StockId { get; set; }
        public long Quantity { get; set; }
        public TransactionType TransactionType { get; set; }
        public double Price { get; set; }
        public DateTime TransactionDate { get; set; }

        protected StockTransaction()
        {
        }

        public StockTransaction(Stock stock, long quantity, TransactionType transactionType, double price,
            DateTime transactionDate)
        {
            Stock = stock;
            Quantity = quantity;
            TransactionType = transactionType;
            Price = price;
            TransactionDate = transactionDate;
        }
    }
}