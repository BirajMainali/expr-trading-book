using System;

namespace Portfolio_Management.ViewModel.ResponseViewModel
{
    public class StockResponse
    {
        public string StockName { get; set; }
        public string Prefix { get; set; }
        public long Quantity { get; set; }
        public decimal OpeningRate { get; set; }
        public decimal? ClosingRate { get; set; }
        public string TransactionDate { get; set; }
    }
}