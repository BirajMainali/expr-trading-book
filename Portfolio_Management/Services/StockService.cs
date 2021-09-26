using System;
using System.Threading.Tasks;
using AutoMapper;
using Portfolio_Management.Dto;
using Portfolio_Management.Entities;
using Portfolio_Management.Infrastructure.Helper;
using Portfolio_Management.Repository.Interface;
using Portfolio_Management.Services.Interface;
using Portfolio_Management.Valuator.Interface;

namespace Portfolio_Management.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IStockValuator _stockValuator;
        private readonly IMapper _mapper;

        public StockService(IStockRepository stockRepository, IStockValuator stockValuator, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _stockValuator = stockValuator;
            _mapper = mapper;
        }

        public async Task<Stock> CreateStock(StockDto dto)
        {
            using var tsc = TransactionScopeHelper.Scope();
            try
            {
                await _stockValuator.EnsureUniqueStock(dto.StockName);
                await _stockValuator.EnsureUniquePrefix(dto.Prefix);
                var stock = _mapper.Map<Stock>(dto);
                await _stockRepository.CreateAsync(stock);
                await _stockRepository.FlushAsync();
                tsc.Complete();
                return stock;
            }
            catch (Exception e)
            {
                tsc.Dispose();
                throw;
            }
        }


        public async Task Update(Stock stock, StockDto dto)
        {
            using var tsc = TransactionScopeHelper.Scope();
            try
            {
                await _stockValuator.EnsureUniqueStock(dto.StockName, stock.Id);
                await _stockValuator.EnsureUniquePrefix(dto.Prefix, stock.Id);
                var updatedStock = _mapper.Map<Stock>(dto);
                _stockRepository.Update(updatedStock);
                await _stockRepository.FlushAsync();
                tsc.Complete();
            }
            catch (Exception e)
            {
                tsc.Dispose();
                throw;
            }
        }

        public async Task Remove(Stock stock)
        {
            using var tsc = TransactionScopeHelper.Scope();
            try
            {
                _stockRepository.Remove(stock);
                await _stockRepository.FlushAsync();
                tsc.Complete();
            }
            catch (Exception e)
            {
                tsc.Dispose();
                throw;
            }
        }
    }
}