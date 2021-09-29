using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Portfolio_Management.Dto;
using Portfolio_Management.Repository.Interface;
using Portfolio_Management.Services.Interface;
using Portfolio_Management.ViewModel;

namespace Portfolio_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StockController : Controller
    {
        private readonly IStockRepository _stockRepository;
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;

        public StockController(IStockRepository stockRepository, IStockService stockService, IMapper mapper)
        {
            _stockRepository = stockRepository;
            _stockService = stockService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var stocks = await _stockRepository.GetStocks();
                return Ok(stocks);
            }
            catch (Exception e)
            {
                return BadRequest("Invalid Request");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            try
            {
                if (id < 0)
                {
                    return BadRequest("Invalid Id");
                }

                var stock = await _stockRepository.FindOrThrowAsync(id);
                return Ok(stock);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> New(StockVm viewModel)
        {
            try
            {
                if (!ModelState.IsValid) return UnprocessableEntity(viewModel);

                var dto = _mapper.Map<StockDto>(viewModel);
                var stock = await _stockService.CreateStock(dto);
                return CreatedAtAction("Details", new { stock.Id }, stock);
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
                if (id < 0)
                {
                    return BadRequest("Invalid Id");
                }

                var stock = await _stockRepository.FindOrThrowAsync(id);
                if (stock == null)
                {
                    return NotFound();
                }

                await _stockService.Remove(stock);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
