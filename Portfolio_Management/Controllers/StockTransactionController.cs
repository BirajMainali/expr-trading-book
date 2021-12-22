using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portfolio_Management.Dto;
using Portfolio_Management.Enum;
using Portfolio_Management.Repository.Interface;
using Portfolio_Management.Services.Interface;
using Portfolio_Management.ViewModel;

namespace Portfolio_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StockTransactionController : Controller
    {
        private readonly IStockTransactionRepository _transactionRepository;
        private readonly IStockTransactionService _transactionService;
        private readonly IStockRepository _stockRepository;

        public StockTransactionController(IStockTransactionRepository transactionRepository,
            IStockTransactionService transactionService, IStockRepository stockRepository)
        {
            _transactionRepository = transactionRepository;
            _transactionService = transactionService;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var transactions = await _transactionRepository.GetTransactions();
                return Ok(transactions);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> New(StockTransactionVm viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return UnprocessableEntity(viewModel);
                }

                if (viewModel.TransactionType == TransactionType.Sell)
                    await ValidateRemainingStockAfterSell(viewModel.StockId);
                
                if (viewModel.Price < 0)
                {
                    throw new Exception("Stock price can not be negative.");
                }

                var stock = await _stockRepository.FindOrThrowAsync(viewModel.StockId);
                if (stock == null)
                {
                    return NotFound();
                }

                if (viewModel.TransactionType == TransactionType.Sell)
                {
                    stock.ClosingRate += stock.ClosingRate * 10 / 100;
                    if (stock.ClosingRate < (decimal?)viewModel.Price)
                    {
                        throw new Exception($"Max is {stock.ClosingRate}");
                    }
                }

                var dto = new StockTransactionDto(stock, viewModel.Quantity, viewModel.TransactionType, viewModel.Price,
                    viewModel.TransactionDate);
                var transaction = await _transactionService.RecordStockTransaction(dto);

                var history = new
                {
                    transaction.Id, transaction.StockId, transaction.Price, transaction.Quantity,
                    stock = transaction.Stock.StockName,
                    TransactionDate = transaction.TransactionDate.ToShortDateString(),
                    TransactionType = (transaction.TransactionType == TransactionType.Buy) ? "BUY" : "SELL"
                };
                return CreatedAtAction("History", new { stock.Id }, history);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Remove(long id)
        {
            try
            {
                var transaction = await _transactionRepository.FindOrThrowAsync(id);
                if (transaction == null)
                {
                    return NotFound();
                }

                await _transactionService.RemoveTransaction(transaction);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> History(long id)
        {
            try
            {
                if (id < 0)
                {
                    return BadRequest("Invalid Id");
                }

                var stock = await _stockRepository.FindOrThrowAsync(id);
                if (stock == null)
                {
                    return NotFound("No data found");
                }

                var history = await _transactionRepository.GetStockHistoryById(id);
                return Ok(history);
            }
            catch (Exception e)
            {
                return BadRequest("Invalid Request");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Portfolio()
        {
            var res = await _transactionRepository.GetPortfolio();
            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> DashBoard()
        {
            try
            {
                var data = await _transactionRepository.GetSummary();
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private async Task ValidateRemainingStockAfterSell(long stockId)
        {
            var investment = await _transactionRepository.GetInvestment(stockId);
            var totalSales = await _transactionRepository.GetTotalSold(stockId);
            if ((investment - totalSales) >= 0)
            {
                throw new Exception("Insufficient Quantity");
            }
        }
    }
}