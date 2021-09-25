using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio_Management.Dto;
using Portfolio_Management.Extension;
using Portfolio_Management.Repository.Interface;
using Portfolio_Management.Services.Interface;
using Portfolio_Management.ViewModel;

namespace Portfolio_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        private readonly IStockService _stockService;

        public StockController(IStockRepository stockRepository, IStockService stockService)
        {
            _stockRepository = stockRepository;
            _stockService = stockService;
        }

        [HttpGet("Stocks")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var stocks = await _stockRepository.GetQueryable().OrderByDescending(x => x.RecDate).Select(y => new
                {
                    id = y.Id,
                    name = y.StockName,
                    prefix = y.Prefix,
                    quantity = y.Quantity,
                    openingAmount = y.OpeningRate
                }).ToListAsync();
                return this.SendSuccess("Stocks", stocks);
            }
            catch (Exception e)
            {
                return this.SendError(e.Message);
            }
        }

        [HttpPost("New")]
        public async Task<IActionResult> New(StockVm viewModel)
        {
            try
            {
                var dto = new StockDto(viewModel.StockName, viewModel.Quantity, viewModel.OpeningAmount,
                    viewModel.Prefix, viewModel.OpeningAmount);
                await _stockService.CreateStock(dto);
                return this.SendSuccess($"{dto.StockName} has been Published for transaction");
            }
            catch (Exception e)
            {
                return this.SendError(e.Message);
            }
        }

        [HttpGet("Edit/{id:long}")]
        public async Task<IActionResult> Edit(long id)
        {
            try
            {
                var stock = await _stockRepository.FindOrThrowAsync(id);
                var stockEditViewModel = new StockVm()
                {
                    StockName = stock.StockName,
                    OpeningAmount = stock.OpeningRate,
                    Prefix = stock.Prefix,
                    Quantity = stock.Quantity
                };
                return this.SendSuccess("Enable For Edit", stockEditViewModel);
            }
            catch (Exception e)
            {
                return this.SendError(e.Message);
            }
        }

        [HttpPost("Edit/{id:long}")]
        public async Task<IActionResult> Edit(long id, StockVm viewModel)
        {
            try
            {
                var stock = await _stockRepository.FindOrThrowAsync(id);
                var stockUpdateDto = new StockDto(viewModel.StockName, viewModel.Quantity, viewModel.OpeningAmount,
                    viewModel.Prefix, viewModel.OpeningAmount);
                await _stockService.Update(stock, stockUpdateDto);
                return this.SendSuccess($"{stock.StockName} has been updated");
            }
            catch (Exception e)
            {
                return this.SendError(e.Message);
            }
        }

        [HttpDelete("Remove")]
        public async Task<IActionResult> Remove(long id)
        {
            try
            {
                var stock = await _stockRepository.FindOrThrowAsync(id);
                await _stockService.Remove(stock);
                return this.SendSuccess("Removed");
            }
            catch (Exception e)
            {
                return this.SendError(e.Message);
            }
        }
    }
}