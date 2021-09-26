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

                var stock = await _stockRepository.FindOrThrowAsync(viewModel.StockId);
                if (stock == null)
                {
                    return NotFound();
                }

                if (viewModel.TransactionType == TransactionType.Sell)
                {
                    stock.ClosingRate += stock.ClosingRate * 10 / 100;
                    if (stock.ClosingRate > (decimal?)viewModel.Price)
                    {
                        throw new Exception($"Max is {stock.ClosingRate}");
                    }
                }

                var dto = new StockTransactionDto(stock, viewModel.Quantity, viewModel.TransactionType, viewModel.Price,
                    viewModel.TransactionDate);
                await _transactionService.RecordStockTransaction(dto);
                if (dto.TransactionType == TransactionType.Buy)
                {
                    return Ok($"{dto.Stock.Prefix} Added {dto.Quantity} at {dto.Price}");
                }

                return Ok($"{dto.Stock.Prefix} Sold {dto.Quantity} at {dto.Price}");
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
    }
}