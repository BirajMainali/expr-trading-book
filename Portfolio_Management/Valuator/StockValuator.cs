using System;
using System.Threading.Tasks;
using Portfolio_Management.Repository.Interface;
using Portfolio_Management.Valuator.Interface;

namespace Portfolio_Management.Valuator
{
    public class StockValuator : IStockValuator
    {
        private readonly IStockRepository _stockRepository;

        public StockValuator(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task EnsureUniqueStock(string stockName, long? id = null)
        {
            if (await _stockRepository.IsNameUsed(stockName, id))
            {
                throw new Exception("Duplicate Company");
            }
        }

        public async Task EnsureUniquePrefix(string prefix, long? id = null)
        {
            if (await _stockRepository.IsPrefixUsed(prefix, id))
            {
                throw new Exception("Duplicate Company Prefix");
            }
        }
    }
}