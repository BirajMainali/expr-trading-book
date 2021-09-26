using System;
using System.ComponentModel.DataAnnotations;

namespace Portfolio_Management.ViewModel
{
    public class StockVm
    {
        [Required]
        public string StockName { get; set; }
        [Required]
        public string Prefix { get; set; }
        [Required]
        public long Quantity { get; set; }
        [Required]
        public decimal OpeningAmount{get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        
        public decimal? ClosingRate { get; set; }
    }
}