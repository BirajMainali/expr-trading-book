using System;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using Portfolio_Management.Dto;
using Portfolio_Management.Entities;
using Portfolio_Management.Enum;
using Portfolio_Management.Infrastructure.Helper;
using Portfolio_Management.Repository.Interface;
using Portfolio_Management.Services.Interface;

namespace Portfolio_Management.Services
{
    public class StockTransactionService : IStockTransactionService
    {
        private readonly IStockTransactionRepository _stockTransactionRepository;
        private readonly IMapper _mapper;

        public StockTransactionService(IStockTransactionRepository stockTransactionRepository, IMapper mapper)
        {
            _stockTransactionRepository = stockTransactionRepository;
            _mapper = mapper;
        }

        public async Task<StockTransaction> RecordStockTransaction(StockTransactionDto dto)
        {
            using var tsc = TransactionScopeHelper.Scope();
            try
            {
                var transaction = _mapper.Map<StockTransaction>(dto);
                if (transaction.TransactionType == TransactionType.Sell)
                {
                    transaction.Stock.ClosingRate = (decimal?)dto.Price;
                }

                await _stockTransactionRepository.CreateAsync(transaction);
                await _stockTransactionRepository.FlushAsync();
                tsc.Complete();
                return transaction;
            }
            catch (Exception e)
            {
                tsc.Dispose();
                throw;
            }
        }

        public async Task RemoveTransaction(StockTransaction stockTransaction)
        {
            using var tsc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            await ValidateNegativeStockQuantity(stockTransaction.StockId);
            _stockTransactionRepository.Remove(stockTransaction);
            await _stockTransactionRepository.FlushAsync();
            tsc.Complete();
        }

        private async Task ValidateNegativeStockQuantity(long stockId)
        {
            var buy = await _stockTransactionRepository.GetInvestment(stockId);
            var sell = await _stockTransactionRepository.GetTotalSold(stockId);
            if ((buy - sell) < 0)
            {
                throw new Exception("Insufficient (-) stock.");
            }
        }
    }
}