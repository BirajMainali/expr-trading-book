using System;
using Portfolio_Management.Enum;

namespace Portfolio_Management.ViewModel
{
    public class StockTransactionVm
    {
        public long StockId { get; set; }
        public long Quantity { get; set; }
        public TransactionType TransactionType { get; set; }
        public double Price { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}