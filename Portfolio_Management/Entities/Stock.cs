using Portfolio_Management.Infrastructure.Model;

namespace Portfolio_Management.Entities
{
    public class Stock : BaseModel
    {
        public string StockName { get; protected set; }
        public string Prefix { get; protected set; }
        public long Quantity { get; protected set; }
        public decimal OpeningRate { get; protected set; }
        public decimal? ClosingRate { get; set; }

        protected Stock()
        {
        }

        public Stock(string stockName, long quantity, decimal openingAmount, string prefix, decimal? closingRate)
            => Copy(stockName, quantity, openingAmount, prefix, closingRate);

        public void Update(string stockName, long quantity, decimal openingAmount, string prefix, decimal? closingRate)
            => Copy(stockName, quantity, openingAmount, prefix, closingRate);

        private void Copy(string stockName, long quantity, decimal openingAmount, string prefix, decimal? closingRate)
        {
            StockName = stockName;
            Quantity = quantity;
            OpeningRate = openingAmount;
            Prefix = prefix;
            ClosingRate = closingRate;
        }
    }
}