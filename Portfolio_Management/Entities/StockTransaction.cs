using System;
using Portfolio_Management.Enum;
using Portfolio_Management.Model;

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
        
    }
}