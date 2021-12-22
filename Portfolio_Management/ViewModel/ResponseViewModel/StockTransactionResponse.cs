namespace Portfolio_Management.ViewModel.ResponseViewModel
{
    public class StockTransactionResponse
    {
        public long Id { get; set; }
        public string Stock { get; set; }
        public long Quantity { get; set; }
        public string TransactionType { get; set; }
        public double Price { get; set; }
        public string TransactionDate { get; set; }
    }
}