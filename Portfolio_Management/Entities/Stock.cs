using System;
using Portfolio_Management.Model;

namespace Portfolio_Management.Entities
{
    public class Stock : BaseModel
    {
        public string StockName { get; set; }
        public string Prefix { get; set; }
        public long Quantity { get; set; }

        public decimal OpeningAmount { get; set; }
        
        public DateTime TransactionDate { get; set; }
        public decimal? ClosingRate { get; set; }
    }
}